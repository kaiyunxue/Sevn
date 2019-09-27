using UnityEngine;
using System.Collections;
using System.Threading;

public class NetService : MonoBehaviour
{
    public enum MSG_ID
    {
        MSG_ID_LOGIN = 1,
        MSG_ID_TICK = 2,
    };

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(Tick());
    }

    IEnumerator Tick()
    {
        WWWForm vMessageList = new WWWForm();
        vMessageList.AddField("iMessageID", (int)MSG_ID.MSG_ID_TICK);
        while(true)
        {
            yield return HttpTool.HttpPost("http://agame.sunz.me/api/post", vMessageList);
            yield return new WaitForSeconds((float)0.5);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public static IEnumerator SendMessage(MSG_ID eMessageID, WWWForm vMessageList)
    {
        vMessageList.AddField("iMessageID", (int)eMessageID);
        yield return HttpTool.HttpPost("http://agame.sunz.me/api/post", vMessageList);
    }
}
