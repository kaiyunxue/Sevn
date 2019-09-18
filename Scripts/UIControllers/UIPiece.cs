using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPiece : MonoBehaviour
{
    Color myColor;
    public void Init(Color c)
    {
        c.a = 0.3f;
        myColor = c;
        GetComponent<Image>().color = c;
    }
    public void InitEffect()
    {
        //todo
    }
    public void TurnOn()
    {
        myColor.a = 1f;
        GetComponent<Image>().color = myColor;
    }
    public void TurnOff()
    {
        myColor.a = 0.3f;
        GetComponent<Image>().color = myColor;
    }
}
