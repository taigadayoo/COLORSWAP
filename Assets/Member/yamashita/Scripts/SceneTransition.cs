using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �R���g���[���[�����ĂȂ��̂œ���m�F�Ƀ}�E�X�g�p�B����܂łɔ����Ă����܂�
        if (Input.GetMouseButtonDown(0) || Input.GetButton("Fire2"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
