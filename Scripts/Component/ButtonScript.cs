using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    private float Ping;
    private bool IsStart = false;
    private float LastTime = 0;
    public UnityEvent onSlightClick { get; set; }
    public UnityEvent onLongPress { get; set; }
    public UnityEvent onMouseUp { get; set; }
    public UnityEvent onMouseDown { get; set; }
    public UnityEvent onDrag { get; set; }

    void Awake()
    {
        onSlightClick = onSlightClick == null ? new UnityEvent() : onSlightClick;
        onLongPress = onLongPress == null ? new UnityEvent() : onLongPress;
        onMouseUp = onMouseUp == null ? new UnityEvent() : onMouseUp;
        onMouseDown = onMouseDown == null ? new UnityEvent() : onMouseDown;
        onDrag = onDrag == null ? new UnityEvent() : onDrag;
    }

    void Update()
    {
        if (IsStart && Ping > 0 && LastTime > 0 && Time.time - LastTime > Ping)
        {
            if (onDrag != null)
            {
                onDrag.Invoke();
            }
        }
    }

    public void BeginPress(bool bStart)
    {
        IsStart = bStart;
        if (IsStart)
        {
            LastTime = Time.time;
            if (onMouseDown != null)
            {
                onMouseDown.Invoke();
            }
            return;
        }

        if (Ping > 0 && LastTime > 0 && Time.time - LastTime > Ping)
        {
            if (onLongPress != null)
            {
                onLongPress.Invoke();
            }
        }
        else if (LastTime != 0)
        {
            if (onSlightClick != null)
            {
                onSlightClick.Invoke();
            }
        }
        if (onMouseUp != null)
        {
            onMouseUp.Invoke();
        }
        LastTime = 0;
    }
}
