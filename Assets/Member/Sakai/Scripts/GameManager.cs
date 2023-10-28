using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject player;
    public GameObject savePoint;
    public MonoBehaviour targetScript = null;
    public GameObject door;
    public GameObject flag;
    public GameObject lever;
    public GameObject moveStage;

    void Start()
    {
        if(savePoint == null)
        {
            savePoint = flag;
        }
        if (targetScript != null)
        {
            targetScript.enabled = false;
        }
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
        SceneManager.LoadScene("Clear");
    }

    public void ActivateLever()
    {
        if (targetScript != null)
        {
            targetScript.enabled = true;
        }
    }

   

    // 他のゲームロジックを追加することもできます

    // Update is called once per frame
    void Update()
    {
        // ゲーム全体のロジックをここに追加
    }
    }

