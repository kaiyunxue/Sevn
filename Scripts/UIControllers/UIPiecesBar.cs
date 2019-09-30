using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPiecesBar : MonoBehaviour
{
    public Image head;
    public Transform bar;
    public UIPiece prefab;
    public List<UIPiece> pieces;
    public void Init(int length,int idx, Color piecesColor, PrefabsConfig pConfig)
    {
        for(int i = 0; i < length + 1; i++)
        {
            if(i == 0)
            {
                head.sprite = pConfig.UIScoreHeader[idx];
            }
            else
            {
                UIPiece p = GameObject.Instantiate(prefab);
                p.transform.SetParent(bar);
                p.Init(piecesColor, pConfig.UIScoreTextures_0[idx], pConfig.UIScoreTextures_1[idx], pConfig.UIScoreBlack, pConfig.UIScoreGrey);
                pieces.Add(p);
            }
        }
    }
    public void Disappear()
    {
        foreach(var p in pieces)
        {
            p.Disappear();
        }
    }
}
