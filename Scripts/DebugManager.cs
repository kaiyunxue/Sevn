using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public void ShowPiecesColors()
    {
        Debug.Log("ShowPiecesColors-----------------------------------------------------------");

        BoardManager board = GameManager.Instance.boardManager;
        for(int i = 0; i < board.boardLength; i++)
        {
            string tmp = "";
            for(int j = 0; j < board.boardLength; j++)
            {
                tmp = tmp + (int)board.pieces[i, j].pieceColor + "   ";
            }
            Debug.Log(tmp);
        }
        Debug.Log("ShowPiecesColors-----------------------------------------------------------");

    }
    public void ShowPiecesIsValid()
    {
        Debug.Log("ShowPiecesIsValid-----------------------------------------------------------");
        BoardManager board = GameManager.Instance.boardManager;
        for (int i = 0; i < board.boardLength; i++)
        {
            string tmp = "";
            for (int j = 0; j < board.boardLength; j++)
            {
                 tmp += (board.pieces[i, j].isRightHasPiece + "   ");
            }
            Debug.Log(tmp);
        }
        Debug.Log("ShowPiecesIsValid-----------------------------------------------------------");
    }
    public void ShowPiecesLeft()
    {
        Debug.Log("ShowPiecesLeft-----------------------------------------------------------");

        BoardManager board = GameManager.Instance.boardManager;
        for (int i = 0; i < board.boardLength; i++)
        {
            string tmp = "";
            for (int j = 0; j < board.boardLength; j++)
            {
                tmp += (board.pieces[i, j].isLeftHasPiece + "   ");
            }
            Debug.Log(tmp);
        }
        Debug.Log("ShowPiecesLeft-----------------------------------------------------------");

    }
    public void ShowPiecesUp()
    {
        Debug.Log("ShowPiecesUp-----------------------------------------------------------");

        BoardManager board = GameManager.Instance.boardManager;
        for (int i = 0; i < board.boardLength; i++)
        {
            string tmp = "";
            for (int j = 0; j < board.boardLength; j++)
            {
                tmp += (board.pieces[i, j].isUpHasPiece + "   ");
            }
            Debug.Log(tmp);
        }
        Debug.Log("ShowPiecesUp-----------------------------------------------------------");

    }
    public void ShowPiecesRight()
    {
        Debug.Log("ShowPiecesRight-----------------------------------------------------------");

        BoardManager board = GameManager.Instance.boardManager;
        for (int i = 0; i < board.boardLength; i++)
        {
            string tmp = "";
            for (int j = 0; j < board.boardLength; j++)
            {
                tmp += (board.pieces[i, j].isRightHasPiece + "   ");
            }
            Debug.Log(tmp);
        }
        Debug.Log("ShowPiecesRight-----------------------------------------------------------");

    }
    public void ShowPiecesDown()
    {
        Debug.Log("ShowPiecesDown-----------------------------------------------------------");

        BoardManager board = GameManager.Instance.boardManager;
        for (int i = 0; i < board.boardLength; i++)
        {
            string tmp = "";
            for (int j = 0; j < board.boardLength; j++)
            {
                tmp += (board.pieces[i, j].isDownHasPiece + "   ");
            }
            Debug.Log(tmp);
        }
        Debug.Log("ShowPiecesDown-----------------------------------------------------------");

    }
    public void ShowIsCanGoUp()
    {
        Debug.Log("ShowIsCanGoUp-----------------------------------------------------------");
        BoardManager board = GameManager.Instance.boardManager;
        for (int i = 0; i < board.boardLength; i++)
        {
            string tmp = "";
            for (int j = 0; j < board.boardLength; j++)
            {
                tmp += (board.CheckIsCanGoUp(i,j) + "   ");
            }
            Debug.Log(tmp);
        }
        Debug.Log("ShowIsCanGoUp-----------------------------------------------------------");
    }
}
