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

    public bool IsNextPressed { get; set; }

    public bool IsTitlePressed { get; set; }

    public bool IsSelectPressed { get; set; }

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction gravityReverseAction;
    private InputAction pauseAction;
    private InputAction nextAction;
    private InputAction titleAction;
    private InputAction selectAction;

    private void Awake()
    {
        // アクションの参照を取得
        moveAction = inputActions.FindAction("Move");
        jumpAction = inputActions.FindAction("Jump");
        gravityReverseAction = inputActions.FindAction("GravityReverse");
        pauseAction = inputActions.FindAction("Pause");
        nextAction = inputActions.FindAction("Next");
        titleAction = inputActions.FindAction("Title");
       selectAction= inputActions.FindAction("sceneSelect");
    }

    private void OnEnable()
    {
        // アクションを有効化
        moveAction.Enable();
        jumpAction.Enable();
        gravityReverseAction.Enable();
        pauseAction.Enable();
        nextAction.Enable();
        titleAction.Enable();
        selectAction.Enable();
    }

    private void OnDisable()
    {
        // アクションを無効化
        moveAction.Disable();
        jumpAction.Disable();
        gravityReverseAction.Disable();
        pauseAction.Disable();
        nextAction.Disable();
        titleAction.Disable();
        selectAction.Disable();
    }

    private void Update()
    {
        // 他のアクションの状態を更新
        IsJumpPressed = jumpAction.triggered;
        IsGravityReversePressed = gravityReverseAction.triggered;
        IsPausePressed = pauseAction.triggered;
        IsTitlePressed = titleAction.triggered;
        IsNextPressed = nextAction.triggered;
        IsSelectPressed = selectAction.triggered;

        // Vector2型の値として入力を読み取る
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Horizontal = moveInput.x; // X軸（水平方向）の値を取得
    }
}