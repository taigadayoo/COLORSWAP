using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Color whiteColor = Color.white;
    public Color blueColor = Color.blue;
    public float colorSwitchInterval = 2f;

    private Renderer objectRenderer;
    private bool isWhite = true;
    enum CircleColor
    {
        front,
        back
    }
    [SerializeField]
    private CircleColor circlecolor;
    public Image UIobj;
    public bool roop;
    public float countTime = 2.0f;
    // Update is called once per frame
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (circlecolor == CircleColor.front)
        {
            InvokeRepeating("SwitchColorfront", 0f, colorSwitchInterval);
        }
        if (circlecolor == CircleColor.back)
        {
            InvokeRepeating("SwitchColorback", 0f, colorSwitchInterval);
        }
        StartCoroutine(UpdateUIFill());
    }

    private void SwitchColorfront()
    {
        UIobj.color = isWhite ? whiteColor : blueColor;
        isWhite = !isWhite;
    }
    private void SwitchColorback()
    {
        UIobj.color = isWhite ? blueColor : whiteColor;
        isWhite = !isWhite;
    }
    private IEnumerator UpdateUIFill()
    {
        while (true)
        {
            if (roop)
            {
                UIobj.fillAmount -= 1.0f / countTime * Time.deltaTime;
            }
            else
            {
                UIobj.fillAmount = 1.0f;
            }

            if (UIobj.fillAmount <= 0 || UIobj.fillAmount >= 1)
            {
                UIobj.fillClockwise = !UIobj.fillClockwise;
                roop = !roop;
            }

            yield return null; // 1ÉtÉåÅ[ÉÄë“Ç¬
        }
    }
}
