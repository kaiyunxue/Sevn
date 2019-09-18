using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScorePanel : MonoBehaviour
{
    public UIPiecesBar prefab;
    public List<UIPiecesBar> bars;
    public void Init(GamePlayMode gpm)
    {
        for(int i = 0; i < gpm.boardSideLength; i++)
        {
            UIPiecesBar b = GameObject.Instantiate(prefab);
            b.transform.SetParent(transform);
            b.Init(gpm.boardSideLength, gpm.colors[i]);
            bars.Add(b);
        }
    }
    public void SetScore(int color, int score)
    {
        int n = bars[color].pieces.Count;
        for(int i = 0; i < n; i++)
        {
            if(i < score)
            {
                bars[color].pieces[i].TurnOn();
            }
            else
            {
                bars[color].pieces[i].TurnOff();
            }
        }
    }
}
