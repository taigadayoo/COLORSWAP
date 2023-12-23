using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    public AudioSource audioSouceBGM;
    public static SoundManager Instance;//�V���O���g���A�ǂ�����ł��Ăׂ�悤�ɂ���

    [SerializeField]
   private List<AudioClip> BGMList = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> SEList = new List<AudioClip>();

    int test;
    
    private void Awake()
    {

        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);// ���Ȃ�����
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
    
    public void PlayBGM(BGMtype bgmtype)
    {
        audioSouceBGM.clip = BGMList[(int)bgmtype]; //clip�̒���List�̒��̋ȓ����
        audioSouceBGM.Play();
        if(bgmtype == BGMtype.title)
        {
            audioSouceBGM.loop = true;
        }
        else if(bgmtype == BGMtype.result)
        {
            audioSouceBGM.loop = false;
        }

    }
    public void StopBGM()
    {
        if (audioSouceBGM != null)
        {
            audioSouceBGM.Stop();//�~�߂�
        }
    }
    public void PauseBGM()
    {
        audioSouceBGM.Pause();
    }
    public void StartSE(SEtype sEtype , AudioSource audio)
    {
        audio.clip = SEList[(int)sEtype];    
        audio.Play();
      
    }
    public void PauseSE( AudioSource audio)
    {
        audio.Pause();
    }
}
public enum BGMtype�@//�񋓌^�@���O����
{
    title = 0,
    game,
    result       
}
public enum SEtype
{
    ChangeTimerPon,
    ChangeTimerTin,
    Dead,
    Reverse
}
