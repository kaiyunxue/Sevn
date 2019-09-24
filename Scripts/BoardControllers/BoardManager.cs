using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Piece
{
    public PieceColor pieceColor;
    //是否被摁下去
    public bool isValid;

    //是否为碎裂棋子
    public bool isCrackPiece;

    //这么写是为了后面不被写蒙-
    public bool isLeftHasPiece;
    public bool isRightHasPiece;
    public bool isUpHasPiece;
    public bool isDownHasPiece;
    public int x;
    public int y;

    public static bool operator ==(Piece l, Piece r)
    {
        if (l.x == r.x && l.y == r.y)
            return true;
        return false;
    }
    public override bool Equals(object obj)
    {
        return this == (Piece)obj;
    }
    public override int GetHashCode()
    {
        return 1;
    }
    public static bool operator !=(Piece l, Piece r)
    {
        if (l.x != r.x || l.y != r.y)
            return true;
        return false;
    }
    public bool CheckCanPieceStand()
    {
        //todo
        return false;
    }
    public Piece(PieceColor pieceColor, bool left, bool right, bool up, bool down, int x, int y)
    {
        this.pieceColor = pieceColor;
        isCrackPiece = false;
        isValid = true;
        isLeftHasPiece = left;
        isRightHasPiece = right;
        isUpHasPiece = up;
        isDownHasPiece = down;
        this.x = x;
        this.y = y;
    }
    public override string ToString()
    {
        string s = "" + pieceColor + "PositionOnBoard(" + (GameManager.Instance.gamePlayMode.boardSideLength - 1 - y) + " , " + x + ")   " + "True Position:(" + x + " , " + y + ")" + "Color:" + (int)pieceColor;
        return s;
    }
}

public class BoardManager : MonoBehaviour
{
    public int boardLength;
    public Piece[,] pieces;
    public List<Piece> changedPieces;
    public List<Piece> nextPieces;
    public List<PieceColor> colorPool;
    public PieceColor selectedColor;
    public BoardInstance boardInstance;
    static private GamePlayMode gpm;

    public void InitColorPool(int len)
    {
        for (int i = 0; i < len; ++i)
            for (int j = 0; j < len; ++j)
            {
                PieceColor color = (PieceColor)(j);
                colorPool.Add(color);
            }
    }

    public void InitCrackPieces()
    {
        int randomX = 0;
        int randomY = 0;
        for (int i = 0; i < boardLength; ++i)
        {
            while (true)
            {
                randomX = Random.Range(0, boardLength - 1);
                randomY = Random.Range(0, boardLength - 1);
                if (!(nextPieces.Contains(pieces[randomX, randomY]) || pieces[randomX, randomY].isCrackPiece))
                {
                    break;
                }
            }

            pieces[randomX, randomY].isCrackPiece = true;
        }
    }

    public bool IsValidBoard()
    {
        int[] moveX = { 0, 1 };
        int[] moveY = { 1, 0 };
        for (int i = 0; i < boardLength; ++i)
        {
            for (int j = 0; j < boardLength; ++j)
            {
                Piece piece = pieces[i, j];
                PieceColor color = piece.pieceColor;
                for (int moveIdx = 0; moveIdx < 2; ++moveIdx)
                {
                    int newX = i + moveX[moveIdx];
                    int newY = j + moveY[moveIdx];

                    if (0 <= newX && newX < boardLength && 0 <= newY && newY < boardLength)
                    {
                        PieceColor newColor = pieces[newX, newY].pieceColor;
                        if (newColor == color)
                            return false;
                    }
                }
            }
        }
        return true;
    }

