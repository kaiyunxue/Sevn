using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PVEUI : LobbyUIBase
{
    [SerializeField]
    private Button btnPlay;
    protected override void Start()
    {
        base.Start();
        btnPlay.onClick.AddListener(delegate ()
        {
            OnClickPlay();
        });
    }
    private void OnClickPlay()
    {
        lobby.PlayGame();
    }
}
