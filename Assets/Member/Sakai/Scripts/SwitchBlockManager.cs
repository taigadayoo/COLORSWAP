using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlockManager : MonoBehaviour
{
    public List<SwitchBlock> switchBlocks = new List<SwitchBlock>();
    [SerializeField]
    Timer timer;
    [SerializeField]
    ColorSwitch colorSwitch;

    void Start()
    {
        timer.changeTimerEvent += colorSwitch.ColorChange;
        colorSwitch.changecolor();
        StartCoroutine(timer.ChangeTimerColor());

        foreach (SwitchBlock switchBlock in switchBlocks)
        {
            switchBlock.StartSwitching();
        }
    }
}
