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
                 tmp = tmp + (int)board.pieces[j, board.boardLength - i - 1].pieceColor + "   ";
            }
            Debug.Log(tmp);
        }
        Debug.Log(" ShowPiecesColors-----------------------------------------------------------");
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
                 tmp += (board.pieces[j, board.boardLength - 1 - i].isValid + "   ");
            }
            Debug.Log(tmp);
        }
        Debug.Log("ShowPiecesIsValid-----------------------------------------------------------");
    }

    //下面左右上下没对上是因为横纵坐标问题，没对上才是对的
    public void ShowPiecesLeft()
    {
        Debug.Log("ShowPiecesLeft-----------------------------------------------------------");

        BoardManager board = GameManager.Instance.boardManager;
        for (int i = 0; i < board.boardLength; i++)
        {
            string tmp = "";
            for (int j = 0; j < board.boardLength; j++)
            {
                if (board.pieces[j, board.boardLength - 1 - i].isValid)
                    tmp += (board.pieces[j, board.boardLength - 1 - i].isUpHasPiece + "   ");
                else
                    tmp += "        ";
            }
            Debug.Log(tmp);
        }
        Debug.Log("ShowPiecesLeft-----------------------------------------------------------");

    }
    //下面左右上下没对上是因为横纵坐标问题，没对上才是对的
    public void ShowPiecesUp()
    {
        Debug.Log("ShowPiecesUp-----------------------------------------------------------");

        BoardManager board = GameManager.Instance.boardManager;
        for (int i = 0; i < board.boardLength; i++)
        {
            string tmp = "";
            for (int j = 0; j < board.boardLength; j++)
            {
                if (board.pieces[j, board.boardLength - 1 - i].isValid)
                    tmp += (board.pieces[j, board.boardLength - 1 - i].isRightHasPiece + "   ");
                else
                    tmp += "        ";
            }
            Debug.Log(tmp);
        }
        Debug.Log("ShowPiecesUp-----------------------------------------------------------");

    }
    //下面左右上下没对上是因为横纵坐标问题，没对上才是对的
    public void ShowPiecesRight()
    {
        Debug.Log("ShowPiecesRight-----------------------------------------------------------");

        BoardManager board = GameManager.Instance.boardManager;
        for (int i = 0; i < board.boardLength; i++)
        {
            string tmp = "";
            for (int j = 0; j < board.boardLength; j++)
            {
                if (board.pieces[j, board.boardLength - 1 - i].isValid)
                    tmp += (board.pieces[j, board.boardLength - 1 - i].isDownHasPiece + "   ");
                else
                    tmp += "        ";
            }
            Debug.Log(tmp);
        }
        Debug.Log("ShowPiecesRight-----------------------------------------------------------");

    }
    //下面左右上下没对上是因为横纵坐标问题，没对上才是对的
    public void ShowPiecesDown()
    {
        Debug.Log("ShowPiecesDown-----------------------------------------------------------");

        BoardManager board = GameManager.Instance.boardManager;
        for (int i = 0; i < board.boardLength; i++)
        {
            string tmp = "";
            for (int j = 0; j < board.boardLength; j++)
            {
                if (board.pieces[j, board.boardLength - 1 - i].isValid)
                    tmp += (board.pieces[j, board.boardLength - 1 - i].isLeftHasPiece + "   ");
                else
                    tmp += "        ";
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
                if (board.pieces[j, board.boardLength - 1 - i].isValid)
                    tmp += (board.CheckIsCanGoUp(j, board.boardLength - 1 - i) + "   ");
                else
                    tmp += "        ";
            }
            Debug.Log(tmp);
        }
        Debug.Log("ShowIsCanGoUp-----------------------------------------------------------");
    }
}
