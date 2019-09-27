using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using System;

public class TestHttpTool : MonoBehaviour
{
    private string url = "http://agame.sunz.me/api/status";
    private string postUrl = "http://agame.sunz.me/api/post";

    // Start is called before the first frame update
    void Awake()
    {
       
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
}
