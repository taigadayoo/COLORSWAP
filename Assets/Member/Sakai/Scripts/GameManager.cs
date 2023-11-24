using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void PaudeHandller();
    public PaudeHandller PauseEvent;
    public delegate void UnPauseHandller();
    public PaudeHandller UnPauseEvent;

    [SerializeField]
    public GameObject player;
    public GameObject savePoint;
    public MonoBehaviour targetScript = null;
    public GameObject door;
    public GameObject flag;
    public GameObject lever;
    public GameObject moveStage;

    public Sprite newleverSprite;
    private Sprite leverimage;
    //public Sprite newSaveSprite;


    private bool isPause;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

        leverimage = lever.GetComponent<Sprite>();
        UnPauseEvent += StartBGM;
        UnPauseEvent += ChangePause;
        PauseEvent += PauseBGM;
        PauseEvent += ChangePause;

        if(savePoint == null)
        {
            savePoint = flag;
        }
        if (targetScript != null)
        {
            targetScript.enabled = false;
        }
        StartBGM();
    }

    public void RespawnPlayer()
    {
        if (player != null && savePoint != null)
        {
           player.transform.position = savePoint.transform.position;
        }
    }

    public void LoadClearScene()
    {
        SceneManager.LoadScene("Clear");
    }

    public void SetSavePoint(GameObject newFlag)
    {
        savePoint = newFlag;
       
    }

    public void EnableTargetScript()
    {
        if (targetScript != null)
        {
            targetScript.enabled = true;
        }
    }

    public void ParentPlayerToMoveStage(Transform stage)
    {
        player.transform.SetParent(stage);
    }

    public void UnparentPlayerFromMoveStage()
    {
        player.transform.SetParent(null);
    }

    public void OpenDoor()
    {
        SceneManager.LoadScene("SecondStage");
    }
    public void GoolDoor()
    {
        SceneManager.LoadScene("Clear");
    }

    public void ActivateLever()
    {
        if (targetScript != null)
        {
            leverimage = newleverSprite;
            targetScript.enabled = true;
        }
    }

   private void StartBGM()
    {
        SoundManager.Instance.PlayBGM(BGMtype.title);
    }

    private void PauseBGM()
    {
        SoundManager.Instance.PauseBGM();
    }

    public bool GetIsPause()
    {
        return isPause;
    }

    private void ChangePause()
    {
        isPause = !isPause;
    }
    

    // Update is called once per frame
  
    }

