using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public struct AIResult
{
    public int x;
    public int y;
    public AIResult(int argX,int argY)
    {
        x = argX;
        y = argY;
    }
}
public enum AILevel
{
    easy,
    middle,
    hard,
}
public class AIController : MonoBehaviour
{
    private bool isDone;
    private int moveRound;
    private TurnAtuoTimer timer;
    private float intervalSecond;
    List<AIResult> result;
    BoardManager boardManager;

    void Start()
    {
        intervalSecond = 0.5f;
        result = new List<AIResult>();

        isDone = false;
        moveRound = 0;
        boardManager = null;
        timer = null;
    }

    public void StartMove(BoardManager argBoardManager, AILevel aiLevel, int argMoveRound, TurnAtuoTimer argTimer)
    {
        boardManager = argBoardManager;
        moveRound = argMoveRound;
        timer = argTimer;
        switch (aiLevel)
        {
            case AILevel.easy:
                new Thread(new ThreadStart(MoveInEasyLevel)).Start();
                break;
            case AILevel.middle:
                new Thread(new ThreadStart(MoveInMiddleLevel)).Start();
                break;
            default:
                new Thread(new ThreadStart(MoveInMiddleLevel)).Start();
                break;
        }
        StartCoroutine(OnMove());
    }

    private IEnumerator OnMove()
    {
        while (!isDone && moveRound == GameManager.Instance.GetCurRound())
        {
            yield return 0;
        }
        if (moveRound != GameManager.Instance.GetCurRound())
        {
            EndMove();
            yield break;
        }

        for (int i = 0; i < result.Count; ++i)
        {
            AIResult v = result[i];
            if (timer.GameTurnStatus == GameTurnStatus.WaitingForDrop)
            {
                if (timer.waitingTime > intervalSecond * result.Count)
                {
                    float maxRandSec = timer.waitingTime - intervalSecond * result.Count;
                    maxRandSec = maxRandSec > 5 ? 5 : maxRandSec;
                    float randSec = UnityEngine.Random.Range(0f, maxRandSec);
                    Debug.Log("randSec: " + randSec);
                    yield return new WaitForSeconds(randSec);
                }
            }
            else if (timer.GameTurnStatus == GameTurnStatus.DropingMore)
            {
                if (timer.selectTime > intervalSecond * (result.Count - i))
                {
                    yield return new WaitForSeconds(intervalSecond);
                }
            }

            if (moveRound != GameManager.Instance.GetCurRound())
            {
                break;
            }
            boardManager.SelectPiece(v.x, v.y);
        }

        EndMove();
    }

    private void EndMove()
    {
        isDone = false;
        moveRound = 0;
        boardManager = null;
        timer = null;
    }

    public bool IsDone()
    {
        return isDone;
    }

    public List<AIResult> GetResult()
    {
        List<AIResult> res = new List<AIResult>();
        return res;
    }

    private Dictionary<PieceColor, List<Piece>> GroupByColor(List<Piece> pieces)
    {
        Dictionary<PieceColor, List<Piece>> dic = new Dictionary<PieceColor, List<Piece>>();
        foreach (var piece in pieces)
        {
            if (!dic.ContainsKey(piece.pieceColor))
            {
                dic[piece.pieceColor] = new List<Piece>();
            }
            dic[piece.pieceColor].Add(piece);
        }

        return dic;
    }

    private PieceColor GetMostColor(Dictionary<PieceColor, List<Piece>> dic)
    {
        int maxNum = 0;
        PieceColor maxColor = PieceColor.NULL;
        foreach(var v in dic)
        {
            PieceColor color = v.Key;
            List<Piece> list = v.Value;
            int listNum = list.Count;
            if(listNum>maxNum)
            {
                maxNum = listNum;
                maxColor = color;
            }
        }

        return maxColor;
    }

    private List<PieceColor> GetColorList(Dictionary<PieceColor, List<Piece>> dic)
    {
        List<PieceColor> res = new List<PieceColor>();
        foreach(var v in dic)
        {
            res.Add(v.Key);
        }
        return res;
    }

    private void BeginCalculate()
    {
        isDone = false;
    }

    private void EndCalculate()
    {
        isDone = true;
    }

    private void MoveInEasyLevel()
    {
        BeginCalculate();

        List<Piece> nextPieces = boardManager.GetNextPieces();
        Dictionary<PieceColor, List<Piece>> piecesByColor = GroupByColor(nextPieces);
        List<PieceColor> colorList = GetColorList(piecesByColor);
        System.Random rand = new System.Random((int)DateTime.Now.Ticks);

        int randColorIdx = rand.Next(0, colorList.Count - 1);
        PieceColor randColor = colorList[randColorIdx];

        List<Piece> pieceList = piecesByColor[randColor];
        int randNum = rand.Next(1, pieceList.Count);

        for (int i = 1; i <= randNum; ++i)
        {
            int randPieceIdx = rand.Next(0, pieceList.Count - 1);
            Piece piece = pieceList[randPieceIdx];
            result.Add(new AIResult(piece.x, piece.y));
            pieceList.RemoveAt(randPieceIdx);
        }

        EndCalculate();
    }

    private void MoveInMiddleLevel()
    {
        BeginCalculate();

        List<Piece> nextPieces = boardManager.GetNextPieces();
        Dictionary<PieceColor, List<Piece>> piecesByColor = GroupByColor(nextPieces);
        PieceColor mostColor = GetMostColor(piecesByColor);

        result.Clear();
        foreach (var v in piecesByColor[mostColor])
        {
            result.Add(new AIResult(v.x, v.y));
        }

        EndCalculate();
    }
}
