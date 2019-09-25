using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UIScorePanel[] panels;
    public void Init(GamePlayMode gpm)
    {
        panels[0].Init(gpm);
        panels[1].Init(gpm);
    }
}
