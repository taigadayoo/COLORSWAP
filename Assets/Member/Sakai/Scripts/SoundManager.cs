using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    public AudioSource audioSouceBGM;
    public static SoundManager Instance;//シングルトン、どこからでも呼べるようにする

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
            DontDestroyOnLoad(gameObject);// 壊れなくする
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
    
    public void PlayBGM(BGMtype bgmtype)
    {
        audioSouceBGM.clip = BGMList[(int)bgmtype]; //clipの中にListの中の曲入れる
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
            audioSouceBGM.Stop();//止める
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
public enum BGMtype　//列挙型　名前つける
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
