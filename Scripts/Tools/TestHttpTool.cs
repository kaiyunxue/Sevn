﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TestHttpTool : MonoBehaviour
{
    private string url = "http://127.0.0.1:8000/api/status";
    private string postUrl = "http://127.0.0.1:8000/api/post";
    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log("test");
        //StartCoroutine(HttpTool.HttpGet(url));
        //WWWForm form = new WWWForm();
        //form.AddField("name", "agame");
        //StartCoroutine(HttpTool.HttpPost(postUrl, form));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        string username = GameObject.Find("InputField").GetComponent<InputField>().text;
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        StartCoroutine(HttpTool.HttpPost(postUrl, form));

        StartCoroutine(NetService.SendMessage(NetService.MSG_ID.MSG_ID_LOGIN, form));
    }
}