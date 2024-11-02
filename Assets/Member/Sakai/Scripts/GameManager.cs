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

// ゲーム全体の管理を行うGameManagerクラス
public class GameManager : MonoBehaviour
{
    // タイマー、オーディオ、プレイヤー、アニメーター、移動床などの各種コンポーネントをシリアライズ
    [SerializeField]
    public Timer timer; // ゲームのタイマー
    [SerializeField]
    private AudioSource audioSourc2; // オーディオソース（未使用）
    [SerializeField] private InputAction _action; // 入力アクション
    [SerializeField]
    PlayerController playerController; // プレイヤーコントローラー
    [SerializeField]
    Animator animator; // アニメーター1
    [SerializeField]
    Animator animator2; // アニメーター2
    [SerializeField]
    private FloorMove floorMove; // 移動床の管理

    public static GameManager Instance; // シングルトンインスタンス

    // イベントハンドラーのデリゲート定義
    public delegate void PaudeHandller();
    public PaudeHandller PauseEvent;
    public delegate void UnPauseHandller();
    public PaudeHandller UnPauseEvent;

    // その他のオブジェクトやコンポーネント
    [SerializeField]
    private AudioSource audioSource; // メインオーディオソース
    [SerializeField]
    public GameObject player; // プレイヤーオブジェクト
    public GameObject savePoint; // セーブポイント
    public MonoBehaviour targetScript = null; // ターゲットスクリプト
    public MonoBehaviour targetScript2 = null; // ターゲットスクリプト2
    public GameObject door; // ドアオブジェクト
    public GameObject flag; // フラグオブジェクト
    public GameObject lever; // レバーオブジェクト
    public GameObject lever2; // もう一つのレバーオブジェクト
    public GameObject moveStage; // 移動ステージ

    [SerializeField]
    private SoundManager soundManager; // サウンドマネージャー
    public Sprite newleverSprite; // 新しいレバーのスプライト
    private SpriteRenderer leverimage; // レバーのスプライトレンダラー
    private SpriteRenderer leverimage2; // もう一つのレバーのスプライトレンダラー

    public Sprite newFlagSprite; // 新しいフラグのスプライト
    private SpriteRenderer flagimage; // フラグのスプライトレンダラー
    private Rigidbody2D otherRigidbody; // 他のオブジェクトのRigidbody2D

    // 状態管理用のブール変数
    private bool Nextbool = false;
    private bool Next2bool = false;
    private bool Next3bool = false;
    private bool Next4bool = false;
    private bool Nexttutobool = false;
    private bool isPause; // 一時停止状態
    private bool Switchnext = false; // 次のステージへのスイッチ
    private bool PadClearSwitch = false; // パッドクリアスイッチ
    public bool PlayerStop = false; // プレイヤー停止フラグ

    // BGMタイプ管理用の変数
    public BGMtype bgmtype;
    
    // シーン名の設定
    [SerializeField] public string sceneName1;
    [SerializeField] public string sceneName2;
    [SerializeField] public string sceneName3;
    [SerializeField] public string sceneName4;
    [SerializeField] public string sceneNameClear;
    [SerializeField] public string sceneselection;
    [SerializeField] public string title;

    // フェードエフェクト用の色と速度
    [SerializeField] public Color fadeColor;
    [SerializeField] public float fadeSpeed;

