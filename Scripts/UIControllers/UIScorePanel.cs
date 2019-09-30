using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScorePanel : MonoBehaviour
{
    public UIPiecesBar prefab;
    public List<UIPiecesBar> bars;
    public void Init(int length, PrefabsConfig pConfig)
    {
        for(int i = 0; i < length; i++)
        {
            UIPiecesBar b = GameObject.Instantiate(prefab);
            b.transform.SetParent(transform);
            b.Init(length, i, pConfig.UIColors[i], pConfig);
            bars.Add(b);
        }
    }
    public void SetScore(int color, int score, bool isPlayer1)
    {
        int n = bars[color].pieces.Count;
        if (isPlayer1)
        {
            bars[color].pieces[score - 1].SetScore(ScorePieceState.Player1);
        }
        else
        {
            bars[color].pieces[n - score].SetScore(ScorePieceState.Player2);
        }
    }

    public void Disappear()
    {
        foreach(var v in bars)
        {
            v.Disappear();
        }
    }
}
