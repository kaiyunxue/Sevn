using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIBase : MonoBehaviour
{
    protected Lobby lobby;
    private int nextIdxLeft;
    private int nextIdxRight;
    [SerializeField]
    protected GraphicRaycaster raycaster;
    [SerializeField]
    protected LobbyCircle lobbyCircleLeft;
    [SerializeField]
    protected LobbyCircle lobbyCircleRight;

    protected virtual void Start()
    {
        nextIdxLeft = 0;
        nextIdxRight = 0;

        if (lobbyCircleLeft != null)
        {
            lobbyCircleLeft.SetClickEvent(delegate ()
            {
                SetNextIdxLeft(lobbyCircleLeft.GetSelectIdx());
                OnClickLeft();
            });
        }

        if (lobbyCircleRight != null)
        {
            lobbyCircleRight.SetClickEvent(delegate ()
            {
                SetNextIdxRihgt(lobbyCircleRight.GetSelectIdx());
                OnClickRight();
            });
        }
    }

    protected void SetNextIdxLeft(int idx) { nextIdxLeft = idx; }

    protected void SetNextIdxRihgt(int idx) { nextIdxRight = idx; }

    protected void OnClickLeft() { lobby.OnClickLeft(nextIdxLeft); }

    protected void OnClickRight() { lobby.OnClickRight(nextIdxRight); }

    public void SetLobbyInst(Lobby inst) { lobby = inst; }

    public void OnSlideEnd()
    {
        if (raycaster != null)
        {
            raycaster.enabled = true;
        }
        if (lobbyCircleLeft != null)
        {
            lobbyCircleLeft.ShowFanPanel();
        }
        if (lobbyCircleRight != null)
        {
            lobbyCircleRight.ShowFanPanel();
        }
    }

    public void OnSlideBegin()
    {
        if (raycaster != null)
        {
            raycaster.enabled = false;
        }
        if (lobbyCircleLeft != null)
        {
            lobbyCircleLeft.HideFanPanel();
        }
        if (lobbyCircleRight != null)
        {
            lobbyCircleRight.HideFanPanel();
        }
    }
}
