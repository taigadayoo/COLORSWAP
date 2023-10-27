using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitch : MonoBehaviour
{
    
    public Color whiteColor = Color.white;
    public Color blueColor = Color.blue;
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

   
        
      public void  ColorChange()
        {
        isWhite = !isWhite;
        objectRenderer.material.color = isWhite ? whiteColor : blueColor;
        changecolor();
        }
   private  void changecolor()
    {
        foreach (var sprite in spriteList)
        {
            if (isWhite)
            {
                sprite.color = new Color(98, 114, 166);
            }
            else
            {
                sprite.color = new Color(255, 255, 255);
            }


        }
    }
}
