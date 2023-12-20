using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitchBlock : MonoBehaviour
{
    public bool hitJudgment = false;
    public BoxCollider2D col;
    public Color blueColor = Color.blue;
    public Color whiteColor = Color.white;
    enum Block
    {
        First,
        Next
    }
    public float switchInterval = 2f;
    [SerializeField]
    private Block block;

    public void ChangeCol()
    {
            hitJudgment = !hitJudgment;
    }
    private void Update()
    {
        if (block == Block.First)
        {
            if (hitJudgment)
            {
                col.enabled = true;
                gameObject.GetComponent<Renderer>().material.color = blueColor;
            }
            else
            {
                col.enabled = false;
                gameObject.GetComponent<Renderer>().material.color = Color.clear;
            }
        }
        else if (block == Block.Next)
        {
            if (hitJudgment)
            {
                col.enabled = false;
                gameObject.GetComponent<Renderer>().material.color = Color.clear;
            }
            else
            {
                col.enabled = true;
                gameObject.GetComponent<Renderer>().material.color = whiteColor;
            }
        }
    }

 
}
