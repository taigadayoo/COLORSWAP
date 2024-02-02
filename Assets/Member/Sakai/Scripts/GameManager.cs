using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Haptics;

public class DualShock4GamepadClear : DualShockGamepad, IDualShockHaptics, IDualMotorRumble, IHaptics, IEventPreProcessor
{
}

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
    public MonoBehaviour targetScript2 = null;
    public GameObject door;
    public GameObject flag;
    public GameObject lever;
    public GameObject lever2;
    public GameObject moveStage;
   

    [SerializeField]
     private SoundManager soundManager;
    public Sprite newleverSprite;
    private SpriteRenderer leverimage;
    private SpriteRenderer leverimage2;

    public Sprite newFlagSprite;
    private SpriteRenderer flagimage;
    //public Sprite newSaveSprite;
    private Rigidbody2D otherRigidbody;
    private bool Nextbool = false;
    private bool Next2bool = false;
    private bool Next3bool = false;
    private bool Next4bool = false;
    private bool Nexttutobool = false;
    private bool isPause;
    private bool Switchnext = false;
    private bool PadClearSwitch = false;
    public bool PlayerStop = false;
    public BGMtype bgmtype;
    [SerializeField] public string sceneName1;
    [SerializeField] public string sceneName2;
    [SerializeField] public string sceneName3;
    [SerializeField] public string sceneName4;
    [SerializeField] public string sceneNameClear;
    [SerializeField] public string sceneselection;
    [SerializeField] public Color fadeColor;
    [SerializeField] public float fadeSpeed;
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
        if (lever2 != null)
        {
            leverimage2 = lever2.GetComponent<SpriteRenderer>();
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
        if (targetScript2 != null)
        {
            targetScript2.enabled = false;
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
        
        soundManager.PauseSE(timer.audioSource);
        soundManager.StopBGM();
        SoundManager.Instance.PauseSE(audioSource);
        StartClearBGM();
        InvokeRepeating("PadClear", 0f, 0.1f);
        Invoke("animatorBack", 0.4f);
        Invoke("animatorNext", 0.6f);
        Nextbool = true;
        PlayerStop = true;
        Invoke("SwitchNext", 2.0f);
    }
    public void GoolDoor()
    {
        timer.audioSource.mute = true;
        soundManager.PauseSE(timer.audioSource);
        soundManager.StopBGM();
        SoundManager.Instance.PauseSE(audioSource);
        StartClearBGM();
        InvokeRepeating("PadClear", 0f, 0.1f);
        Invoke("animatorBack", 0.4f);
        Invoke("animatorNext", 0.6f);
        Next2bool = true;
        PlayerStop = true;
        Invoke("SwitchNext", 2.0f);
    }
    public void OpenDoor2()
    {
        timer.audioSource.mute = true;
        soundManager.PauseSE(timer.audioSource);
        soundManager.StopBGM();
        SoundManager.Instance.PauseSE(audioSource);
        StartClearBGM();
        InvokeRepeating("PadClear", 0f, 0.1f);
        Invoke("animatorBack", 0.4f);
        Invoke("animatorNext", 0.6f);
        Nexttutobool = true;
        PlayerStop = true;
        Invoke("SwitchNext", 2.0f);
    }
    public void OpenDoor3()
    {
        timer.audioSource.mute = true;
        soundManager.PauseSE(timer.audioSource);
        soundManager.StopBGM();
        SoundManager.Instance.PauseSE(audioSource);
        StartClearBGM();
        InvokeRepeating("PadClear", 0f, 0.1f);
        Invoke("animatorBack", 0.4f);
        Invoke("animatorNext", 0.6f);
        Next3bool = true;
        PlayerStop = true;
        Invoke("SwitchNext", 2.0f);
    }
    public void OpenDoor4()
    {
        timer.audioSource.mute = true;
        soundManager.PauseSE(timer.audioSource);
        soundManager.StopBGM();
        SoundManager.Instance.PauseSE(audioSource);
        StartClearBGM();
        InvokeRepeating("PadClear", 0f, 0.1f);
        Invoke("animatorBack", 0.4f);
        Invoke("animatorNext", 0.6f);
        Next4bool = true;
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
    public void ActivateLever2()
    {
        if (targetScript != null)
        {
            leverimage2.sprite = newleverSprite;
            targetScript2.enabled = true;
        }
    }

    private void StartBGM()
    {
        SoundManager.Instance.PlayBGM(BGMtype.title);
    }
    private void StartClearBGM()
    {
        SoundManager.Instance.PlayBGM(BGMtype.result);

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
  private void animatorBack()
    {
        animator.SetTrigger("Slide");
    }
    private void animatorNext()
    {
        animator2.SetTrigger("NextSelect");
    }
    private void PadClear()
    {
        string[] joystickNamesClear = Input.GetJoystickNames();
        if (joystickNamesClear.Length > 0 && !string.IsNullOrEmpty(joystickNamesClear[0]))
        {
            if (!PadClearSwitch)
            {
                DualShock4GamepadClear.current.SetLightBarColor(Color.white);
                PadClearSwitch = true;
            }
            else if (PadClearSwitch)
            {
                DualShock4GamepadClear.current.SetLightBarColor(Color.blue);
                PadClearSwitch = false;
            }
        }
    }
    void Update()
    {
       if(Nextbool == true && Switchnext == true)
        {
            if(Input.GetKeyDown(KeyCode.M) || playerController.IsNextPressed)
            {
                Initiate.Fade(sceneName2, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;

            }
            if (Input.GetKeyDown(KeyCode.N) || playerController.IsTitlePressed)
            {
                Initiate.Fade(sceneselection, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;

            }
        }
        if (Next2bool == true && Switchnext == true)
        {
            if (Input.GetKeyDown(KeyCode.M) || playerController.IsNextPressed)
            {
                Initiate.Fade(sceneName3, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;
            }
            if (Input.GetKeyDown(KeyCode.N) || playerController.IsTitlePressed)
            {
                
                Initiate.Fade(sceneselection, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;
                
            }
        }
        if (Next3bool == true && Switchnext == true)
        {
            if (Input.GetKeyDown(KeyCode.M) || playerController.IsNextPressed)
            {
                Initiate.Fade(sceneName4, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;
            }
            if (Input.GetKeyDown(KeyCode.N) || playerController.IsTitlePressed)
            {
                Initiate.Fade(sceneselection, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;
            }
        }
        if (Next4bool == true && Switchnext == true)
        {
            if (Input.GetKeyDown(KeyCode.M) || playerController.IsNextPressed)
            {
                Initiate.Fade(sceneNameClear, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;
            }
            if (Input.GetKeyDown(KeyCode.N) || playerController.IsTitlePressed)
            {
                Initiate.Fade(sceneselection, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;
            }
        }
        if (Nexttutobool == true && Switchnext == true)
        {
            if (Input.GetKeyDown(KeyCode.M) || playerController.IsNextPressed)
            {
                Initiate.Fade(sceneName1, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;
            }
            if (Input.GetKeyDown(KeyCode.N) || playerController.IsTitlePressed)
            {
                Initiate.Fade(sceneselection, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;
            }
        }
    }

}

