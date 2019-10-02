using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Login : MonoBehaviour
{
    public VideoClip videoClip;
    public VideoComponent videoComponent;
    AsyncOperation loadSceneAsync;
    void Start()
    {
        EventComponent.RegistEvent(EVENTTYPE.Login, EVENTID.LoginSuccess, new UnityAction(OnLoginSuccess));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        string uid = CacheService.Get("uid");
        WWWForm form = new WWWForm();
        if (uid != null)
        {
            form.AddField("uid", uid);
        }
        StartCoroutine(NetService.SendMessage(Message.MSG_ID.MSG_ID_LOGIN, form));
    }

    void OnLoginSuccess()
    {
        if (videoComponent.gameObject.activeSelf) return;
        videoComponent.gameObject.SetActive(true);
        videoComponent.PlayVideo(videoClip, new UnityAction(OnVideoPlayerEnd));
        loadSceneAsync = SceneManager.LoadSceneAsync("LobbyScene");
        loadSceneAsync.allowSceneActivation = false;
    }

    void OnVideoPlayerEnd()
    {
        TurnToLobby();
    }

    void TurnToLobby()
    {
        loadSceneAsync.allowSceneActivation = true;
    }
}
