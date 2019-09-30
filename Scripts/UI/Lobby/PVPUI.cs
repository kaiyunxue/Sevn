using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PVPUI : LobbyUIBase
{
    [SerializeField] Button buttonPlay;
    protected override void Start()
    {
        base.Start();
        buttonPlay.onClick.AddListener(delegate ()
        {
            OnClickPlay();
        });
    }

    void OnClickPlay()
    {
        CacheService.Set("playMode", "PVP");
        lobby.PlayGame();
    }
}
