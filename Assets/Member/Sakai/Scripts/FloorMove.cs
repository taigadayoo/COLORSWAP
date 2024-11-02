using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloorMove : MonoBehaviour
{
 
    private enum Direction
    {
        Side,
        Vertical,
        StopSide,
        StopVertical
    }
       [SerializeField]
    Direction direction;
    public float moveSpeed = 500.0f; // 移動速度
    public float maxHeight = 250.0f; // 上限高さ
    public float minHeight = -250.0f; // 下限高さ
    public float Stoppos = 500f;

    public bool isReversing = false;

    private float currentVelocity = 1.0f;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (direction == Direction.Vertical)
        {
            float newPosition = transform.position.y + currentVelocity * moveSpeed * Time.deltaTime;

            
            if ((newPosition >= minHeight && newPosition <= maxHeight) && isReversing)
            {
                isReversing = false;
            }
            if (newPosition > maxHeight || newPosition < minHeight)
            {
                // 速度の反転
                if (!isReversing)
                {
                    currentVelocity *= -1.0f;
                    isReversing = true;
                    float adjustment = currentVelocity > 0 ? 0.01f : -0.01f;
                    transform.position = new Vector3 (transform.position.x, Mathf.Clamp(newPosition, minHeight + adjustment, maxHeight - adjustment) ,transform.position.z);
                }
            }

            // 移動
            rb.velocity = new Vector3(0, currentVelocity * moveSpeed, 0);
        }
        if (direction == Direction.Side)
        {
            float newPosition = transform.position.x + currentVelocity * moveSpeed * Time.deltaTime;

            if (newPosition > maxHeight || newPosition < minHeight)
            {
                if (!isReversing)
                {
                    currentVelocity *= -1.0f;
                    isReversing = true;
                    float adjustment = currentVelocity > 0 ? 0.01f : -0.01f;
                    transform.position = new Vector3(Mathf.Clamp(newPosition, minHeight + adjustment, maxHeight - adjustment),transform.position.y, transform.position.z);
                }
            }

            // 移動
            rb.velocity = new Vector3(currentVelocity * moveSpeed, 0, 0);

        }
        if(direction == Direction.StopSide)
        {       
            float newPosition = transform.position.x + moveSpeed * Time.deltaTime;
            if(this.transform.position.x >= Stoppos)
            {

                this.enabled = false;
            }
            transform.position = new Vector3(newPosition, transform.position.y,transform.position.z);
        }
        if (direction == Direction.StopVertical)
        {
           
            float newPosition = transform.position.y + moveSpeed * Time.deltaTime;
            if (this.transform.position.y >= Stoppos)
            {
                this.enabled = false;
            }
            transform.position = new Vector3(transform.position.x, newPosition, transform.position.z);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        // プレイヤーが床に触れたときの処理
        if (other.gameObject.CompareTag("Player"))
        {
            // プレイヤーを床の子オブジェクトにする
            other.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // プレイヤーが床から離れたときの処理
        if (other.gameObject.CompareTag("Player"))
        {
            // プレイヤーの親をリセットする
            other.transform.SetParent(null);
        }
    }
}
