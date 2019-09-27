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
        uIScorePanel.SetScore((int)color, record.secord[(int)color],this == GameManager.Instance.controller);
    }
    public void Init(int maxColor, UIScorePanel p)
    {
        record.secord = new int[maxColor];
        uIScorePanel = p;
    }
    public bool CheckDoesGetSevn()
    {
        foreach (int s in record.secord)
        {
            if (s >= 7)
            {
                return true;
            }
        }
        return false;
    }
}
