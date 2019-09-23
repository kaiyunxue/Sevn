using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventComponent : MonoBehaviour
{
    public static EventComponent instance;
    public Dictionary<EventDefine, UnityEvent> dic;
    void Awake()
    {
        instance = this;
        dic = new Dictionary<EventDefine, UnityEvent>();
    }

    public void RegistEvent(EventDefine eventDefine,UnityAction action)
    {
        if (!dic.ContainsKey(eventDefine))
        {
            dic.Add(eventDefine, new UnityEvent());
        }
        dic[eventDefine].AddListener(action);
    }

    public void PostEvent(EventDefine eventDefine)
    {
        if (dic.ContainsKey(eventDefine))
        {
            dic[eventDefine].Invoke();
        }
        else
        {
            Debug.Log("Function is not exist!");
        }
    }
}
