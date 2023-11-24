using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActions;

    private InputAction sceneAction;
    // Start is called before the first frame update

    private void Awake()
    {
        // �A�N�V�����̎Q�Ƃ��擾
        sceneAction = inputActions.FindAction("Fire2");
    }
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �~�{�^����������V�[���J��
        if (Input.GetButton("Fire2")|| Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("FirstStage");
        }
    }
}
