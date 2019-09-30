using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIResult : MonoBehaviour
{
    public string nextSceneName;
    public string lobbySceneName;
    public Text text;
    public Button button1;
    public Button button2;
    public void Init(bool isWin = true)
    {
        if (isWin)
        {
            string iCurrentLevelID = CacheService.Get("iCurrentLevelID");
            string uid = CacheService.Get("uid");
            if (uid != null && iCurrentLevelID != null && GameManager.Instance.gamePlayMode.gameMode == GameMode.VSAI)
            {
                WWWForm form = new WWWForm();
                form.AddField("uid", uid);
                form.AddField("iCurrentLevelID", int.Parse(iCurrentLevelID));
                form.AddField("bWin", 1);
                NetService.SendMessage(Message.MSG_ID.MSG_ID_RESULT, form);
            }

            text.text = "胜利";
            Debug.Log("胜利");
            button1.onClick.AddListener(new UnityAction(TurnToNextScene));
            button2.onClick.AddListener(new UnityAction(TurnToLobby));
        }
        else
        {
            string iCurrentLevelID = CacheService.Get("iCurrentLevelID");
            string uid = CacheService.Get("uid");
            if (uid != null && iCurrentLevelID != null && GameManager.Instance.gamePlayMode.gameMode == GameMode.VSAI)
            {
                WWWForm form = new WWWForm();
                form.AddField("uid", uid);
                form.AddField("iCurrentLevelID", int.Parse(iCurrentLevelID));
                form.AddField("bWin", 0);
                NetService.SendMessage(Message.MSG_ID.MSG_ID_RESULT, form);
            }

            text.text = "输了彩笔";
            Debug.Log("输了彩笔");
            button1.onClick.AddListener(new UnityAction(TurnToThisScene));
            button2.onClick.AddListener(new UnityAction(TurnToLobby));
        }
    }
    void TurnToLobby()
    {
        SceneManager.LoadScene(lobbySceneName);
    }
    void TurnToThisScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void TurnToNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
