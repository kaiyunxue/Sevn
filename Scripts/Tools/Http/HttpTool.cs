using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ResponseData
{
    public int iMessageID;
    public int iErrCode;
    public string data;
}

public class HttpTool : MonoBehaviour
{


    public static IEnumerator HttpGet(string url)
    {
        Debug.Log("enter HttpGet");
        var request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
    }

    public static IEnumerator HttpPost(string url, WWWForm postData)
    {
        Debug.Log("enter HttpPost");
        var request = UnityWebRequest.Post(url, postData);
        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            ResponseData responseData = JsonUtility.FromJson<ResponseData>(request.downloadHandler.text);
            Debug.Log(request.downloadHandler.text);
            switch(responseData.iMessageID)
            {
                case (int)Message.MSG_ID.MSG_ID_LOGIN:
                    Message.User user = new Message.User();
                    Debug.Log(responseData.data);
                    user = JsonUtility.FromJson<Message.User>(responseData.data);
                    CacheTool.Set("uid", user.uid);
                    CacheTool.Set("iGameLevel", user.iGameLevel.ToString());
                    Debug.Log("now insert userinfo, uid:" + user.uid + "iGameLevel:" + user.iGameLevel);
                    SceneManager.LoadScene("LobbyScene");
                    break;
                default:
                    break;
            }
        }
    }
}
