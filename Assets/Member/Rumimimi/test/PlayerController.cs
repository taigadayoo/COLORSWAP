using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Horizontal { get; private set; }
    public bool IsJumpPressed { get; private set; }
    public bool IsBrakePressed { get; private set; }
    public bool IsGravityReversePressed { get; private set; }

    private void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        IsJumpPressed = Input.GetKeyDown(KeyCode.W);
        IsBrakePressed = Input.GetKey(KeyCode.Space);
        IsGravityReversePressed = Input.GetKeyDown(KeyCode.LeftShift);
    }
}