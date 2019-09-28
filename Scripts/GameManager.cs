﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum PieceColor
{
    Red,
    Green,
    White,
    Blue,
    Grey,
    Black,
    Orange,
    NULL = 999
}
[System.Serializable]
public enum GameMode
{
    OneClientTwoPlayers,
    VSAI,
    VSPlayer,
    Story
}
public enum GameTurnStatus
{
    WaitingForDrop,
    DropingMore,
    Sleeping,
}
[System.Serializable]
public struct GamePlayMode
{
    public GameMode gameMode;
    public int boardSideLength;
    public float selectLimitTime;
    public float waitingLimitTime;
    [Header("游戏细节玩法开关")]
    public bool doUseCrack;
    public bool doUseTurnTimer;
}
[System.Serializable]
public struct PrefabsConfig
{
    [Header("棋子")]
    public Color[] colors;
    public PieceInstance[] pieceInstancePrefabs;
    [Header("计分板表头")]
    public Sprite[] UIScoreHeader;
    [Header("计分板-我的得分")]
    public Color[] UIcolors;
    public Sprite[] UIScoreTextures_0;
    public Sprite[] UIScoreTextures_1;
    [Header("计分板-对方得分")]
    public Sprite UIScoreBlack;
    public Sprite UIScoreGrey;

}
[RequireComponent(typeof(BoardManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Config")]
    public GamePlayMode gamePlayMode;
    public PrefabsConfig prefabConfig;

    [Header("References")]
    public BoardManager boardManager;
    public GameController controller;
    public GameController controller2;
    public BoardInstance boardInstance;
    public UIManager UIInstance;
    public GameController currentController;
    public TurnAtuoTimer timer;
    private bool isReadyToStart;

    [Header("Prefabs")]
    [SerializeField] GameController controllerPrefab;
    [SerializeField] BoardInstance boardInstancePrefab;
    [SerializeField] UIManager uiPrefab;
    public AIController aiController;
    int round;

    public UnityEvent UIAndBoardLogic_WhenGameEnd;

    void Awake()
    {
        controller2 = null;
        Instance = this;
        timer = gameObject.AddComponent<TurnAtuoTimer>();
    }


    IEnumerator InitCoroutine()
    {
        gamePlayMode.gameMode = GameMode.VSAI;
        UIInstance = Instantiate(uiPrefab);
        UIInstance.Init(gamePlayMode.boardSideLength, prefabConfig);
        boardInstance = Instantiate(boardInstancePrefab);
        boardManager = GetComponent<BoardManager>();
        boardManager.Init(gamePlayMode);
        while (!boardManager.IsReady())
        {
            yield return 0;
        }
        switch (gamePlayMode.gameMode)
        {
            case GameMode.OneClientTwoPlayers:
                controller = Instantiate(controllerPrefab);
                controller.name = "Player 1";
                controller2 = Instantiate(controllerPrefab);
                controller2.name = "Player 2";
                controller.Init(gamePlayMode.boardSideLength, UIInstance.panels);
                controller2.Init(gamePlayMode.boardSideLength, UIInstance.panels);
                break;
            case GameMode.VSAI:
                controller = Instantiate(controllerPrefab);
                controller.name = "Player 1";
                controller2 = Instantiate(controllerPrefab);
                controller2.name = "AI";
                controller.Init(gamePlayMode.boardSideLength, UIInstance.panels);
                controller2.Init(gamePlayMode.boardSideLength, UIInstance.panels);
                break;
            default:
                break;
        }
        boardInstance.Init(prefabConfig);

        GameStart();
        if (gamePlayMode.gameMode == GameMode.OneClientTwoPlayers)
        {
            currentController = controller;
            controller.gameObject.SetActive(true);
            controller2.gameObject.SetActive(false);
        }
        else if(gamePlayMode.gameMode == GameMode.VSAI)
        {
            currentController = controller;
            controller.gameObject.SetActive(true);
            controller2.gameObject.SetActive(false);
            aiController = gameObject.AddComponent<AIController>();
        }
        UnityAction BoardDisappear = new UnityAction(boardInstance.BoardDisappear);
        UnityAction UIDisappear = new UnityAction(UIInstance.ActionsOnGameEnd);
        UIAndBoardLogic_WhenGameEnd.AddListener(BoardDisappear);
        UIAndBoardLogic_WhenGameEnd.AddListener(UIDisappear);
    }
    void Start()
    {
        StartCoroutine(InitCoroutine());
    }
    void GameStart()
    {
        isGameEnded = false;
        UnityAction WhenWaitingTimeOver = new UnityAction(OnWaitingTimeOver);
        UnityAction WhenSelectingTimeOver = new UnityAction(EndTurn);
        round = 0;
        boardManager.GameStart();
        timer.GameTurnStatus = GameTurnStatus.WaitingForDrop;
        timer.WhenSelectingTimeOver.AddListener(WhenSelectingTimeOver);
        timer.WhenWaitingTimeOver.AddListener(WhenWaitingTimeOver);
        UIInstance.sBtn.UpdateSkillButton(0);
    }
    /* 
     * 结束回合
     */
    public void EndTurn()
    {
        ++round;
        timer.GameTurnStatus = GameTurnStatus.Sleeping;
        //Debug.Log("On Click EndTurn");
        boardManager.EndTurn();
        StartCoroutine(OnWaitBoardEndTurn());
        UIInstance.sBtn.UpdateSkillButton(round);
    }

    private IEnumerator OnWaitBoardEndTurn()
    {
        while (!boardManager.IsEndTurnDone())
        {
            yield return 0;
        }
        CheckEndGame();
        if(!isGameEnded)
        {
            ChangePlayer();
            timer.GameTurnStatus = GameTurnStatus.WaitingForDrop;
        }
    }

    public void EndGame()
    {
        Debug.Log("EndGame");
        isGameEnded = true;
        timer.GameTurnStatus = GameTurnStatus.Sleeping;
        ShowWiner();
        UIAndBoardLogic_WhenGameEnd.Invoke();
    }


    private void ShowWiner()
    {
        //先判断是否获得一种颜色的全部棋子
        if (controller.CheckDoesGetSevn())
        {
            Win(true);
        }
        else if (controller2.CheckDoesGetSevn())
        {
            Win(false);
        }
        else
        {
            //再判断谁的分数更高
            if (controller.GetControledColorNum() > controller2.GetControledColorNum())
                Win(true);
            else
                Win(false);
        }
    }

    private bool isGameEnded;
    public bool IsGameEnded()
    {
        return isGameEnded;
    }

    private void CheckEndGame()
    {
        if (boardManager.nextPieces.Count <= 0)
            EndGame();
        if (controller.CheckDoesGetSevn())
            EndGame();
        if (controller2.CheckDoesGetSevn())
            EndGame();
    }

    private void ChangePlayer()
    {
        if (gamePlayMode.gameMode == GameMode.OneClientTwoPlayers)
        {
            controller.gameObject.SetActive(!controller.gameObject.activeSelf);
            controller2.gameObject.SetActive(!controller.gameObject.activeSelf);
            currentController = controller.gameObject.activeSelf ? controller : controller2;
            boardInstance.UpdateBoard();
            timer.GameTurnStatus = GameTurnStatus.WaitingForDrop;
        }
        else if (gamePlayMode.gameMode == GameMode.VSAI)
        {
            controller.gameObject.SetActive(!controller.gameObject.activeSelf);
            currentController = controller.gameObject.activeSelf ? controller : controller2;
            boardInstance.UpdateBoard();
            timer.GameTurnStatus = GameTurnStatus.WaitingForDrop;

            if (!controller.gameObject.activeSelf)
            {
                aiController.StartMove(boardManager, AILevel.middle, round, timer);
            }
        }
    }

    public int GetCurRound()
    {
        return round;
    }

    public void OnWaitingTimeOver()
    {
        boardManager.SelectRandomPiece();
        EndTurn();
    }
    public void GetScore(PieceColor c)
    {
        if (gamePlayMode.gameMode == GameMode.OneClientTwoPlayers)
        {
            if (controller.gameObject.activeSelf)
            {
                controller.GetScore(c);
            }
            else
            {
                controller2.GetScore(c);
            }
        }
        else if (gamePlayMode.gameMode == GameMode.VSAI)
        {
            if (controller.gameObject.activeSelf)
            {
                controller.GetScore(c);
            }
            else
            {
                controller2.GetScore(c);
            }
        }
        CheckEndGame();
    }
    public void Win(bool isPlayerOne = true)
    {
        if (gamePlayMode.gameMode == GameMode.OneClientTwoPlayers)
        {
            if (isPlayerOne)
            {
                Debug.Log("Player 1 wins!");
            }
            else
            {
                Debug.Log("Player 2 wins!");
            }
        }
        else if (gamePlayMode.gameMode == GameMode.VSAI)
        {
            if (isPlayerOne)
            {
                Debug.Log("Player 1 wins!");
            }
            else
            {
                Debug.Log("AI wins!");
            }
        }
    }
    public void StartSelect()
    {
        timer.GameTurnStatus = GameTurnStatus.DropingMore;
        //gameTurnStatus = GameTurnStatus.DropingMore;
        Debug.Log("StartSelect!!!");
        //selectTime = 0;
    }

    void Update()
    {
        if (gamePlayMode.doUseTurnTimer)
        {
        //    if(gameTurnStatus == GameTurnStatus.DropingMore)
        //    {
        //        selectTime += Time.deltaTime;
        //        if (selectTime > gamePlayMode.selectLimitTime)
        //        {
        //            EndTurn();
        //        }
        //    }
        }
    }
}
