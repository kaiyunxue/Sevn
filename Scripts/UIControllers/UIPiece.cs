using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ScorePieceState
{
    Less,
    More,
    Grey,
    Black
}
public class UIPiece : MonoBehaviour
{
    Color myColor;
    [SerializeField] Sprite texture0;
    [SerializeField] Sprite texture1;
    [SerializeField] Sprite textureB;
    [SerializeField] Sprite textureG;
    public void SetHeader(Sprite header)
    {
        GetComponent<Image>().sprite = header;
    }
    public void Init(Color c, Sprite texture0, Sprite texture1, Sprite textureB, Sprite textureG)
    {
        c.a = 0.3f;
        myColor = c;
        GetComponent<Image>().color = c;
        this.texture0 = texture0;
        this.texture1 = texture1;
        this.textureB = textureB;
        this.textureG = textureG;
    }
    public void InitEffect()
    {
        //todo
    }
    public void SetScore(ScorePieceState status)
    {
        switch(status)
        {
            case ScorePieceState.Black:
                GetComponent<Image>().sprite = textureB;
                break;
            case ScorePieceState.Grey:
                GetComponent<Image>().sprite = textureG;
                break;
            case ScorePieceState.Less:
                GetComponent<Image>().sprite = texture0;
                break;
            case ScorePieceState.More:
                GetComponent<Image>().sprite = texture1;
                break;
            default:
                break;
        }
    }
}
