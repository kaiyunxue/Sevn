using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    public VideoPlayer vp;
    public GameObject videoPanel;
    public Button buttonSkip;
    void Awake()
    {
        vp.Stop();
        videoPanel.SetActive(false);

    }
    void Start()
    {
        EventComponent.RegistEvent(EVENTTYPE.Login, EVENTID.LoginSuccess, new UnityAction(OnLoginSuccess));
        buttonSkip.onClick.AddListener(delegate ()
        {
            TurnToLobby();
        });
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
        vp.loopPointReached += VideoPlayerEnd;
        vp.Play();
        videoPanel.SetActive(true);
    }

    void VideoPlayerEnd(UnityEngine.Video.VideoPlayer vp)
    {
        TurnToLobby();
    }

    void TurnToLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
