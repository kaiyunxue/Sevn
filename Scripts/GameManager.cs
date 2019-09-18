using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
[System.Serializable]
public struct GamePlayMode
{
    public GameMode gameMode;
    public int boardSideLength;
    public double selectLimitTime;
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

    [Header("Prefabs")]
    [SerializeField] GameController controllerPrefab;
    [SerializeField] BoardInstance boardInstancePrefab;

    private bool isStartSelect = false;
    private double selectTime = 0;
    void Awake()
    {
        Instance = this;
        boardInstance = Instantiate(boardInstancePrefab);
        boardManager = GetComponent<BoardManager>();
        boardManager.Init(gamePlayMode);
        controller2 = null;
        switch (gamePlayMode.gameMode)
        {
            case GameMode.OneClientTwoPlayers:
                controller = Instantiate(controllerPrefab);
                controller.name = "Player 1";
                controller2 = Instantiate(controllerPrefab);
                controller2.name = "Player 2";
                controller.Init(gamePlayMode.boardSideLength,UIManager.instance.panels[0]);
                controller2.Init(gamePlayMode.boardSideLength, UIManager.instance.panels[1]);
                break;
            default:
                break;
        }
        boardInstance.Init();
        //UI设置成单例了，方便去耦合
        UIManager.instance.Init(gamePlayMode);
    }
    void Start()
    {
        GameStart();
        if (gamePlayMode.gameMode == GameMode.OneClientTwoPlayers)
        {
            controller.gameObject.SetActive(true);
            controller2.gameObject.SetActive(false);
        }
    }
    void GameStart()
    {
        boardManager.GameStart();
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
        }
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
        isStartSelect = true;
        //Debug.Log("StartSelect!!!");
        selectTime = 0;
    }

    void Update()
    {
        if (gamePlayMode.doUseTurnTimer&&isStartSelect)
        {
            selectTime += Time.deltaTime;
            if (selectTime > gamePlayMode.selectLimitTime)
            {
                EndTurn();
                isStartSelect = false;
            }
        }
    }
}
