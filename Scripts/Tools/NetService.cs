using UnityEngine;
using System.Collections;
using System.Threading;

public class NetService : MonoBehaviour
{
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //StartCoroutine(Tick());
    }

    IEnumerator Tick()
    {
        WWWForm vMessageList = new WWWForm();
        vMessageList.AddField("iMessageID", (int)Message.MSG_ID.MSG_ID_TICK);
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

    public static IEnumerator SendMessage(Message.MSG_ID eMessageID, WWWForm vMessageList)
    {
        vMessageList.AddField("iMessageID", (int)eMessageID);
        yield return HttpTool.HttpPost("http://agame.sunz.me/api/post", vMessageList);
    }
}
