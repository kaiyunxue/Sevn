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
            b.Init(length,i, pConfig.UIcolors[i],pConfig);
            bars.Add(b);
        }
    }
    public void SetScore(int color, int score, bool isPlayer1)
    {
        int n = bars[color].pieces.Count;
        if(isPlayer1)
        {
            if(score < 4)
            {
                for(int i = 0; i < score; i++)
                {
                    bars[color].pieces[i].SetScore(ScorePieceState.Less);
                }
            }
            else
            {
                for (int i = 0; i < score; i++)
                {
                    bars[color].pieces[i].SetScore(ScorePieceState.More);
                }
            }
        }
        else
        {
            if (score < 4)
            {
                for (int i = 0; i < score; i++)
                {
                    bars[color].pieces[n - i - 1].SetScore(ScorePieceState.Grey);
                }
            }
            else
            {
                for (int i = 0; i < score; i++)
                {
                    bars[color].pieces[n - i - 1].SetScore(ScorePieceState.Black);
                }
            }
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
