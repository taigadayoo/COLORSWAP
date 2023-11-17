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
        // ×ボタン押したらシーン遷移
        if (Input.GetButton("Fire2")|| Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(2);
        }
    }
}
