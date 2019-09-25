using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : LobbyUIBase
{
    [SerializeField]
    private Button btnRight;
    void Start()
    {
        btnRight.onClick.AddListener(delegate ()
        {
            OnClickRight();
        });
    }
}
