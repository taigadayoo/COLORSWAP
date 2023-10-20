using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitch : MonoBehaviour
{
    public Color whiteColor = Color.white;
    public Color blueColor = Color.blue;
    public float colorSwitchInterval = 2f;

    private Renderer objectRenderer;
    private bool isWhite = true;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        StartCoroutine(SwitchBackCoroutine());
    }

   
        
        IEnumerator SwitchBackCoroutine()
        {
            while (true)
            {
                objectRenderer.material.color = isWhite ? whiteColor : blueColor;
                isWhite = !isWhite;
                yield return new WaitForSeconds(colorSwitchInterval);
            }

        }
    
}
