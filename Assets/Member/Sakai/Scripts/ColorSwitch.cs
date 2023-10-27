using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitch : MonoBehaviour
{

    public Color whiteColor;
    public Color blueColor;
    public float colorSwitchInterval = 2f;

    [SerializeField]
    private List<SpriteRenderer> spriteList = new List<SpriteRenderer>();

    private Renderer objectRenderer;
    private bool isWhite = true;

    [SerializeField]
    private Timer timer;
    void Start()
    {
        timer.changeTimerEvent += ColorChange;
        objectRenderer = GetComponent<Renderer>();

        changecolor();
    }



    public void ColorChange()
    {
        isWhite = !isWhite;
        objectRenderer.material.color = isWhite ? whiteColor : blueColor;
        changecolor();
    }
    private void changecolor()
    {
        var _color = Color.white;

        if(isWhite)
        {
            _color = blueColor;
        }
        else
        {
            _color = whiteColor;
        }


        foreach (var sprite in spriteList)
        {
            sprite.color = _color;
        }
    }
}
