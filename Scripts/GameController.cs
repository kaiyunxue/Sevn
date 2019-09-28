using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerRecord
{
    public int[] secord;
    public PlayerRecord Clone()
    {
        PlayerRecord res = new PlayerRecord();
        res.secord = new int[secord.GetLength(0)];
        for (int i = 0; i < secord.GetLength(0); i++)
        {
            res.secord[i] = secord[i];
        }
        return res;
    }
}
public class GameController : MonoBehaviour
{
    public PlayerRecord record;
    public UIScorePanel uIScorePanel;

    public void GetScore(PieceColor color)
    {
        record.secord[(int)color]++;
        uIScorePanel.SetScore((int)color, record.secord[(int)color],this == GameManager.Instance.controller);
    }
    public void Init(int maxColor, UIScorePanel p)
    {
        record.secord = new int[maxColor];
        uIScorePanel = p;
    }
    public bool CheckDoesGetSevn()
    {
        GamePlayMode gpm = GameManager.Instance.gamePlayMode;
        foreach (int s in record.secord)
        {
            if (s >= gpm.boardSideLength)
            {
                return true;
            }
        }
        return false;
    }
    public PlayerRecord GetScoreValue()
    {
        return record.Clone();
    }
    public int GetControledColorNum()
    {
        int controledNum = 0;
        foreach(var v in record.secord)
        {
            GamePlayMode gpm = GameManager.Instance.gamePlayMode;
            if (v > gpm.boardSideLength / 2)
            {
                ++controledNum;
            }
        }
        return controledNum;
    }
}
