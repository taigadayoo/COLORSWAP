using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHit : MonoBehaviour
{

    public PlayerMovement move;
    // Start is called before the first frame update
    void OnCollisionStay2D(Collision2D other)
    {
        // ínñ Ç…ê⁄ÇµÇƒÇ¢ÇÈÇ©îªíË
        move.isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // ínñ Ç©ÇÁó£ÇÍÇΩÇ©îªíË
        move.isGrounded = false;
    }
}


