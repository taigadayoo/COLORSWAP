using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGimik : MonoBehaviour
{

    // GameManager�ւ̎Q�Ƃ�ǉ�
    public GameManager gameManager;


    void Start()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
      
        if (other.gameObject.tag == "Door")
        {

            gameManager.OpenDoor();
        }
    }

   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dead")
        {

            gameManager.RespawnPlayer();
        }
        if (other.gameObject.tag == "Flag")
        {

            gameManager.SetSavePoint(other.gameObject);

          
        }
        if (other.gameObject.tag == "lever")
        {
            // Lever�̃A�N�e�B�x�[�V������GameManager�ɈϔC
            gameManager.ActivateLever();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "MoveStage")
        {
            // �v���C���[�̐e�I�u�W�F�N�g�̐ݒ��GameManager�ɈϔC
            gameManager.ParentPlayerToMoveStage(col.transform);
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "MoveStage")
        {
            // �v���C���[�̐e�I�u�W�F�N�g�����Z�b�g��GameManager�ɈϔC
            gameManager.UnparentPlayerFromMoveStage();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}