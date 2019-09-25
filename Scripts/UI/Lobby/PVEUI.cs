using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PVEUI : LobbyUIBase
{
    [SerializeField]
    private Button btnLeft;
    [SerializeField]
    private Button btnPlay;
    void Start()
    {
        btnLeft.onClick.AddListener(delegate ()
        {
            Debug.Log("PVEUI OnClickLeft");
            OnClickLeft();
        });

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
