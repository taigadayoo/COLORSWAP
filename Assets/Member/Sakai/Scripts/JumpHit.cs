using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHit : MonoBehaviour
{

    public PlayerMovement move;
    // Start is called before the first frame update
    void OnCollisionStay2D(Collision2D other)
    {
        // �n�ʂɐڂ��Ă��邩����
        move.isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // �n�ʂ��痣�ꂽ������
        move.isGrounded = false;
    }
}


