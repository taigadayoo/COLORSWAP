using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    public delegate void JumpHandle();
    public JumpHandle JumpEvent;

    public delegate void CantGravityReversedHandle();
    public CantGravityReversedHandle CantGravityReversedEvent;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            JumpEvent.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            CantGravityReversedEvent.Invoke();
        }
    }
}
