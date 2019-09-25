using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

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
            Debug.Log(request.downloadHandler.text);
        }
    }
}