    private void Awake()
    {
        // シングルトンパターンの実装
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 既存のインスタンスがある場合はこのオブジェクトを破棄
        }
    }

    void Start()
    {
        // 初期設定
        if (floorMove != null)
        {
            otherRigidbody = floorMove.rb; // 移動床のRigidbody2Dを取得
        }
        if (lever != null)
        {
            leverimage = lever.GetComponent<SpriteRenderer>(); // レバーのスプライトレンダラーを取得
        }
        if (lever2 != null)
        {
            leverimage2 = lever2.GetComponent<SpriteRenderer>(); // もう一つのレバーのスプライトレンダラーを取得
        }

        // イベントの購読
        UnPauseEvent += StartBGM;
        UnPauseEvent += ChangePause;
        PauseEvent += PauseBGM;
        PauseEvent += ChangePause;

        // セーブポイントの初期設定
        if (savePoint == null)
        {
            savePoint = flag; // フラグがセーブポイントの場合
        }
        if (targetScript != null)
        {
            targetScript.enabled = false; // ターゲットスクリプトを無効にする
        }
        if (targetScript2 != null)
        {
            targetScript2.enabled = false; // ターゲットスクリプト2を無効にする
        }
        StartBGM(); // BGMを開始
    }

    // プレイヤーをセーブポイントにリスポーンさせるメソッド
    public void RespawnPlayer()
    {
        if (player != null && savePoint != null)
        {
            player.transform.position = savePoint.transform.position; // プレイヤーの位置をセーブポイントに設定
        }
    }

    // ゲームクリアシーンをロードするメソッド
    public void LoadClearScene()
    {
        SceneManager.LoadScene("Clear"); // クリアシーンをロード
    }

    // 新しいセーブポイントを設定するメソッド
    public void SetSavePoint(GameObject newFlag)
    {
        savePoint = newFlag; // セーブポイントを更新
        flagimage = savePoint.GetComponent<SpriteRenderer>(); // フラグのスプライトレンダラーを取得
        flagimage.sprite = newFlagSprite; // 新しいスプライトに更新
    }

    // ターゲットスクリプトを有効にするメソッド
    public void EnableTargetScript()
    {
        if (targetScript != null)
        {
            targetScript.enabled = true; // ターゲットスクリプトを有効にする
        }
    }

    // プレイヤーを移動ステージに親子関係を設定するメソッド
    public void ParentPlayerToMoveStage(Transform stage)
    {
        player.transform.SetParent(stage); // プレイヤーを指定されたステージの子にする
    }

    // プレイヤーの親子関係を解除し、速度をリセットするメソッド
    public void UnparentPlayerFromMoveStage()
    {
        player.transform.SetParent(null); // プレイヤーの親子関係を解除
        otherRigidbody.velocity = Vector3.zero; // Rigidbodyの速度をリセット
    }

    // ドアを開けるメソッド
    public void OpenDoor()
    {
        timer.audioSource.mute = true; // タイマーのオーディオをミュート
        soundManager.PauseSE(timer.audioSource); // タイマーのSEを一時停止
        soundManager.StopBGM(); // BGMを停止
        SoundManager.Instance.PauseSE(audioSource); // メインオーディオのSEを一時停止
        StartClearBGM(); // クリアBGMを開始
        InvokeRepeating("PadClear", 0f, 0.1f); // パッドクリアを一定間隔で呼び出し
        Invoke("animatorBack", 0.8f); // アニメーターをバックさせる
        Invoke("animatorNext", 1.0f); // 次のアニメーションを呼び出す
        Nextbool = true; // 次の遷移フラグをセット
        PlayerStop = true; // プレイヤー停止フラグをセット
        Invoke("SwitchNext", 2.0f); // 次の遷移を2秒後に実行
    }
    public void GoolDoor()
    {
        timer.audioSource.mute = true;
        soundManager.PauseSE(timer.audioSource);
        soundManager.StopBGM();
        SoundManager.Instance.PauseSE(audioSource);
        StartClearBGM();
        InvokeRepeating("PadClear", 0f, 0.1f);
        Invoke("animatorBack", 0.8f);
        Invoke("animatorNext", 1.0f);
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
        Invoke("animatorBack", 0.8f);
        Invoke("animatorNext", 1.0f);
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
        Invoke("animatorBack", 0.8f);
        Invoke("animatorNext", 1.0f);
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
        Invoke("animatorBack", 0.8f);
        Invoke("animatorNext", 1.0f);
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
        soundManager.audioSouceBGM.volume = 2.0f;
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
        if(playerController.IsTitlePressed)
        {
            Initiate.Fade(title, fadeColor, fadeSpeed);
            soundManager.audioSouceBGM.clip = null;
            soundManager.PauseSE(timer.audioSource);
            SoundManager.Instance.PauseSE(audioSource);
            timer.audioSource.mute = true;
        }
       if(Nextbool == true && Switchnext == true)
        {
            if(Input.GetKeyDown(KeyCode.M) || playerController.IsSelectPressed)
            {
                Initiate.Fade(sceneselection, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;

            }
            if (Input.GetKeyDown(KeyCode.N) || playerController.IsNextPressed)
            {
                Initiate.Fade(sceneName2, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;

            }
        }
        if (Next2bool == true && Switchnext == true)
        {
            if (Input.GetKeyDown(KeyCode.M) || playerController.IsSelectPressed)
            {
                Initiate.Fade(sceneselection, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;
            }
            if (Input.GetKeyDown(KeyCode.N) || playerController.IsNextPressed)
            {

                Initiate.Fade(sceneName3, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;
                
            }
        }
        if (Next3bool == true && Switchnext == true)
        {
            if (Input.GetKeyDown(KeyCode.M) || playerController.IsSelectPressed)
            {
                Initiate.Fade(sceneselection, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;
            }
            if (Input.GetKeyDown(KeyCode.N) || playerController.IsNextPressed)
            {
                Initiate.Fade(sceneName4, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;
            }
        }
        if (Next4bool == true && Switchnext == true)
        {
            if (Input.GetKeyDown(KeyCode.M) || playerController.IsSelectPressed)
            {
                Initiate.Fade(sceneselection, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;
            }
            if (Input.GetKeyDown(KeyCode.N) || playerController.IsNextPressed)
            {
                Initiate.Fade(sceneNameClear, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;
            }
        }
        if (Nexttutobool == true && Switchnext == true)
        {
            if (Input.GetKeyDown(KeyCode.M) || playerController.IsSelectPressed)
            {
                Initiate.Fade(sceneselection, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;
            }
            if (Input.GetKeyDown(KeyCode.N) || playerController.IsNextPressed)
            {
                Initiate.Fade(sceneName1, fadeColor, fadeSpeed);
                soundManager.audioSouceBGM.clip = null;
            }
        }
    }

}

