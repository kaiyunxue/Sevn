using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIBase : MonoBehaviour
{
    protected Lobby lobby;
    private int nextIdxLeft;
    private int nextIdxRight;

    void Start()
    {
        nextIdxLeft = 0;
        nextIdxRight = 0;
    }

    protected void SetNextIdxLeft(int idx) { nextIdxLeft = idx; }

    protected void SetNextIdxRihgt(int idx) { nextIdxRight = idx; }

    protected void OnClickLeft() { lobby.OnClickLeft(nextIdxLeft); }

    protected void OnClickRight() { lobby.OnClickRight(nextIdxRight); }

    public void SetLobbyInst(Lobby inst) { lobby = inst; }
}
