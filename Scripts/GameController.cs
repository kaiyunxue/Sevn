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

    public void GetScore(PieceColor color)
    {
        record.secord[(int)color]++;
    }
    public void Init(int maxColor)
    {
        record.secord = new int[maxColor];
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
