using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlockManager : MonoBehaviour
{
    // SwitchBlock のリスト
    public List<SwitchBlock> switchBlocks = new List<SwitchBlock>();

    // タイマーの参照
    [SerializeField]
    Timer timer;

    // 色を変更するための ColorSwitch の参照
    [SerializeField]
    ColorSwitch colorSwitch;

    void Start()
    {
        // タイマーが時間を変更した時に、ColorChange メソッドを呼び出すイベントを登録
        timer.changeTimerEvent += colorSwitch.ColorChange;

        // 色を初期化
        colorSwitch.changecolor();

        // タイマーの色を変更するコルーチンを開始
        StartCoroutine(timer.ChangeTimerColor());
    }

    // タイマーが来た時に呼び出されるメソッド
    public void TimerColCameon()
    {
        // 全ての SwitchBlock の状態を変更
        foreach (SwitchBlock switchBlock in switchBlocks)
        {
            switchBlock.ChangeCol();
        }
    }
}
