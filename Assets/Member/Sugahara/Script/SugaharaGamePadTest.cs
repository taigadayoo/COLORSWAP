using UnityEngine;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Haptics;

//public class DualShock4GamepadHID : DualShockGamepad, IDualShockHaptics, IDualMotorRumble, IHaptics, IEventPreProcessor
//{
//}




public class SugaharaGamePadTest : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            DualShock4GamepadHID.current.SetLightBarColor(Color.red);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {

            DualShock4GamepadHID.current.SetLightBarColor(Color.white);

        }
    }
}
