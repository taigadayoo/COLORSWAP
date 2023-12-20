using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public Timer timer;
    [SerializeField]
    private AudioSource audioSourc2;
    [SerializeField] private InputAction _action;
    [SerializeField]
    PlayerController playerController;
   [SerializeField]
    Animator animator;
    [SerializeField]
    Animator animator2;
    [SerializeField]
   private FloorMove floorMove;
    public static GameManager Instance;

    public delegate void PaudeHandller();
    public PaudeHandller PauseEvent;
    public delegate void UnPauseHandller();
    public PaudeHandller UnPauseEvent;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    public GameObject player;
    public GameObject savePoint;
    public MonoBehaviour targetScript = null;
    public GameObject door;
    public GameObject flag;
    public GameObject lever;
    public GameObject moveStage;

    [SerializeField]
     private SoundManager soundManager;
    public Sprite newleverSprite;
    private SpriteRenderer leverimage;

    public Sprite newFlagSprite;
    private SpriteRenderer flagimage;
    //public Sprite newSaveSprite;
    private Rigidbody2D otherRigidbody;
    private bool Nextbool = false;
    private bool Next2bool = false;
    private bool Nexttutobool = false;
    private bool isPause;
    private bool Switchnext = false;
    public bool PlayerStop = false;
    public BGMtype bgmtype;
    
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
        if (floorMove != null)
        {
            otherRigidbody = floorMove.rb;
        }
        if (lever != null)
        {
            leverimage = lever.GetComponent<SpriteRenderer>();
        }
       
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
        flagimage = savePoint.GetComponent<SpriteRenderer>();
        flagimage.sprite = newFlagSprite;
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
        otherRigidbody.velocity = Vector3.zero;
    }

    public void OpenDoor()
    {
        timer.audioSource.mute = true;
        soundManager.audioSouceBGM.loop = false;
        soundManager.PauseSE(timer.audioSource);
        soundManager.StopBGM();
        SoundManager.Instance.PauseSE(audioSource);
        SoundManager.Instance.PlayBGM(BGMtype.result);
        animator.SetTrigger("Slide");
        animator2.SetTrigger("NextSelect");
        Nextbool = true;
        PlayerStop = true;
        Invoke("SwitchNext", 2.0f);
    }
    public void GoolDoor()
    {
       
        //soundManager.audioSouceBGM.loop = false;
        timer.audioSource.mute = true;
        soundManager.StopBGM();
        SoundManager.Instance.PauseSE(audioSource);
        SoundManager.Instance.PlayBGM(BGMtype.result);
        animator.SetTrigger("Slide");
        animator2.SetTrigger("NextSelect");
        Next2bool = true;
        PlayerStop = true;
        Invoke("SwitchNext", 2.0f);
    }
    public void OpenDoor2()
    {
        timer.audioSource.mute = true;
        //soundManager.audioSouceBGM.loop = false;
        soundManager.PauseSE(timer.audioSource);
        soundManager.StopBGM();
        SoundManager.Instance.PauseSE(audioSource);
        SoundManager.Instance.PlayBGM(BGMtype.result);
        animator.SetTrigger("Slide");
        animator2.SetTrigger("NextSelect");
        Nexttutobool = true;
        PlayerStop = true;
        Invoke("SwitchNext", 2.0f);
    }

    public void ActivateLever()
    {
        if (targetScript != null)
        {
            leverimage.sprite = newleverSprite;
            targetScript.enabled = true;
        }
    }

   private void StartBGM()
    {
        SoundManager.Instance.PlayBGM(BGMtype.title);
            soundManager.audioSouceBGM.loop = true;

       
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
    private void SwitchNext()
    {
        Switchnext = true;
    }
  
    void Update()
    {
       if(Nextbool == true && Switchnext == true)
        {
            if(Input.GetKeyDown(KeyCode.M) || playerController.IsNextPressed)
            {
                SceneManager.LoadScene("SecondStage");
                soundManager.audioSouceBGM.loop = true;

            }
            if (Input.GetKeyDown(KeyCode.N) || playerController.IsTitlePressed)
            {
                SceneManager.LoadScene("Stageselection");

            }
        }
        if (Next2bool == true && Switchnext == true)
        {
            if (Input.GetKeyDown(KeyCode.M) || playerController.IsNextPressed)
            {
                SceneManager.LoadScene("Clear");
            }
            if (Input.GetKeyDown(KeyCode.N) || playerController.IsTitlePressed)
            {
                SceneManager.LoadScene("Stageselection");
            }
        }
        if (Nexttutobool == true && Switchnext == true)
        {
            if (Input.GetKeyDown(KeyCode.M) || playerController.IsNextPressed)
            {
                SceneManager.LoadScene("FirstStage");
                soundManager.audioSouceBGM.loop = true;

            }
            if (Input.GetKeyDown(KeyCode.N) || playerController.IsTitlePressed)
            {
                SceneManager.LoadScene("Stageselection");
            }
        }
    }

}

