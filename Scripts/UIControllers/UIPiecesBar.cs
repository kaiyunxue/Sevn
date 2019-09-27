using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPiecesBar : MonoBehaviour
{
    public UIPiece prefab;
    public List<UIPiece> pieces;
    public void Init(int length,int idx, Color piecesColor, PrefabsConfig pConfig)
    {
        for(int i = 0; i < length + 1; i++)
        {
            if(i == 0)
            {
                UIPiece p = GameObject.Instantiate(prefab);
                p.transform.SetParent(transform);
                p.SetHeader(pConfig.UIScoreHeader[idx]);
                pieces.Add(p);
            }
            else
            {
                UIPiece p = GameObject.Instantiate(prefab);
                p.transform.SetParent(transform);
                p.Init(piecesColor, pConfig.UIScoreTextures_0[idx], pConfig.UIScoreTextures_1[idx], pConfig.UIScoreBlack, pConfig.UIScoreGrey);
                pieces.Add(p);
            }
        }
    }
}
