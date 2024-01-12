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

    private float beforeInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //var a = _selectButton.transform.position;
        
        float select = Input.GetAxis("Horizontal");
        // 右のステージに移動
        if (select > 0f && beforeInput == 0f)
        {
            if (_selectButton.gameObject.transform.position == _stage1.transform.position)
                _selectButton.gameObject.transform.position = _stage2.transform.position;
            else if (_selectButton.gameObject.transform.position == _stage2.transform.position)
                _selectButton.gameObject.transform.position = _stage3.transform.position;
            else if (_selectButton.gameObject.transform.position == _stage3.transform.position)
                _selectButton.gameObject.transform.position = _stage4.transform.position;
            else if (_selectButton.gameObject.transform.position == _stage4.transform.position)
                _selectButton.gameObject.transform.position = _stage5.transform.position;
            else if (_selectButton.gameObject.transform.position == _stage5.transform.position)
                _selectButton.gameObject.transform.position = _stage1.transform.position;
        }
        // 左のステージに移動
        else if (select < 0f && beforeInput == 0f)
        {
            if (_selectButton.gameObject.transform.position == _stage1.transform.position)
                _selectButton.gameObject.transform.position = _stage5.transform.position;
            else if (_selectButton.gameObject.transform.position == _stage2.transform.position)
                _selectButton.gameObject.transform.position = _stage1.transform.position;
            else if (_selectButton.gameObject.transform.position == _stage3.transform.position)
                _selectButton.gameObject.transform.position = _stage2.transform.position;
            else if (_selectButton.gameObject.transform.position == _stage4.transform.position)
                _selectButton.gameObject.transform.position = _stage3.transform.position;
            else if (_selectButton.gameObject.transform.position == _stage5.transform.position)
                _selectButton.gameObject.transform.position = _stage4.transform.position;
        }
        beforeInput = select;

        if (_selectButton.gameObject.transform.position == _stage1.transform.position && Input.GetButton("Fire2"))
        {
            // 仮のシーン
            SceneManager.LoadScene("StageN");
        }
        if (_selectButton.gameObject.transform.position == _stage2.transform.position && Input.GetButton("Fire2"))
        {
            SceneManager.LoadScene("StageN");
        }
        if (_selectButton.gameObject.transform.position == _stage3.transform.position && Input.GetButton("Fire2"))
        {
            SceneManager.LoadScene("StageN");
        }
        if (_selectButton.gameObject.transform.position == _stage4.transform.position && Input.GetButton("Fire2"))
        {
            SceneManager.LoadScene("StageN");
        }
        if (_selectButton.gameObject.transform.position == _stage5.transform.position && Input.GetButton("Fire2"))
        {
            SceneManager.LoadScene("StageN");
        }
    }
}
