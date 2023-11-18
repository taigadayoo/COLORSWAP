using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum MoveDirection
    {
        Horizontal,
        Vertical
    }

    public MoveDirection moveDirection = MoveDirection.Horizontal;
    public float moveSpeed = 5f; // 移動速度
    public float moveDistance = 5f; // 移動距離

    private Vector3 initialPosition;
    private bool movingPositive = true;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // 移動
        float deltaMovement = moveSpeed * Time.deltaTime;

        if (moveDirection == MoveDirection.Horizontal)
        {
            MovePlatform(Vector2.right * (movingPositive ? 1 : -1) * deltaMovement);
        }
        else if (moveDirection == MoveDirection.Vertical)
        {
            MovePlatform(Vector2.up * (movingPositive ? 1 : -1) * deltaMovement);
        }

        // 移動距離の制限
        if (Mathf.Abs(GetDistance()) >= moveDistance)
        {
            movingPositive = !movingPositive;
        }
    }

    void MovePlatform(Vector2 movement)
    {
        transform.Translate(movement);
    }

    float GetDistance()
    {
        if (moveDirection == MoveDirection.Horizontal)
        {
            return transform.position.x - initialPosition.x;
        }
        else if (moveDirection == MoveDirection.Vertical)
        {
            return transform.position.y - initialPosition.y;
        }

        return 0f;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // プレイヤーが床に触れたときの処理
        if (other.gameObject.CompareTag("Player"))
        {
            // プレイヤーを床の子オブジェクトにする
            other.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // プレイヤーが床から離れたときの処理
        if (other.gameObject.CompareTag("Player"))
        {
            // プレイヤーの親をリセットする
            other.transform.SetParent(null);
        }
    }
}