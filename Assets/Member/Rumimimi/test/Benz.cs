using System;
using System.Collections;
using UnityEngine;

public class Benz : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody2D;
    
    [SerializeField]
    private int _speed;
    [SerializeField]
    private int _brakes;
    [SerializeField]
    private int _jump;

    private bool HidariFalse = false;

    private int jumpNum = 2;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (jumpNum <= 0)
            {
                return;
            }

            jumpNum--;
            
            var nowSpeed = _rigidbody2D.velocity;
            nowSpeed.y = 0;
            _rigidbody2D.velocity = nowSpeed;
            
            _rigidbody2D.AddForce(new Vector2(0,_jump),ForceMode2D.Impulse);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            var nowSpeed = _rigidbody2D.velocity;
            nowSpeed.x = -_speed;
            _rigidbody2D.velocity = nowSpeed;
            transform.rotation = new Quaternion(0, 0, 0,0);
            HidariFalse = false;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            var nowSpeed = _rigidbody2D.velocity;
            nowSpeed.x = _speed;
            _rigidbody2D.velocity = nowSpeed;
            transform.rotation = new Quaternion(0, 180, 0,0);
            HidariFalse = true;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            var nowSpeed = _rigidbody2D.velocity;
            
            if (!HidariFalse)
            {
                nowSpeed.x += _brakes;
                if (nowSpeed.x >= 0)
                {
                    nowSpeed.x = 0;
                    return; 
                }
            }
            else
            {
                nowSpeed.x -= _brakes;
                if (nowSpeed.x <= 0)
                {
                    nowSpeed.x = 0;
                    return; 
                }
            }

            _rigidbody2D.velocity = nowSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            jumpNum = 2;
        }
    }
}
