using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    OneClientTwoPlayers,
    VSAI,
    VSPlayer,
    Story
}
[RequireComponent(typeof(BoardManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameMode gameMode;
    public readonly int boardSideLength = 7;
    public BoardManager boardManager;
    public GameController controller;
    public GameController controller2;
    public BoardInstance boardInstance;
    private bool isStartSelect = false;
    private double selectTime = 0;
    private double selectLimitTime = 5;
    [Header("Prefabs")]
    [SerializeField] GameController controllerPrefab;
    [SerializeField] BoardInstance boardInstancePrefab;
    void Awake()
    {
        Instance = this;
        boardInstance = Instantiate(boardInstancePrefab);
        boardManager = GetComponent<BoardManager>();
        boardManager.Init(boardSideLength);
        controller2 = null;
        switch (gameMode)
        {
            case GameMode.OneClientTwoPlayers:
                controller = Instantiate(controllerPrefab);
                controller.name = "Player 1";
                controller2 = Instantiate(controllerPrefab);
                controller2.name = "Player 2";
                controller.Init(boardSideLength);
                controller2.Init(boardSideLength);
                break;
            default:
                break;
        }
        boardInstance.Init();
    }
    void Start()
    {
        GameStart();
        if (gameMode == GameMode.OneClientTwoPlayers)
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
        Debug.Log("On Click EndTurn");
        boardManager.EndTurn();
        if (gameMode == GameMode.OneClientTwoPlayers)
        {
            controller.gameObject.SetActive(!controller.gameObject.activeSelf);
            controller2.gameObject.SetActive(!controller.gameObject.activeSelf);
        }
    }
    public void GetScore(PieceColor c)
    {
        if (gameMode == GameMode.OneClientTwoPlayers)
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
        if (gameMode == GameMode.OneClientTwoPlayers)
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
        Debug.Log("StartSelect!!!");
        selectTime = 0;
    }

    void Update()
    {
        if (isStartSelect)
        {
            selectTime += Time.deltaTime;
            if (selectTime > selectLimitTime)
            {
                EndTurn();
                isStartSelect = false;
            }
        }
    }
}
