using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum MoveDirection
    {
        Horizontal,
        Vertical
    }

    public MoveDirection moveDirection = MoveDirection.Horizontal;
    public float moveSpeed = 5f; // �ړ����x
    public float moveDistance = 5f; // �ړ�����

    private Vector3 initialPosition;
    private bool movingPositive = true;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // �ړ�
        float deltaMovement = moveSpeed * Time.deltaTime;

        if (moveDirection == MoveDirection.Horizontal)
        {
            MovePlatform(Vector2.right * (movingPositive ? 1 : -1) * deltaMovement);
        }
        else if (moveDirection == MoveDirection.Vertical)
        {
            MovePlatform(Vector2.up * (movingPositive ? 1 : -1) * deltaMovement);
        }

        // �ړ������̐���
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
        // �v���C���[�����ɐG�ꂽ�Ƃ��̏���
        if (other.gameObject.CompareTag("Player"))
        {
            // �v���C���[�����̎q�I�u�W�F�N�g�ɂ���
            other.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // �v���C���[�������痣�ꂽ�Ƃ��̏���
        if (other.gameObject.CompareTag("Player"))
        {
            // �v���C���[�̐e�����Z�b�g����
            other.transform.SetParent(null);
        }
    }
}