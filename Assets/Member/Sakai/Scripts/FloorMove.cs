using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 床の動きを制御するクラス
public class FloorMove : MonoBehaviour
{
    // 移動方向の列挙型
    private enum Direction
    {
        Side,          // 横移動
        Vertical,      // 縦移動
        StopSide,      // 停止横移動
        StopVertical    // 停止縦移動
    }

    // 移動方向を指定するための変数
    [SerializeField]
    Direction direction;

    public float moveSpeed = 500.0f; // 移動速度
    public float maxHeight = 250.0f; // 上限高さ
    public float minHeight = -250.0f; // 下限高さ
    public float Stoppos = 500f; // 停止位置

    public bool isReversing = false; // 速度反転フラグ

    private float currentVelocity = 1.0f; // 現在の速度
    public Rigidbody2D rb; // Rigidbody2D コンポーネント

    void Start()
    {
        // Rigidbody2D コンポーネントを取得
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 縦移動の場合の処理
        if (direction == Direction.Vertical)
        {
            // 新しい位置を計算
            float newPosition = transform.position.y + currentVelocity * moveSpeed * Time.deltaTime;

            // 上限・下限をチェックして、反転の必要があるかを判定
            if ((newPosition >= minHeight && newPosition <= maxHeight) && isReversing)
            {
                isReversing = false; // 反転フラグをオフ
            }

            // 上限または下限を超えた場合、速度を反転
            if (newPosition > maxHeight || newPosition < minHeight)
            {
                if (!isReversing)
                {
                    currentVelocity *= -1.0f; // 速度を反転
                    isReversing = true; // 反転フラグをオン
                    float adjustment = currentVelocity > 0 ? 0.01f : -0.01f;
                    // 新しい位置を制限
                    transform.position = new Vector3(transform.position.x, Mathf.Clamp(newPosition, minHeight + adjustment, maxHeight - adjustment), transform.position.z);
                }
            }

            // Rigidbody2D の速度を設定
            rb.velocity = new Vector3(0, currentVelocity * moveSpeed, 0);
        }

        // 横移動の場合の処理
        if (direction == Direction.Side)
        {
            // 新しい位置を計算
            float newPosition = transform.position.x + currentVelocity * moveSpeed * Time.deltaTime;

            // 上限または下限を超えた場合、速度を反転
            if (newPosition > maxHeight || newPosition < minHeight)
            {
                if (!isReversing)
                {
                    currentVelocity *= -1.0f; // 速度を反転
                    isReversing = true; // 反転フラグをオン
                    float adjustment = currentVelocity > 0 ? 0.01f : -0.01f;
                    // 新しい位置を制限
                    transform.position = new Vector3(Mathf.Clamp(newPosition, minHeight + adjustment, maxHeight - adjustment), transform.position.y, transform.position.z);
                }
            }

            // Rigidbody2D の速度を設定
            rb.velocity = new Vector3(currentVelocity * moveSpeed, 0, 0);
        }

        // 停止横移動の場合の処理
        if (direction == Direction.StopSide)
        {
            float newPosition = transform.position.x + moveSpeed * Time.deltaTime;
            // 停止位置を超えた場合、スクリプトを無効化
            if (this.transform.position.x >= Stoppos)
            {
                this.enabled = false; // 自身を無効化
            }
            transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
        }

        // 停止縦移動の場合の処理
        if (direction == Direction.StopVertical)
        {
            float newPosition = transform.position.y + moveSpeed * Time.deltaTime;
            // 停止位置を超えた場合、スクリプトを無効化
            if (this.transform.position.y >= Stoppos)
            {
                this.enabled = false; // 自身を無効化
            }
            transform.position = new Vector3(transform.position.x, newPosition, transform.position.z);
        }
    }

    // プレイヤーが床に触れたときの処理
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // プレイヤーを床の子オブジェクトにする
            other.transform.SetParent(transform);
        }
    }

    // プレイヤーが床から離れたときの処理
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // プレイヤーの親をリセットする
            other.transform.SetParent(null);
        }
    }
}
