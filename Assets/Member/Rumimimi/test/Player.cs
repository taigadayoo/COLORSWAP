using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private PlayerController _playerController;

    [Header("ステータス")]
    [SerializeField,Header("移動速度")]
    private int  speed;
    [SerializeField,Header("ブレーキ力")]
    private int  brakes;
    [SerializeField,Header("ジャンプ力")]
    private int  jumpPower;
    [SerializeField,Header("最大ジャンプ数")]
    private int  maxJumpCount = 2;
    private int  currentJumpCount = 0;
    
    private bool isFacingRight     = true;
    private bool canJump           = true;
    private bool isGravityReversed = false;

    private Vector2 movementVector;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _rigidbody2D      = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer   = gameObject.GetComponent<SpriteRenderer>();
        _playerController = GetComponent<PlayerController>();
        currentJumpCount  = maxJumpCount;
    }

    private void Update()
    {
        HandleMovement(_playerController.Horizontal);
        if (_playerController.IsJumpPressed) HandleJump();
        if (_playerController.IsBrakePressed) Brake();
        if (_playerController.IsGravityReversePressed)
        {
            HandleGravityReverse();
            currentJumpCount = 0;
        }
    }

    /// <summary>
    /// 左右移動メソッド
    /// </summary>
    /// <param name="horizontalInput"></param>
    private void HandleMovement(float horizontalInput)
    {
        movementVector = _rigidbody2D.velocity;
        movementVector.x = horizontalInput * speed;
        _rigidbody2D.velocity = movementVector;

        //左
        if (horizontalInput < 0)
        {
            _spriteRenderer.flipX = true;
            isFacingRight = true;
        }
        //右
        else if (horizontalInput > 0)
        {
            _spriteRenderer.flipX = false;
            isFacingRight = false;
        }
    }

    /// <summary>
    /// ブレーキメソッド
    /// </summary>
    private void Brake()
    {
        //右
        if (isFacingRight)
        {
            movementVector.x -= brakes;
            if (movementVector.x <= 0) movementVector.x = 0;
        }
        //左
        else
        {
            movementVector.x += brakes;
            if (movementVector.x >= 0) movementVector.x = 0;
        }
        _rigidbody2D.velocity = movementVector;
    }

    /// <summary>
    /// ジャンプの制御をするメソッド
    /// </summary>
    private void HandleJump()
    {
        if (canJump && currentJumpCount > 0)
        {
            Jump();
            currentJumpCount--;
        }
        if (currentJumpCount <= 0) canJump = false;
    }

    /// <summary>
    /// ジャンプ処理
    /// </summary>
    private void Jump()
    {
        Vector2 jumpVector = new Vector2(0, isGravityReversed ? -jumpPower : jumpPower);
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
        _rigidbody2D.AddForce(jumpVector, ForceMode2D.Impulse);
    }

    /// <summary>
    /// 反転に関する制御
    /// </summary>
    private void HandleGravityReverse()
    {
        if (currentJumpCount == maxJumpCount)
        {
            isGravityReversed = !isGravityReversed;
            _rigidbody2D.gravityScale *= -1;
            _spriteRenderer.flipY = isGravityReversed;
        }
    }
    
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            currentJumpCount = maxJumpCount;
            canJump = true;
        }
    }
}
