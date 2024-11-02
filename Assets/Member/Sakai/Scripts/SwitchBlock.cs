using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlock : MonoBehaviour
{
    // ヒット判定のフラグ
    public bool hitJudgment = false;

    // BoxCollider2D コンポーネント
    public BoxCollider2D col;

    // ブロックの色
    public Color blueColor = Color.blue;
    public Color whiteColor = Color.white;

    // ブロックの種類を定義する列挙型
    enum Block
    {
        First,
        Next
    }

    // スイッチの切り替え間隔
    public float switchInterval = 2f;

    // 現在のブロックの種類
    [SerializeField]
    private Block block;

    // ヒット判定を切り替えるメソッド
    public void ChangeCol()
    {
        hitJudgment = !hitJudgment; // hitJudgment の値を反転
    }

    private void Update()
    {
        // 現在のブロックが First の場合
        if (block == Block.First)
        {
            if (hitJudgment)
            {
                col.enabled = true; // コライダーを有効にする
                gameObject.GetComponent<Renderer>().material.color = blueColor; // 色を青に変更
            }
            else
            {
                col.enabled = false; // コライダーを無効にする
                gameObject.GetComponent<Renderer>().material.color = Color.clear; // 色を透明に変更
            }
        }
        // 現在のブロックが Next の場合
        else if (block == Block.Next)
        {
            if (hitJudgment)
            {
                col.enabled = false; // コライダーを無効にする
                gameObject.GetComponent<Renderer>().material.color = Color.clear; // 色を透明に変更
            }
            else
            {
                col.enabled = true; // コライダーを有効にする
                gameObject.GetComponent<Renderer>().material.color = whiteColor; // 色を白に変更
            }
        }
    }
}
