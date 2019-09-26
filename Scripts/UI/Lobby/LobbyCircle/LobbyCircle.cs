using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LobbyCircle : MonoBehaviour
{
    [SerializeField]
    private float spinAngleMin;
    [SerializeField]
    private float spinAngleMax;
    [SerializeField]
    private float spinSpeed;
    [SerializeField]
    private float bgAngle;
    [SerializeField]
    private int fanNum;
    [SerializeField]
    FanStatus[] fanStatus;
    [SerializeField]
    private float selectAngle;
    [SerializeField]
    private ButtonScript[] buttonDrag;
    [SerializeField]
    private GameObject fanPanel;
    [SerializeField]
    private ButtonScript buttonCircle;
    [SerializeField]
    private bool isReverse;

    private int selectIdx;
    private float fanAngle;
    private Vector3 bgMousePos;
    private Vector3 gameObjPos;

    void Start()
    {
        if (bgAngle > spinAngleMax || bgAngle < spinAngleMin)
        {
            bgAngle = spinAngleMin;
        }
        fanAngle = 360 / fanNum;
        fanPanel.transform.eulerAngles = new Vector3(0, 0, bgAngle);

        int curIdx = GetCurActiveIdx();
        for (int i = 0; i < fanNum; ++i)
        {
            if (i == curIdx)
            {
                fanStatus[i].Activate();
                selectIdx = i;
            }
            else
            {
                fanStatus[i].Deactivate();
            }
        }

        foreach (var button in buttonDrag)
        {
            button.onMouseDown.AddListener(delegate ()
            {
                OnMouseDown();
            });
            button.onDrag.AddListener(delegate ()
            {
                OnDrag();
            });
        }
    }

    void OnMouseDown()
    {
        gameObjPos = fanPanel.transform.position;
        bgMousePos = Input.mousePosition;
        bgAngle = GetCurAngle();
    }

    void OnDrag()
    {
        Vector3 curMousePos = Input.mousePosition;
        int dirFactor = (curMousePos - gameObjPos).x > 0 ? 1 : -1;
        float y = (curMousePos - bgMousePos).y * spinSpeed * dirFactor;
        float targetAngle = bgAngle + y;
        if (targetAngle > spinAngleMax)
        {
            targetAngle = spinAngleMax;
        }
        if (targetAngle < spinAngleMin)
        {
            targetAngle = spinAngleMin;
        }
        fanPanel.transform.eulerAngles = new Vector3(0, 0, targetAngle);

        int curIdx = GetCurActiveIdx();
        for (int i = 0; i < fanNum; ++i)
        {
            if (i == curIdx)
            {
                fanStatus[i].Activate();
                selectIdx = i;
            }
            else
            {
                fanStatus[i].Deactivate();
            }
        }

        bgMousePos = curMousePos;
        bgAngle = targetAngle;
    }

    private int GetCurActiveIdx()
    {
        float curAngle = GetCurAngle();

        if (isReverse)
        {
            for (int i = 0; i < fanNum; ++i)
            {
                if (curAngle + spinAngleMax - fanAngle * (i + 1) <= selectAngle && selectAngle < curAngle + spinAngleMax - fanAngle * i)
                {
                    return fanNum - i - 1;
                }
            }
        }
        else
        {
            for (int i = 0; i < fanNum; ++i)
            {
                if (-curAngle + spinAngleMin + fanAngle * i <= selectAngle && selectAngle < -curAngle + spinAngleMin + fanAngle * (i + 1))
                {
                    return i;
                }
            }
        }
        return -1;
    }

    private float GetCurAngle()
    {
        float curAngle = fanPanel.transform.eulerAngles.z;
        if(curAngle<0)
        {
            curAngle += 360;
        }
        return curAngle;
    }

    public int GetSelectIdx()
    {
        return selectIdx;
    }

    public void ShowFanPanel()
    {
        fanPanel.SetActive(true);
    }

    public void HideFanPanel()
    {
        fanPanel.SetActive(false);
    }

    public void SetClickEvent(UnityAction action)
    {
        buttonCircle.onSlightClick.AddListener(action);
    }
}
