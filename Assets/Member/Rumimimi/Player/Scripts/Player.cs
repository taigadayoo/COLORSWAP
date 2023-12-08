using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private PlayerController _playerController;
    private Animator _animator;

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
    private bool isFly             = false;
    private bool isGravityReversed = false;

    private Vector2 movementVector;
    private enum GravitySwitch
    {
        On,
        Off
    }
    [SerializeField]
    GravitySwitch gravitySwitch;
    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _rigidbody2D      = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer   = gameObject.GetComponent<SpriteRenderer>();
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
        currentJumpCount  = maxJumpCount;
        var jumpC = gameObject.GetComponentInChildren<PlayerJumpController>();
        jumpC.JumpEvent += ResetJump;
        jumpC.CantGravityReversedEvent += CantGravityReversedEvent;
    }

    private void Update()
    {
        HandleMovement(_playerController.Horizontal);
        if (_playerController.IsJumpPressed)
        {
            HandleJump();
        }
        if (_playerController.IsGravityReversePressed && gravitySwitch == GravitySwitch.On)
        {
            HandleGravityReverse();
            currentJumpCount = 0;
        }

        if (_playerController.IsPausePressed)
        {
            if (GameManager.Instance.GetIsPause())
            {
                GameManager.Instance.UnPauseEvent?.Invoke();
            }
            else
            {
                GameManager.Instance.PauseEvent?.Invoke();

            }
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
            _spriteRenderer.flipX = isGravityReversed;
            isFacingRight = true;
        }
        //右
        else if (horizontalInput > 0)
        {
            _spriteRenderer.flipX = !isGravityReversed;
            isFacingRight = false;
        }
        
        
        
        if(horizontalInput == 0)
        {
            //Animator State Chenge
            _animator.SetBool("IsMove",false);
        }
        else
        {
            //Animator State Chenge
            _animator.SetBool("IsMove",true);
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
        //Animator State Chenge
        _animator.SetBool("IsJump",true);
    }

    /// <summary>
    /// 反転に関する制御
    /// </summary>
    private void HandleGravityReverse()
    {
        if (isFly || currentJumpCount != maxJumpCount)
        {
            return;
        }
        
        isGravityReversed = !isGravityReversed;
        _rigidbody2D.gravityScale *= -1;
        transform.Rotate(0,0,180);
            
        //Animator State Chenge
        _animator.SetBool("IsJump",true);
    }
    
    /// <summary>
    /// ジャンプ状態の初期化
    /// </summary>
    private void ResetJump()
    {
        currentJumpCount = maxJumpCount;
        _animator.SetBool("IsJump",false);
        canJump = true;
        isFly = false;
    }

    /// <summary>
    /// 地面から足が離れた場合
    /// </summary>
    private void CantGravityReversedEvent()
    {
        _animator.SetBool("IsJump",true);
        isFly = true;
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
