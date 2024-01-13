using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool isPause = false;
    [SerializeField]
    GameObject pausePanel;
    [SerializeField]
    PlayerController playerController;

    private void Start()
    {
        GameManager.Instance.PauseEvent += GamePause;
        GameManager.Instance.UnPauseEvent += UnGamePause;
        pausePanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)|| playerController.IsPausePressed)
        {
            Debug.Log("Aa");
            if (GameManager.Instance.GetIsPause())
            {
                GameManager.Instance.UnPauseEvent?.Invoke();
               
            }
            else
            {
                GameManager.Instance.PauseEvent?.Invoke();
               
            }
        }
    }

    /// <summary>
    /// ポーズ画面のボタンにつけるポーズを解除する処理
    /// </summary>
    public void CancelPause()
    {
        GameManager.Instance.UnPauseEvent?.Invoke();
    }
    
    private void GamePause()
    {
        Time.timeScale = 0;
        isPause = true;
        pausePanel.SetActive(isPause);
    }
    
    private void UnGamePause()
    {
        Time.timeScale = 1;
        isPause = false;
        pausePanel.SetActive(isPause);

    }
}
