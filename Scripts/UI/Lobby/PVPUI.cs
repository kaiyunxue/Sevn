using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PVPUI : LobbyUIBase
{
    [SerializeField]
    private Button btnLeft;
    void Start()
    {
        btnLeft.onClick.AddListener(delegate ()
        {
            OnClickLeft();
        });
    }
}
