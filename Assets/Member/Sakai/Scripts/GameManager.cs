using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
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
        soundManager.StopBGM();
        SoundManager.Instance.PauseSE(audioSource);
        animator.SetTrigger("Slide");
        animator2.SetTrigger("NextSelect");

    }
    public void GoolDoor()
    {
        SceneManager.LoadScene("Clear");
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

    private void Update()
    {
       
    }

}

