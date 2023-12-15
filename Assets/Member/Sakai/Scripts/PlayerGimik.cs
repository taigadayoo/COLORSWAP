using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGimik : MonoBehaviour
{
    [SerializeField]
    private Player player;
    // GameManagerへの参照を追加
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
        if (other.gameObject.tag == "Gool")
        {

            gameManager.GoolDoor();

        }
        if (other.gameObject.tag == "Gooltutolial")
        {

            gameManager.OpenDoor2();

        }
    }

   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dead")
        {
            player.Dead();
            gameManager.RespawnPlayer();
        }
        if (other.gameObject.tag == "Flag")
        {

            gameManager.SetSavePoint(other.gameObject);

          
        }
        if (other.gameObject.tag == "lever")
        {
            // LeverのアクティベーションをGameManagerに委任
            gameManager.ActivateLever();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "MoveStage")
        {
            // プレイヤーの親オブジェクトの設定をGameManagerに委任
            gameManager.ParentPlayerToMoveStage(col.transform);
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "Ground")
        {
            // プレイヤーの親オブジェクトをリセットをGameManagerに委任
            gameManager.UnparentPlayerFromMoveStage();
        }

        // Update is called once per frame
      
    }
}