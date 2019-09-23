using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class test : MonoBehaviour
{
    private EventComponent eventComponent;
    // Start is called before the first frame update
    void Start()
    {
        eventComponent = EventComponent.instance;
        eventComponent.RegistEvent(EventDefine.OnSlideLeft, new UnityAction(TestFunc));
    }

    void TestFunc()
    {
        Debug.Log("TestFunc()");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
