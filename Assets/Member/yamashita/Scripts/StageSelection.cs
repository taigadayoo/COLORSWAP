using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelection : MonoBehaviour
{
    [SerializeField] private GameObject _selectButton;
    [SerializeField] private GameObject _stage1;
    [SerializeField] private GameObject _stage2;
    [SerializeField] private GameObject _stage3;
    [SerializeField] private GameObject _stage4;
    [SerializeField] private GameObject _stage5;

    [SerializeField] private string Stage1;
    [SerializeField] private string Stage2;
    [SerializeField] private string Stage3;
    [SerializeField] private string Stage4;
    [SerializeField] private string Stage5;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    private float beforeInput;
    private int sizeX = 1920;

    // Update is called once per frame
    void Update()
    {
        var buttonPos = _selectButton.gameObject.transform.position;
        
        float select = Input.GetAxis("Horizontal");
        // 右のステージに移動
        if (select > 0f && beforeInput == 0f)
        {
            if (buttonPos == _stage1.transform.position)
            {
                StageSelect(1);
            }
            else if (buttonPos == _stage2.transform.position)
            {
                StageSelect(2);
            }
            else if (buttonPos == _stage3.transform.position)
            {
                StageSelect(3);
            }
            else if (buttonPos == _stage4.transform.position)
            {
                StageSelect(4);
            }
            else if (buttonPos == _stage5.transform.position)
            {
                StageSelect(0);
            }
        }
        // 左のステージに移動
        else if (select < 0f && beforeInput == 0f)
        {
            if (buttonPos == _stage1.transform.position)
            {
                StageSelect(4);
            }
            else if (buttonPos == _stage2.transform.position)
            {
                StageSelect(0);
            }
            else if (buttonPos == _stage3.transform.position)
            {
                StageSelect(1);
            }
            else if (buttonPos == _stage4.transform.position)
            {
                StageSelect(2);
            }
            else if (buttonPos == _stage5.transform.position)
            {
                StageSelect(3);
            }
        }
        beforeInput = select;

        if (buttonPos == _stage1.transform.position && Input.GetButton("Fire2"))
        {
            Initiate.Fade(Stage1, fadeColor, fadeSpeed);
        }
        if (buttonPos == _stage2.transform.position && Input.GetButton("Fire2"))
        {
            Initiate.Fade(Stage2, fadeColor, fadeSpeed);
        }
        if (buttonPos == _stage3.transform.position && Input.GetButton("Fire2"))
        {
            Initiate.Fade(Stage3, fadeColor, fadeSpeed);
        }
        if (buttonPos == _stage4.transform.position && Input.GetButton("Fire2"))
        {
            Initiate.Fade(Stage4, fadeColor, fadeSpeed);
        }
        if (buttonPos == _stage5.transform.position && Input.GetButton("Fire2"))
        {
            Initiate.Fade(Stage5, fadeColor, fadeSpeed);
        }
    }

    private void StageSelect(int num)
    {
        transform.localPosition = new Vector2(-sizeX * num, 0);
    }
}
