using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelection1 : MonoBehaviour
{
    private int sizeX = 1920;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StageSelect(int num)
    {
        transform.localPosition = new Vector2(-sizeX * num, 0);
    }
}
