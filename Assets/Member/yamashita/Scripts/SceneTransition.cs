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
        // コントローラー持ってないので動作確認にマウス使用。次回までに買っておきます
        if (Input.GetMouseButtonDown(0) || Input.GetButton("Fire2"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
