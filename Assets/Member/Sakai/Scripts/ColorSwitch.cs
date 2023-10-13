using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitch : MonoBehaviour
{
    public Color whiteColor = Color.white;
    public Color blueColor = Color.blue;
    public float colorSwitchInterval = 3f;

    private Renderer objectRenderer;
    private bool isWhite = true;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        InvokeRepeating("SwitchColor", 0f, colorSwitchInterval);
    }

    private void SwitchColor()
    {
        objectRenderer.material.color = isWhite ? whiteColor : blueColor;
        isWhite = !isWhite;
    }
}
