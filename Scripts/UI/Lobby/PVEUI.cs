using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PVEUI : LobbyUIBase
{
    [SerializeField]
    private Button btnPlayLevel0;
    [SerializeField]
    private Button btnPlayLevel1;
    [SerializeField]
    private Button btnPlayLevel2;
    [SerializeField]
    private Button btnPlayLevel3;

    protected override void Start()
    {
        base.Start();
        string iGameLevel = CacheTool.Get("iGameLevel");
        Debug.Log(iGameLevel);
        switch (iGameLevel)
        {
            case "3":
                btnPlayLevel1.enabled = true;
                btnPlayLevel2.enabled = true;
                btnPlayLevel3.enabled = true;
                break;
            case "2":
                btnPlayLevel1.enabled = true;
                btnPlayLevel2.enabled = true;
                btnPlayLevel3.enabled = false;
                btnPlayLevel3.GetComponentInChildren<Text>().text = "Level 3（未开启）";
                break;
            case "1":
                btnPlayLevel1.enabled = true;
                btnPlayLevel2.enabled = false;
                btnPlayLevel2.GetComponentInChildren<Text>().text = "Level 2（未开启）";
                btnPlayLevel3.enabled = false;
                btnPlayLevel3.GetComponentInChildren<Text>().text = "Level 3（未开启）";
                break;
            case "0":
                btnPlayLevel1.enabled = false;
                btnPlayLevel1.GetComponentInChildren<Text>().text = "Level 1（未开启）";
                btnPlayLevel2.enabled = false;
                btnPlayLevel2.GetComponentInChildren<Text>().text = "Level 2（未开启）";
                btnPlayLevel3.enabled = false;
                btnPlayLevel3.GetComponentInChildren<Text>().text = "Level 3（未开启）";
                break;
            default:
                btnPlayLevel1.enabled = false;
                btnPlayLevel1.GetComponentInChildren<Text>().text = "Level 1（未开启）";
                btnPlayLevel2.enabled = false;
                btnPlayLevel2.GetComponentInChildren<Text>().text = "Level 2（未开启）";
                btnPlayLevel3.enabled = false;
                btnPlayLevel3.GetComponentInChildren<Text>().text = "Level 3（未开启）";
                break;
        }
        btnPlayLevel0.onClick.AddListener(delegate ()
        {
            OnClickPlayLevel0();
        });
        btnPlayLevel1.onClick.AddListener(delegate ()
        {
            OnClickPlayLevel1();
        });
        btnPlayLevel2.onClick.AddListener(delegate ()
        {
            OnClickPlayLevel2();
        });
        btnPlayLevel3.onClick.AddListener(delegate ()
        {
            OnClickPlayLevel3();
        });

    }
    private void OnClickPlayLevel0()
    {
        CacheService.Set("playMode", "PVE");
        CacheService.Set("iCurrentLevelID", "0");
        lobby.PlayGame();
    }

    private void OnClickPlayLevel1()
    {
        CacheService.Set("playMode", "PVE");
        CacheService.Set("iCurrentLevelID", "1");
        lobby.PlayGame();
    }

    private void OnClickPlayLevel2()
    {
        CacheService.Set("playMode", "PVE");
        CacheService.Set("iCurrentLevelID", "2");
        lobby.PlayGame();
    }

    private void OnClickPlayLevel3()
    {
        CacheService.Set("playMode", "PVE");
        CacheService.Set("iCurrentLevelID", "3");
        lobby.PlayGame();
    }
}
