using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMain : LobbyUIBase
{
    private int leftIdx;
    private int rightIdx;
    [SerializeField]
    private ButtonScript btnLeft;
    [SerializeField]
    private ButtonScript btnRight;
    [SerializeField]
    private LobbyCircle lobbyCircleLeft;
    [SerializeField]
    private LobbyCircle lobbyCircleRight;
    // Start is called before the first frame update
    void Start()
    {
        btnLeft.onSlightClick.AddListener(delegate ()
        {
            Debug.Log("btnLeft.onSlightClick");
            SetNextIdxLeft(lobbyCircleLeft.GetSelectIdx());
            OnClickLeft();
        });

        btnRight.onSlightClick.AddListener(delegate ()
        {
            Debug.Log("btnRight.onSlightClick");
            Debug.Log("lobbyCircleRight.GetSelectIdx(): " + lobbyCircleRight.GetSelectIdx());
            SetNextIdxRihgt(lobbyCircleRight.GetSelectIdx());
            OnClickRight();
        });
    }
}
