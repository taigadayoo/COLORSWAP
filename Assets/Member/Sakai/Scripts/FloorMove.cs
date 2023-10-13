using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloorMove : MonoBehaviour
{
    public float moveSpeed = 500.0f; // �ړ����x
    public float maxHeight = 250.0f; // �������
    public float minHeight = -250.0f; // ��������

    private float currentVelocity = 1.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float newPosition = transform.position.y + currentVelocity * moveSpeed * Time.deltaTime;

        if (newPosition > maxHeight || newPosition < minHeight)
        {
            // ���x�̔��]
            currentVelocity *= -1.0f;
        }

        // �ړ�
        rb.velocity = new Vector3(0, currentVelocity * moveSpeed, 0);
    }
}
