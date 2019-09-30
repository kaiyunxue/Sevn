using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ScorePieceState
{
    Player1,
    Player2,
}
public class UIPiece : MonoBehaviour
{
    Color myColor;
    [SerializeField] GameObject border;
    [SerializeField] GameObject image;
    [SerializeField] GameObject imageFailed;
    public void SetHeader(Sprite header)
    {
        GetComponent<Image>().sprite = header;
    }
    public void Init(Color c, int length, int idx)
    {
        if (idx == length / 2 + 1)
        {
            border.SetActive(true);
        }
        if (idx > 0)
        {
            image.SetActive(false);
        }
        image.GetComponent<Image>().color = c;
    }
    public void InitEffect()
    {
        //todo
    }
    public void SetScore(ScorePieceState status)
    {
        switch(status)
        {
            case ScorePieceState.Player1:
                imageFailed.SetActive(false);
                break;
            case ScorePieceState.Player2:
                imageFailed.SetActive(true);
                break;
            default:
                break;
        }
        image.SetActive(true);
    }
    public void Disappear()
    {
        StartCoroutine(GoDisappear());
    }
    IEnumerator GoDisappear()
    {
        while(GetComponent<Image>().color.a > 0)
        {
            Color c = GetComponent<Image>().color;
            c.a -= Time.fixedDeltaTime;
            GetComponent<Image>().color = c;
            yield return new WaitForEndOfFrame();
        }
    }
}
