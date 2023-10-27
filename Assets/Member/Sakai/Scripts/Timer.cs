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

    private void Start()
    {
        StartCoroutine(ChangeTimerColor());
    }

    private IEnumerator ChangeTimerColor()
    {
        while (true)
        {
            BlueSprite.fillAmount -= 1.0f / CountTime * Time.deltaTime;
            if (BlueSprite.fillAmount <= 0 || BlueSprite.fillAmount >= 1)
            {
                BlueSprite.fillAmount = 1.0f;
                changeTimerEvent.Invoke();
            }
            yield return null;
        }
    }

}
