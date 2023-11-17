using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool isPause = false;
    [SerializeField]
    GameObject pausePanel;

    private void Start()
    {
        pausePanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnPause();
        }
    }
    //public void Resume()
    //{
    //    Time.timeScale = 1;
    //    pausePanel.SetActive(false);
    //}
    public void OnPause()
    {
        if (isPause == false)
        {
            GameManager.Instance.PauseEvent?.Invoke();
            Time.timeScale = 0;         
        }
        else
        {
            Time.timeScale = 1;
            GameManager.Instance.UnPauseEvent?.Invoke();
        }
        isPause = !isPause;
        pausePanel.SetActive(isPause);
    }
}
