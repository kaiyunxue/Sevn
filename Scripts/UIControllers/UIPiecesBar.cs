using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPiecesBar : MonoBehaviour
{
    public UIPiece prefab;
    public List<UIPiece> pieces;
    public void Init(int length, Color piecesColor)
    {
        for(int i = 0; i < length; i++)
        {
            UIPiece p = GameObject.Instantiate(prefab);
            p.transform.SetParent(transform);
            p.Init(piecesColor);
            pieces.Add(p);
        }
    }
}
