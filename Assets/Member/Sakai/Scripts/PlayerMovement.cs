using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    public bool isGrounded = false;
    [SerializeField]
    public GameObject Save;
    public MonoBehaviour targetScript;
   
    void Start()
    {
        targetScript.enabled = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
       
        // WASDキーによる移動
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        if(moveInput < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (moveInput > 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        // ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            
        }
    }

   
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Dead")
        {
            this.transform.position = Save.transform.position; 
        }
        if (other.gameObject.tag == "Door")
        {
            SceneManager.LoadScene("Clear");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Flag")
        {
            Save = other.gameObject;
        }
        if(other.gameObject.tag == "lever")
        {
            targetScript.enabled = true;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "MoveStage")
        {
            transform.SetParent(col.transform);
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "MoveStage")
        {
            transform.SetParent(null);
        }
    }
    public void Jump()
    {
        
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}