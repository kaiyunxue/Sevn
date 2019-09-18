using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public UIScorePanel[] panels;
    void Awake()
    {
        instance = this;
    }
    public void Init(GamePlayMode gpm)
    {
        panels[0].Init(gpm);
        panels[1].Init(gpm);
    }
}
