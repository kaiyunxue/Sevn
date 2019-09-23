using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SlideScript : MonoBehaviour
{
    private EventComponent eventComponent;
    // Start is called before the first frame update
    void Start()
    {
        eventComponent = EventComponent.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector2 touchFirst = Vector2.zero; //手指开始按下的位置
    private Vector2 touchSecond = Vector2.zero; //手指结束按下的位置
    public float SlidingDistance = 80f;

    void OnGUI()
    {
        if (Event.current.type == EventType.MouseDown)
        //判断当前手指是按下事件 
        {
            touchFirst = Event.current.mousePosition;//记录开始按下的位置
        }

        if (Event.current.type == EventType.MouseUp)
        {//滑动结束
            touchSecond = Event.current.mousePosition;
            Vector2 slideDirection = touchFirst - touchSecond;
            float x = slideDirection.x;
            float y = slideDirection.y;

            if (y + SlidingDistance < x && y > -x - SlidingDistance)
            {
                Debug.Log("right");
                eventComponent.PostEvent(EventDefine.OnSlideRight);
            }
            else if (y > x + SlidingDistance && y < -x - SlidingDistance)
            {
                Debug.Log("left");
                eventComponent.PostEvent(EventDefine.OnSlideLeft);
            }
            else if (y > x + SlidingDistance && y - SlidingDistance > -x)
            {
                Debug.Log("up");
                eventComponent.PostEvent(EventDefine.OnSlideUp);
            }
            else if (y + SlidingDistance < x && y < -x - SlidingDistance)
            {
                Debug.Log("down");
                eventComponent.PostEvent(EventDefine.OnSlideDown);
            }
            touchFirst = Vector2.zero;
        }
    }
}
