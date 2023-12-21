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

    [SerializeField] public string sceneName1;
    [SerializeField] public string sceneName2;
    [SerializeField] public string sceneNameTuto;
    [SerializeField] public Color fadeColor;
    [SerializeField] public float fadeSpeed;

    private float beforeInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float select = Input.GetAxis("Horizontal");
        if (select > 0f && beforeInput == 0f)
        {
            if (_selectButton.gameObject.transform.position == _stage1.transform.position)
                _selectButton.gameObject.transform.position = _stage2.transform.position;
            else if (_selectButton.gameObject.transform.position == _stage2.transform.position)
                _selectButton.gameObject.transform.position = _stage3.transform.position;
            else if (_selectButton.gameObject.transform.position == _stage3.transform.position)
                _selectButton.gameObject.transform.position = _stage1.transform.position;
        }
        else if (select < 0f && beforeInput == 0f)
        {
            if (_selectButton.gameObject.transform.position == _stage1.transform.position)
                _selectButton.gameObject.transform.position = _stage3.transform.position;
            else if (_selectButton.gameObject.transform.position == _stage2.transform.position)
                _selectButton.gameObject.transform.position = _stage1.transform.position;
            else if (_selectButton.gameObject.transform.position == _stage3.transform.position)
                _selectButton.gameObject.transform.position = _stage2.transform.position;
        }
        beforeInput = select;

        if (_selectButton.gameObject.transform.position == _stage1.transform.position && Input.GetButton("Fire2") || _selectButton.gameObject.transform.position == _stage1.transform.position && Input.GetKeyDown(KeyCode.M))
        {
            Initiate.Fade(sceneName1,fadeColor, fadeSpeed);
        }
        if (_selectButton.gameObject.transform.position == _stage2.transform.position && Input.GetButton("Fire2") || _selectButton.gameObject.transform.position == _stage2.transform.position && Input.GetKeyDown(KeyCode.M))
        {
            Initiate.Fade(sceneName2, fadeColor, fadeSpeed);
        }
        if (_selectButton.gameObject.transform.position == _stage3.transform.position && Input.GetButton("Fire2") || _selectButton.gameObject.transform.position == _stage3.transform.position && Input.GetKeyDown(KeyCode.M))
        {
            Initiate.Fade(sceneNameTuto, fadeColor, fadeSpeed);
        }
    }
}
