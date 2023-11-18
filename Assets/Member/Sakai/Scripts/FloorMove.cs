using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloorMove : MonoBehaviour
{
 
    private enum Direction
    {
        Side,
        Vertical
    }
       [SerializeField]
    Direction direction;
    public float moveSpeed = 500.0f; // à⁄ìÆë¨ìx
    public float maxHeight = 250.0f; // è„å¿çÇÇ≥
    public float minHeight = -250.0f; // â∫å¿çÇÇ≥

    private float currentVelocity = 1.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (direction == Direction.Vertical)
        {
            float newPosition = transform.position.y + currentVelocity * moveSpeed * Time.deltaTime;

            if (newPosition > maxHeight || newPosition < minHeight)
            {
                // ë¨ìxÇÃîΩì]
                currentVelocity *= -1.0f;
            }

            // à⁄ìÆ
            rb.velocity = new Vector3(0, currentVelocity * moveSpeed, 0);
        }
        if (direction == Direction.Side)
        {
            float newPosition = transform.position.x + currentVelocity * moveSpeed * Time.deltaTime;

            if (newPosition > maxHeight || newPosition < minHeight)
            {
                // ë¨ìxÇÃîΩì]
                currentVelocity *= -1.0f;
            }

            // à⁄ìÆ
            rb.velocity = new Vector3(currentVelocity * moveSpeed, 0, 0);

        }
    }
}
