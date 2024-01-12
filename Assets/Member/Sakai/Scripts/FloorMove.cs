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
        StopSide
    }
       [SerializeField]
    Direction direction;
    public float moveSpeed = 500.0f; // à⁄ìÆë¨ìx
    public float maxHeight = 250.0f; // è„å¿çÇÇ≥
    public float minHeight = -250.0f; // â∫å¿çÇÇ≥

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
                // ë¨ìxÇÃîΩì]
                if (!isReversing)
                {
                    currentVelocity *= -1.0f;
                    isReversing = true;
                    float adjustment = currentVelocity > 0 ? 0.01f : -0.01f;
                    transform.position = new Vector3 (transform.position.x, Mathf.Clamp(newPosition, minHeight + adjustment, maxHeight - adjustment) ,transform.position.z);
                }
            }

            // à⁄ìÆ
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

            // à⁄ìÆ
            rb.velocity = new Vector3(currentVelocity * moveSpeed, 0, 0);

        }
        if(direction == Direction.StopSide)
        {
            float newPosition = transform.position.x + moveSpeed * Time.deltaTime;
            transform.position = new Vector3(newPosition, transform.position.y,transform.position.z);
        }
    }
}
