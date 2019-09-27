using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerRecord
{
    public int[] secord;
}
public class GameController : MonoBehaviour
{
    public PlayerRecord record;
    public UIScorePanel uIScorePanel;

    public void GetScore(PieceColor color)
    {
        record.secord[(int)color]++;
        uIScorePanel.SetScore((int)color, record.secord[(int)color]);
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
