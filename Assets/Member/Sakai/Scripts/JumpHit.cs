using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHit : MonoBehaviour
{

    public PlayerMovement move;
    // Start is called before the first frame update
    void OnCollisionStay2D(Collision2D other)
    {
        // 地面に接しているか判定
        move.isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // 地面から離れたか判定
        move.isGrounded = false;
    }
}


