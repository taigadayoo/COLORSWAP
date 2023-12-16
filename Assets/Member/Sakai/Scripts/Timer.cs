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
    public Image WhiteSprite;
    [SerializeField]
    public Image BlueSprite;
    [SerializeField]
    private AudioSource audioSource;
    private void Start()
    {
        GameManager.Instance.PauseEvent += PauseTimer;
        GameManager.Instance.UnPauseEvent += UnPauseTimer;
        SoundManager.Instance.StartSE(SEtype.ChangeTimer, audioSource);
       
    }

    public IEnumerator ChangeTimerColor()
    {
        while (true)
        {
            BlueSprite.fillAmount -= 1.0f / CountTime * Time.deltaTime;
            if (BlueSprite.fillAmount <= 0 || BlueSprite.fillAmount >= 1)
            {
                BlueSprite.fillAmount = 1.0f;
                changeTimerEvent?.Invoke();
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
