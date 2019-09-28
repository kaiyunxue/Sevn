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
    private Thread moveThread;
    List<AIResult> result;
    BoardManager boardManager;
    private int ABPruningDepth = 5;

    private bool isBeSkilled = false;
    public void MakeAIStupid()
    {
        isBeSkilled = true;
    }

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
                moveThread = new Thread(new ThreadStart(MoveInEasyLevel));
                moveThread.Start();
                break;
            case AILevel.middle:
                moveThread = new Thread(new ThreadStart(MoveInMiddleLevel));
                moveThread.Start();
                break;
            case AILevel.hard:
                moveThread = new Thread(new ThreadStart(MoveInHardLevel));
                moveThread.Start();
                break;
            default:
                moveThread = new Thread(new ThreadStart(MoveInMiddleLevel));
                moveThread.Start();
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
            moveThread.Abort();
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
            if (isBeSkilled)
            {
                isBeSkilled = false;
                break;
            }
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
        foreach (var v in dic)
        {
            PieceColor color = v.Key;
            List<Piece> list = v.Value;
            int listNum = list.Count;
            if (listNum > maxNum)
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
        foreach (var v in dic)
        {
            res.Add(v.Key);
        }
        return res;
    }

    private void BeginCalculate()
    {
        result.Clear();
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

        foreach (var v in piecesByColor[mostColor])
        {
            result.Add(new AIResult(v.x, v.y));
        }

        EndCalculate();
    }

    private struct AIHardDataStruct
    {
        public Piece[,] pieces;
        public List<Piece> nextPieces;
        public PlayerRecord playerRecord;
        public PlayerRecord aiRecord;
        public List<Piece> bestCombination;
        public int depth;
        public AIHardDataStruct(Piece[,] argPieces, List<Piece> argNextPieces, PlayerRecord argPlayerRecord, PlayerRecord argAiRecord,int argDepth)
        {
            pieces = argPieces;
            nextPieces = argNextPieces;
            playerRecord = argPlayerRecord;
            aiRecord = argAiRecord;
            depth = argDepth;
            bestCombination = new List<Piece>();
        }

        private Piece[,] CopyPieces()
        {
            Piece[,] tar;
            tar = new Piece[pieces.GetLength(0), pieces.GetLength(1)];
            for (int i = 0; i < pieces.GetLength(0); ++i)
            {
                for (int j = 0; j < pieces.GetLength(1); j++)
                {
                    tar[i, j] = pieces[i, j].Clone();
                }
            }

            return tar;
        }
        private List<Piece> CopyCurNextPieces()
        {
            List<Piece> tar = new List<Piece>();
            foreach (var v in nextPieces)
            {
                tar.Add(v.Clone());
            }

            return tar;
        }

        public AIHardDataStruct Clone()
        {
            AIHardDataStruct tar = new AIHardDataStruct();
            tar.pieces = CopyPieces();
            tar.nextPieces = CopyCurNextPieces();
            tar.playerRecord = playerRecord.Clone();
            tar.aiRecord = aiRecord.Clone();
            tar.depth = depth;

            return tar;
        }
    }

    private void MoveInHardLevel()
    {
        BeginCalculate();

        Piece[,] curPieces = boardManager.GetPieces();
        List<Piece> curNextPieces = boardManager.GetNextPieces();
        PlayerRecord playerRecord = GameManager.Instance.controller.GetScoreValue();
        PlayerRecord aiRecord = GameManager.Instance.controller2.GetScoreValue();
        int curDepth = 0;
        int alpha = -0x7FFFFFFE;
        int beta= 0x7FFFFFFE;
        AIHardDataStruct curDepthData = new AIHardDataStruct(curPieces, curNextPieces, playerRecord, aiRecord, curDepth);
        ABPruning(ref curDepthData, alpha, beta, false);
        Debug.Log("curDepthData.bestCombination count: " + curDepthData.bestCombination.Count);
        List<Piece> abRes = curDepthData.bestCombination;

        foreach (var v in abRes)
        {
            result.Add(new AIResult(v.x, v.y));
        }
        Debug.Log("EndCalculate");

        EndCalculate();
    }

    private bool CheckEndGame(AIHardDataStruct curDepthData)
    {
        List<Piece> curNextPieces = curDepthData.nextPieces;
        PlayerRecord curPlayerRecord = curDepthData.playerRecord;
        PlayerRecord curAiRecord = curDepthData.aiRecord;
        GamePlayMode gpm = GameManager.Instance.gamePlayMode;
        if (curNextPieces.Count <= 0)
        {
            return true;
        }

        foreach (int s in curPlayerRecord.secord)
        {
            if (s >= gpm.boardSideLength)
            {
                return true;
            }
        }

        foreach (int s in curAiRecord.secord)
        {
            if (s >= gpm.boardSideLength)
            {
                return true;
            }
        }
        return false;
    }

    private void GetScore(AIHardDataStruct curDepthData, PieceColor color)
    {
        if (curDepthData.depth % 2 != 0)
        {
            curDepthData.playerRecord.secord[(int)color]++;
        }
        else
        {
            curDepthData.aiRecord.secord[(int)color]++;
        }
    }

    private void DeletePiece(AIHardDataStruct curDepthData, int x, int y)
    {
        Piece[,] curPieces = curDepthData.pieces;
        List<Piece> curNextPieces = curDepthData.nextPieces;

        if (!curNextPieces.Contains(curPieces[x, y]))
            Debug.LogError("Not A Valid Piece");
        GetScore(curDepthData, curPieces[x, y].pieceColor);
        curNextPieces.Remove(curPieces[x, y]);
        curPieces[x, y].isValid = false;
    }

    private bool CheckIsCanGoUp(Piece[,] curPieces, int x, int y)
    {
        Piece p = curPieces[x, y];
        if ((!p.isLeftHasPiece) && (!p.isUpHasPiece))
            return true;
        if ((!p.isLeftHasPiece) && (!p.isDownHasPiece))
            return true;
        if ((!p.isRightHasPiece) && (!p.isUpHasPiece))
            return true;
        if ((!p.isRightHasPiece) && (!p.isDownHasPiece))
            return true;
        return false;
    }

    private void SelectPiece(AIHardDataStruct curDepthData, int x, int y)
    {
        Piece[,] curPieces = curDepthData.pieces;
        List<Piece> curNextPieces = curDepthData.nextPieces;

        curPieces[x, y].isValid = false;
        DeletePiece(curDepthData, x, y);
        List<Piece> changedPieces = new List<Piece>();//去掉了注释 若不去掉注释会导致碎裂棋子重复计算
        if (x - 1 >= 0 && curPieces[x - 1, y].isValid)
        {
            curPieces[x - 1, y].isDownHasPiece = false;
            if (!changedPieces.Contains(curPieces[x - 1, y]))
                changedPieces.Add(curPieces[x - 1, y]);
        }
        if (x + 1 < GameManager.Instance.gamePlayMode.boardSideLength && curPieces[x + 1, y].isValid)
        {
            curPieces[x + 1, y].isUpHasPiece = false;
            if (!changedPieces.Contains(curPieces[x + 1, y]))
                changedPieces.Add(curPieces[x + 1, y]);
        }
        if (y - 1 >= 0 && curPieces[x, y - 1].isValid)
        {
            curPieces[x, y - 1].isRightHasPiece = false;
            if (!changedPieces.Contains(curPieces[x, y - 1]))
                changedPieces.Add(curPieces[x, y - 1]);
        }
        if (y + 1 < GameManager.Instance.gamePlayMode.boardSideLength && curPieces[x, y + 1].isValid)
        {
            curPieces[x, y + 1].isLeftHasPiece = false;
            if (!changedPieces.Contains(curPieces[x, y + 1]))
                changedPieces.Add(curPieces[x, y + 1]);
        }
        foreach (var v in changedPieces)
        {
            if (curNextPieces.Contains(v) && !CheckIsCanGoUp(curPieces, x, y))
            {
                curNextPieces.Remove(v);
            }
            else if (CheckIsCanGoUp(curPieces, v.x, v.y))
            {
                if (!curNextPieces.Contains(v))
                {
                    curNextPieces.Add(v);
                }
            }
        }
    }

    //true是玩家赢 false是AI赢
    private bool CheckWinner(AIHardDataStruct curDepthData)
    {
        GamePlayMode gpm = GameManager.Instance.gamePlayMode;
        //先判断是否获得一种颜色的全部棋子
        foreach (int s in curDepthData.playerRecord.secord)
        {
            if (s >= gpm.boardSideLength)
            {
                return true;
            }
        }
        foreach (int s in curDepthData.aiRecord.secord)
        {
            if (s >= gpm.boardSideLength)
            {
                return false;
            }
        }

        int playerControledNum = 0;
        foreach (var v in curDepthData.playerRecord.secord)
        {
            if (v > gpm.boardSideLength / 2)
            {
                ++playerControledNum;
            }
        }

        int aiControledNum = 0;
        foreach (var v in curDepthData.aiRecord.secord)
        {
            if (v > gpm.boardSideLength / 2)
            {
                ++aiControledNum;
            }
        }

        //再判断谁的分数更高
        if (playerControledNum > aiControledNum)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private bool EngTurn(AIHardDataStruct curDepthData)
    {
        List<Piece> nextPieces = curDepthData.nextPieces;
        Piece[,] pieces = curDepthData.pieces;
        GamePlayMode gpm = GameManager.Instance.gamePlayMode;

        bool isChanged = true;
        while (isChanged)
        {
            isChanged = false;

            if (gpm.doUseCrack)
            {
                List<Piece> crackPieces = new List<Piece>();
                foreach (var p in nextPieces)
                {
                    if (p.isValid && p.isCrackPiece)
                    {
                        crackPieces.Add(p);
                        isChanged = true;
                    }
                }

                if (isChanged)
                {
                    foreach (var p in crackPieces)
                    {
                        SelectPiece(curDepthData, p.x, p.y);
                        if (CheckEndGame(curDepthData))
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    private int CalculateCurValue(AIHardDataStruct curDepthData)
    {
        GamePlayMode gpm = GameManager.Instance.gamePlayMode;
        int playerValue = 0;
        int aiValue = 0;
        foreach (var v in curDepthData.playerRecord.secord)
        {
            if (v > gpm.boardSideLength / 2)
            {
                //++playerControledNum;
                playerValue += 100;
            }
            playerValue += v * 10;
        }

        foreach (var v in curDepthData.aiRecord.secord)
        {
            if (v > gpm.boardSideLength / 2)
            {
                aiValue += 100;
            }
            aiValue += v * 10;
        }

        int value = aiValue - playerValue;
        if (curDepthData.depth % 2 == 0)
        {
            return value;
        }
        else
        {
            return -value;
        }
    }

    //返回true则继续往下递归 返回false则停止
    private bool SelectCombination(AIHardDataStruct curDepthData, List<Piece> combination)
    {
        //选择棋子
        foreach (var piece in combination)
        {
            SelectPiece(curDepthData, piece.x, piece.y);

            if (CheckEndGame(curDepthData))
            {
                return true;
            }
        }
        if (EngTurn(curDepthData)) 
        {
            return true;
        }
        return false;
    }

    private int CalculateValue(AIHardDataStruct curDepthData, bool isEndGame)
    {
        int value = 0;
        if (isEndGame)
        {
            if (CheckWinner(curDepthData))
            {
                //玩家赢则权值最低
                value += -1000;
            }
            else
            {
                //AI赢则权值最高
                value += 1000;
            }
        }

        return value + CalculateCurValue(curDepthData);
    }

    private int ABPruning(ref AIHardDataStruct curDepthData,int alpha,int beta, bool isEndGame)
    {
        if (curDepthData.depth > ABPruningDepth || isEndGame)
        {
            return CalculateValue(curDepthData, isEndGame);
        }

        Dictionary <PieceColor, List<Piece>> piecesByColor = GroupByColor(curDepthData.nextPieces);
        int bestValue = -0x7FFFFFF;
        foreach (var pieces in piecesByColor)
        {
            List<List<Piece>> combinations = new List<List<Piece>>();
            for (int i = 1; i <= pieces.Value.Count; ++i)
            {
                List<Piece[]> tmp;
                tmp = Algorithms.PermutationAndCombination<Piece>.GetCombination(pieces.Value.ToArray(), i);
                foreach (var v in tmp)
                {
                    List<Piece> combination = new List<Piece>();
                    for (int j = 0; j < v.GetLength(0); ++j)
                    {
                        combination.Add(v[j]);
                    }
                    combinations.Add(combination);
                }
            }
            foreach (var combination in combinations)
            {
                AIHardDataStruct nextDepthData = curDepthData.Clone();
                bool isNextEndGame = SelectCombination(nextDepthData, combination);
                nextDepthData.depth++;
                int nextValue = -ABPruning(ref nextDepthData, -beta, -alpha, isNextEndGame);


                if (nextValue > bestValue)
                {
                    bestValue = nextValue;
                    curDepthData.bestCombination = combination;
                }
                if (bestValue > alpha)
                {
                    alpha = bestValue;
                }
                if (bestValue >= beta)
                {
                    return bestValue;
                }
            }
        }

        return bestValue;
    }
}
