using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public delegate void  changeTimerHandle();
    public changeTimerHandle changeTimerEvent;
    public float CountTime = 2f;
    [SerializeField]
    SwitchBlockManager switchBlockManager;
    [SerializeField]
    public Image WhiteSprite;
    [SerializeField]
    public Image BlueSprite;
    [SerializeField]
    public AudioSource audioSource;
    
    private bool quarterPassed = false;
    private bool halfPassed = false;
    private bool threeQuartersPassed = false;
    private void Start()
    {
        GameManager.Instance.PauseEvent += PauseTimer;
        GameManager.Instance.UnPauseEvent += UnPauseTimer;
        switchBlockManager.TimerColCameon();
        SoundManager.Instance.StartSE(SEtype.ChangeTimerTin, audioSource);
    }

    public IEnumerator ChangeTimerColor()
    {
        while (true)
        {
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
                switchBlockManager.TimerColCameon();
                SoundManager.Instance.StartSE(SEtype.ChangeTimerTin, audioSource);
                BlueSprite.fillAmount = 1.0f;
                changeTimerEvent?.Invoke();

                // フラグをリセット
                quarterPassed = false;
                halfPassed = false;
                threeQuartersPassed = false;
            }

            yield return null;
        }
    }
    private void PauseTimer()
    {
        SoundManager.Instance.PauseSE(audioSource);
    }
    private void UnPauseTimer()
    {
        audioSource.Play();
    }
}
