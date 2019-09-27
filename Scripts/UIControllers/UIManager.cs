using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UIScorePanel panels;
    public void Init(int length, PrefabsConfig pConfig)
    {
        panels.Init(length, pConfig);
    }
}
