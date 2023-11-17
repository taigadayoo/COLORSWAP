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
        gameObject.GetComponentInChildren<PlayerJumpController>().JumpEvent += ResetJump;
    }

    private void Update()
    {
        HandleMovement(_playerController.Horizontal);
        if (_playerController.IsJumpPressed) HandleJump();
        if (_playerController.IsGravityReversePressed)
        {
            HandleGravityReverse();
            currentJumpCount = 0;
        }

        if (_playerController.IsPausePressed)
        {
            GameManager.Instance.PauseEvent?.Invoke();
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
            _spriteRenderer.flipX = !isGravityReversed;
            isFacingRight = true;
        }
        //右
        else if (horizontalInput > 0)
        {
            _spriteRenderer.flipX = isGravityReversed;
            isFacingRight = false;
        }
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
            transform.Rotate(0,0,180);
        }
    }
    
    /// <summary>
    /// ジャンプ状態の初期化
    /// </summary>
    private void ResetJump()
    {
        currentJumpCount = maxJumpCount;
        canJump = true;
    }

    /// <summary>
    /// 死んだ時の初期化
    /// </summary>
    public void Dead()
    {
        _rigidbody2D.velocity = Vector2.zero;

        if (isGravityReversed == false)
        {
            return;
        }
        ResetJump();
        HandleGravityReverse();
    }
} 
