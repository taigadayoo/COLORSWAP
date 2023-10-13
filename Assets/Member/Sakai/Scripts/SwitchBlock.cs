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
    public float switchInterval = 3f;
    [SerializeField]
    private Block block;
    void Start()
    {
        InvokeRepeating("SwitchBlocks", 0f, switchInterval);
    }

    private void Update()
    {
        if(block == Block.First)
        {
            if(hitJudgment)
            {
                col.enabled = true;
                gameObject.GetComponent<Renderer>().material.color = blueColor;
            }
            if (!hitJudgment)
            {
                col.enabled = false;
                gameObject.GetComponent<Renderer>().material.color = Color.clear;
            }
        }
        if (block == Block.Next)
        {
            if (hitJudgment)
            {
                col.enabled = false;
                gameObject.GetComponent<Renderer>().material.color = Color.clear;
            }
            if (!hitJudgment)
            {
                col.enabled = true;
                gameObject.GetComponent<Renderer>().material.color = whiteColor;
            }
        }
    }
    private void SwitchBlocks()
    {
        hitJudgment = !hitJudgment;
        Debug.Log( hitJudgment);
    }
    
}
