using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private InputActionAsset inputActions;
    
    public float Horizontal { get; private set; }
    public bool IsJumpPressed { get; private set; }
    public bool IsGravityReversePressed { get; private set; }
    public bool IsPausePressed { get; set; }
    
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction gravityReverseAction;
    private InputAction pauseAction;

    private void Awake()
    {
        // アクションの参照を取得
        moveAction = inputActions.FindAction("Move");
        jumpAction = inputActions.FindAction("Jump");
        gravityReverseAction = inputActions.FindAction("GravityReverse");
        pauseAction = inputActions.FindAction("Pause");
    }

    private void OnEnable()
    {
        // アクションを有効化
        moveAction.Enable();
        jumpAction.Enable();
        gravityReverseAction.Enable();
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        // アクションを無効化
        moveAction.Disable();
        jumpAction.Disable();
        gravityReverseAction.Disable();
        pauseAction.Disable();
    }

    private void Update()
    {
        // 他のアクションの状態を更新
        IsJumpPressed = jumpAction.triggered;
        IsGravityReversePressed = gravityReverseAction.triggered;
        IsPausePressed = pauseAction.triggered;
        
        // Vector2型の値として入力を読み取る
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Horizontal = moveInput.x; // X軸（水平方向）の値を取得
    }
}