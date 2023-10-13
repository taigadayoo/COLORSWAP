using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // WASD�L�[�ɂ��ړ�
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // �W�����v
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // �n�ʂɐڂ��Ă��邩����
        isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // �n�ʂ��痣�ꂽ������
        isGrounded = false;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Dead")
        {
            this.transform.position = new Vector3(-1081, 464,0);
        }
    }
}