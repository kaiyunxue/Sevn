﻿using System.Collections;
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
    private int nextLevel;
    public void Init(bool isWin = true)
    {
        if (isWin)
        {
            Debug.Log("Win");
            spriteBackground.sprite = spriteWin;
            int curLevel = int.Parse(CacheService.Get("iCurrentLevelID"));
            nextLevel = curLevel;
            if (curLevel < 3)
            {
                nextLevel++;
            }

            string iCurrentLevelID = CacheService.Get("iCurrentLevelID");
            string uid = CacheService.Get("uid");
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
            spriteBackground.sprite = spriteFail;
            int curLevel = int.Parse(CacheService.Get("iCurrentLevelID"));
            nextLevel = curLevel;

            string iCurrentLevelID = CacheService.Get("iCurrentLevelID");
            string uid = CacheService.Get("uid");
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
        CacheService.Set("playMode", "PVE");
        CacheService.Set("iCurrentLevelID", nextLevel.ToString());
        SceneManager.LoadSceneAsync("MainScene").allowSceneActivation = true;
    }
}
