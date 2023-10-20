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
    void Start()
    {
        StartCoroutine(SwitchHitCoroutine());
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

     IEnumerator SwitchHitCoroutine()
    {
        while (true)
        {
            hitJudgment = !hitJudgment;
            Debug.Log(hitJudgment);
            yield return new WaitForSeconds(switchInterval);
        }
       
    }
    
}
