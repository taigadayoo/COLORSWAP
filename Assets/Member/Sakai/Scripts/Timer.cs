using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // タイマーが変更された時に呼び出されるデリゲート
    public delegate void changeTimerHandle();
    public changeTimerHandle changeTimerEvent;

    // カウントダウンの時間
    public float CountTime = 2f;

    [SerializeField]
    // SwitchBlockManager の参照
    SwitchBlockManager switchBlockManager;

    [SerializeField]
    // 白いスプライトの Image
    public Image WhiteSprite;

    [SerializeField]
    // 青いスプライトの Image
    public Image BlueSprite;

    [SerializeField]
    // 音を出すための AudioSource
    public AudioSource audioSource;

    // 経過時間を示すフラグ
    private bool quarterPassed = false;
    private bool halfPassed = false;
    private bool threeQuartersPassed = false;

    private void Start()
    {
        // ゲームマネージャーからのイベントにリスナーを登録
        GameManager.Instance.PauseEvent += PauseTimer;
        GameManager.Instance.UnPauseEvent += UnPauseTimer;

        // SwitchBlockManager のタイマー色変更メソッドを呼び出し
        switchBlockManager.TimerColCameon();

        // サウンドエフェクトを開始
        SoundManager.Instance.StartSE(SEtype.ChangeTimerTin, audioSource);
    }

    // タイマーの色を変更するコルーチン
    public IEnumerator ChangeTimerColor()
    {
        while (true)
        {
            // 青いスプライトの進行状況を減少させる
            BlueSprite.fillAmount -= 1.0f / CountTime * Time.deltaTime;

            // 4分の3が経過したとき
            if (BlueSprite.fillAmount <= 0.75 && !quarterPassed)
            {
                SoundManager.Instance.StartSE(SEtype.ChangeTimerPon, audioSource);
                quarterPassed = true; // フラグを設定
            }
            // 半分が経過したとき
            else if (BlueSprite.fillAmount <= 0.5 && !halfPassed)
            {
                SoundManager.Instance.StartSE(SEtype.ChangeTimerPon, audioSource);
                halfPassed = true; // フラグを設定
            }
            // 4分の1が経過したとき
            else if (BlueSprite.fillAmount <= 0.25 && !threeQuartersPassed)
            {
                SoundManager.Instance.StartSE(SEtype.ChangeTimerPon, audioSource);
                threeQuartersPassed = true; // フラグを設定
            }
            // タイマーが終了したとき
            else if (BlueSprite.fillAmount <= 0)
            {
                // SwitchBlockManager の状態を変更
                switchBlockManager.TimerColCameon();
                SoundManager.Instance.StartSE(SEtype.ChangeTimerTin, audioSource);
                BlueSprite.fillAmount = 1.0f; // 進行状況をリセット
                changeTimerEvent?.Invoke(); // イベントを呼び出す

                // フラグをリセット
                quarterPassed = false;
                halfPassed = false;
                threeQuartersPassed = false;
            }

            yield return null; // 次のフレームまで待機
        }
    }

    // タイマーを一時停止するメソッド
    private void PauseTimer()
    {
        SoundManager.Instance.PauseSE(audioSource);
    }

    // タイマーを再開するメソッド
    private void UnPauseTimer()
    {
        audioSource.Play();
    }
}
