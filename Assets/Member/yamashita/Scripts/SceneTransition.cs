using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ×ボタン押したらシーン遷移
        if (Input.GetButton("Fire2") || Input.GetKeyDown(KeyCode.M))
        {
           Initiate.Fade(sceneName, fadeColor, fadeSpeed);
        }
    }
}
