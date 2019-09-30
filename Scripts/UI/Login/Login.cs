using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    public VideoPlayer vp;
    public GameObject videoTexture;
    void Awake()
    {
        vp.Stop();
        videoTexture.SetActive(false);
    }
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
        vp.loopPointReached += VideoPlayerEnd;
        videoTexture.SetActive(true);
        vp.Play();
    }

    void VideoPlayerEnd(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene("LobbyScene");
        //videoTexture.SetActive(false);
    }
}
