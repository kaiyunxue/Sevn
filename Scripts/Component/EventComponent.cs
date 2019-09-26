using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EventComponent : MonoBehaviour
{
    public static EventComponent instance;
    private static MultiDictionary<EVENTTYPE, EVENTID, UnityEvent> dic;
    void Awake()
    {
        instance = this;
        dic = new MultiDictionary<EVENTTYPE, EVENTID, UnityEvent>();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static void RegistEvent(EVENTTYPE eventType, EVENTID eventID, UnityAction action)
    {
        if (dic.Get(eventType, eventID) == default(UnityEvent)) 
        {
            dic.Set(eventType, eventID, new UnityEvent());
        }
        dic.Get(eventType, eventID).AddListener(action);
    }

    public static void PostEvent(EVENTTYPE eventType, EVENTID eventID)
    {
        var unityEvent = dic.Get(eventType, eventID);
        if (unityEvent != default(UnityEvent))
        {
            unityEvent.Invoke();
        }
        else
        {
            Debug.Log("Function is not exist!");
        }
    }
}
