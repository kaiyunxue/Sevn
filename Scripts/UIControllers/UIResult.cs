using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIResult : MonoBehaviour
{
    public Button buttonToNextScene;
    public Button buttonToLobby;
    public Sprite spriteWin;
    public Sprite spriteFail;
    public Image spriteBackground;
    public void Init(bool isWin = true)
    {
        if (isWin)
        {
            string iCurrentLevelID = CacheService.Get("iCurrentLevelID");
            string uid = CacheService.Get("uid");
            spriteBackground.sprite = spriteWin;
            if (uid != null && iCurrentLevelID != null && GameManager.Instance.gamePlayMode.gameMode == GameMode.VSAI)
            {
                WWWForm form = new WWWForm();
	            form.AddField("uid", uid);
	            form.AddField("iCurrentLevelID", int.Parse(iCurrentLevelID));
	            form.AddField("bWin", 1);
                StartCoroutine(NetService.SendMessage(Message.MSG_ID.MSG_ID_RESULT, form));
            }
        }
        else
        {
            string iCurrentLevelID = CacheService.Get("iCurrentLevelID");
            string uid = CacheService.Get("uid");
            spriteBackground.sprite = spriteFail;
            if (uid != null && iCurrentLevelID != null && GameManager.Instance.gamePlayMode.gameMode == GameMode.VSAI)
            {
	            WWWForm form = new WWWForm();
	            form.AddField("uid", uid);
	            form.AddField("iCurrentLevelID", int.Parse(iCurrentLevelID));
	            form.AddField("bWin", 0);
	            StartCoroutine(NetService.SendMessage(Message.MSG_ID.MSG_ID_RESULT, form));
            }
        }
        buttonToNextScene.onClick.AddListener(new UnityAction(TurnToNextScene));
        buttonToLobby.onClick.AddListener(new UnityAction(TurnToLobby));
    }
    void TurnToLobby()
    {
        SceneManager.LoadSceneAsync("LobbyScene").allowSceneActivation = true;
    }
    void TurnToThisScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void TurnToNextScene()
    {
        int curLevel = int.Parse(CacheService.Get("iCurrentLevelID"));
        int nextLevel = curLevel;
        if (curLevel < 3)
        {
            nextLevel++;
        }
        CacheService.Set("playMode", "PVE");
        CacheService.Set("iCurrentLevelID", nextLevel.ToString());
        SceneManager.LoadSceneAsync("MainScene").allowSceneActivation = true;
    }
}
