using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//工具类 强依赖GameManager
public class TurnAtuoTimer : MonoBehaviour
{
    [HideInInspector] public UnityEvent WhenWaitingTimeOver;
    [HideInInspector] public UnityEvent WhenSelectingTimeOver;
    [SerializeField] private GameTurnStatus gameTurnStatus = GameTurnStatus.Sleeping;
    public float selectTime;
    public float waitingTime;

    public GameTurnStatus GameTurnStatus
    {
        get
        {
            return gameTurnStatus;
        }
        set
        {
            gameTurnStatus = value;
            switch (value)
            {
                case GameTurnStatus.DropingMore:
                    selectTime = GameManager.Instance.gamePlayMode.selectLimitTime;
                    waitingTime = -1;
                    break;
                case GameTurnStatus.WaitingForDrop:
                    waitingTime = GameManager.Instance.gamePlayMode.waitingLimitTime;
                    selectTime = -1;
                    break;
            }
        }
    } 
    public float GetTurnTimeRemainPercent()
    {
        switch (gameTurnStatus)
        {
            case GameTurnStatus.DropingMore:
                return selectTime / GameManager.Instance.gamePlayMode.selectLimitTime; ;
            case GameTurnStatus.WaitingForDrop:
                return waitingTime / GameManager.Instance.gamePlayMode.waitingLimitTime;
            default:
                return -1;
        }
    }
    void Update()
    {
        switch(gameTurnStatus)
        {
            case GameTurnStatus.DropingMore:
                selectTime -= Time.deltaTime;
                if(selectTime <= 0 && WhenSelectingTimeOver != null)
                {
                    WhenSelectingTimeOver.Invoke();
                }
                break;
            case GameTurnStatus.WaitingForDrop:
                waitingTime -= Time.deltaTime;
                if(waitingTime <= 0 && WhenWaitingTimeOver != null)
                {
                    WhenWaitingTimeOver.Invoke();
                }
                break;
        }
    }
}
