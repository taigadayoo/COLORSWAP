using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    public delegate void JumpHandle();
    public JumpHandle JumpEvent;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            JumpEvent.Invoke();
        }
    }
}