    public void Init(GamePlayMode arg_gpm)
    {
        gpm = arg_gpm;
        selectedColor = PieceColor.NULL;
        boardLength = gpm.boardSideLength;
        nextPieces = new List<Piece>();

        for (; ; )
        {
            InitColorPool(boardLength);
            pieces = new Piece[boardLength, boardLength];
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    bool up = !(i == 0);
                    bool down = !(i == boardLength - 1);
                    bool left = !(j == 0);
                    bool right = !(j == boardLength - 1);
                    int randomIdx = Random.Range(0, colorPool.Count - 1);
                    pieces[i, j] = new Piece(colorPool[randomIdx], left, right, up, down, i, j);
                    colorPool.RemoveAt(randomIdx);
                }
            }
            if (IsValidBoard())
                break;
        }

        nextPieces.Add(pieces[0, 0]);
        nextPieces.Add(pieces[0, boardLength - 1]);
        nextPieces.Add(pieces[boardLength - 1, 0]);
        nextPieces.Add(pieces[boardLength - 1, boardLength - 1]);
        if(gpm.doUseCrack)
            InitCrackPieces();

        boardInstance = GameManager.Instance.boardInstance;
    }
    void Start()
    {
    }
    public void GameStart()
    {
        foreach (var p in nextPieces)
        {
            if(p.isValid)
                boardInstance.pieces[p.x, p.y].GoUp();
        }

    }
    public void PieceLeftRemoved(ref Piece piece)
    {
        piece.isLeftHasPiece = true;
    }
    public void PieceUpRemoved(ref Piece piece)
    {
        piece.isUpHasPiece = true;
    }
    public void PieceRightRemoved(ref Piece piece)
    {
        piece.isRightHasPiece = true;
    }
    public void PieceDownRemoved(ref Piece piece)
    {
        piece.isDownHasPiece = true;
    }
    public bool CheckIsCanGoUp(int x, int y)
    {
        Piece p = pieces[x, y];
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
    /*
     * 如果是第一次 return true
     * 如果是相同颜色 return true
     * 如果不是相同颜色 return false
     */
    public bool TrySelectFirstColor(PieceColor color)
    {
        if (selectedColor == PieceColor.NULL)
        {
            selectedColor = color;
            GameManager.Instance.StartSelect();
            return true;
        }
        if (selectedColor == color)
        {
            return true;
        }
        return false;
    }
    public void PieceBeKilled(int x, int y)
    {
        pieces[x, y].isValid = false;
        //changedPieces.Clear();
        if (x - 1 >= 0 && pieces[x - 1, y].isValid)
        {
            pieces[x - 1, y].isDownHasPiece = false;
            if (!changedPieces.Contains(pieces[x - 1, y]))
                changedPieces.Add(pieces[x - 1, y]);
        }
        if (x + 1 < boardLength && pieces[x + 1, y].isValid)
        {
            pieces[x + 1, y].isUpHasPiece = false;
            if (!changedPieces.Contains(pieces[x + 1, y]))
                changedPieces.Add(pieces[x + 1, y]);
        }
        if (y - 1 >= 0 && pieces[x, y - 1].isValid)
        {
            pieces[x, y - 1].isRightHasPiece = false;
            if (!changedPieces.Contains(pieces[x, y - 1]))
                changedPieces.Add(pieces[x, y - 1]);
        }
        if (y + 1 < boardLength && pieces[x, y + 1].isValid)
        {
            pieces[x, y + 1].isLeftHasPiece = false;
            if (!changedPieces.Contains(pieces[x, y + 1]))
                changedPieces.Add(pieces[x, y + 1]);
        }
        foreach (var v in changedPieces)
        {
            if (nextPieces.Contains(v) && !CheckIsCanGoUp(x, y))
            {
                nextPieces.Remove(v);
                boardInstance.pieces[v.x, v.y].GoBack();
            }
            else if (CheckIsCanGoUp(v.x, v.y))
            {
                if (!nextPieces.Contains(v))
                {
                    nextPieces.Add(v);
                }
            }
        }
    }
    public void DeletePiece(int x, int y)
    {
        if (!nextPieces.Contains(pieces[x, y]))
            Debug.LogError("Not A Valid Piece");
        GameManager.Instance.GetScore(pieces[x, y].pieceColor);
        nextPieces.Remove(pieces[x, y]);
        //Debug.Log("Delete piece:" + pieces[x, y].ToString());
        pieces[x, y].isValid = false;
    }
    public void EndTurn()
    {
        changedPieces.Clear();
        bool isChanged = true;
        while (isChanged)
        {
            foreach (var p in nextPieces)
            {
                if (p.isValid)
                {
                    if (boardInstance.pieces[p.x, p.y] != null)
                        boardInstance.pieces[p.x, p.y].GoUp();
                    else
                        Debug.LogError("Pieces has been destoryed:::" + p.ToString());
                }
            }
            isChanged = false;

            if (gpm.doUseCrack)
            {
                foreach (var p in nextPieces)
                {
                    if (p.isValid)
                    {
                        if (p.isCrackPiece)
                        {
                            Debug.Log("Crack Piece Going Down!");
                            if (boardInstance.pieces[p.x, p.y] != null)
                            {
                                boardInstance.pieces[p.x, p.y].GoDown();
                                PieceBeKilled(p.x, p.y);
                            }
                            else
                                Debug.LogError("Pieces has been destoryed");
                            isChanged = true;
                        }
                    }
                }
            }
        }
        selectedColor = PieceColor.NULL;
    }
}
