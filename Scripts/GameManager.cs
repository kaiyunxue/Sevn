using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    Sleeping
}
[System.Serializable]
public struct GamePlayMode
{
    public GameMode gameMode;
    public int boardSideLength;
    public float selectLimitTime;
    public float waitingLimitTime;
    public Color[] colors;
    [Header("游戏细节玩法开关")]
    public bool doUseCrack;
    public bool doUseTurnTimer;
}
[RequireComponent(typeof(BoardManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Config")]
    public GamePlayMode gamePlayMode;

    [Header("References")]
    public BoardManager boardManager;
    public GameController controller;
    public GameController controller2;
    public BoardInstance boardInstance;
    public UIManager UIInstance;
    public GameController currentController;
    public TurnAtuoTimer timer;

    [Header("Prefabs")]
    [SerializeField] GameController controllerPrefab;
    [SerializeField] BoardInstance boardInstancePrefab;
    [SerializeField] UIManager uiPrefab;
    UnityAction WhenWaitingTimeOver;// = new UnityAction(OnWaitingTimeOver);
    UnityAction WhenSelectingTimeOver;// = new UnityAction(EndTurn);

    void Awake()
    {
        timer = gameObject.AddComponent<TurnAtuoTimer>();
        Instance = this;
        boardInstance = Instantiate(boardInstancePrefab);
        boardManager = GetComponent<BoardManager>();
        boardManager.Init(gamePlayMode);
        controller2 = null;
        UIInstance = Instantiate(uiPrefab);
        UIInstance.Init(gamePlayMode);
        switch (gamePlayMode.gameMode)
        {
            case GameMode.OneClientTwoPlayers:
                controller = Instantiate(controllerPrefab);
                controller.name = "Player 1";
                controller2 = Instantiate(controllerPrefab);
                controller2.name = "Player 2";
                controller.Init(gamePlayMode.boardSideLength, UIInstance.panels[0]);
                controller2.Init(gamePlayMode.boardSideLength, UIInstance.panels[1]);
                break;
            default:
                break;
        }
        boardInstance.Init();
    }
    void Start()
    {
        GameStart();
        if (gamePlayMode.gameMode == GameMode.OneClientTwoPlayers)
        {
            currentController = controller;
            controller.gameObject.SetActive(true);
            controller2.gameObject.SetActive(false);
        }
    }
    void GameStart()
    {
        UnityAction WhenWaitingTimeOver = new UnityAction(OnWaitingTimeOver);
        UnityAction WhenSelectingTimeOver = new UnityAction(EndTurn);
        boardManager.GameStart();
        timer.GameTurnStatus = GameTurnStatus.WaitingForDrop;
        timer.WhenSelectingTimeOver.AddListener(WhenSelectingTimeOver);
        timer.WhenWaitingTimeOver.AddListener(WhenWaitingTimeOver);
    }
    /* 
     * 结束回合
     */
    public void EndTurn()
    {
        //Debug.Log("On Click EndTurn");
        boardManager.EndTurn();
        if (gamePlayMode.gameMode == GameMode.OneClientTwoPlayers)
        {
            controller.gameObject.SetActive(!controller.gameObject.activeSelf);
            controller2.gameObject.SetActive(!controller.gameObject.activeSelf);
            currentController = controller.gameObject.activeSelf ? controller : controller2;
            boardInstance.UpdateBoard();
            timer.GameTurnStatus = GameTurnStatus.WaitingForDrop;
        }
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
                if (controller.CheckDoesGetSevn())
                {
                    Win();
                }
            }
            else
            {
                controller2.GetScore(c);
                if (controller.CheckDoesGetSevn())
                {
                    Win(false);
                }
            }
        }
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
    }
    public void StartSelect()
    {
        timer.GameTurnStatus = GameTurnStatus.DropingMore;
        //gameTurnStatus = GameTurnStatus.DropingMore;
        ////Debug.Log("StartSelect!!!");
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
