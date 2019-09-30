using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct LevelSpriteStruct
{
    public Sprite unlocked;
    public Sprite locked;
}
public class PVEUI : LobbyUIBase
{
    [SerializeField]
    private GameObject[] btnPlayLevel;
    public LevelSpriteStruct[] levelSprite;

    protected override void Start()
    {
        base.Start();
        string iGameLevel = CacheTool.Get("iGameLevel");
        Debug.Log(iGameLevel);
        for (int i = 0; i < btnPlayLevel.GetLength(0); ++i)
        {
            if (i <= int.Parse(iGameLevel))
            {
                btnPlayLevel[i].GetComponent<Image>().sprite = levelSprite[i].unlocked;
                btnPlayLevel[i].GetComponent<Button>().enabled = true;
            }
            else
            {
                btnPlayLevel[i].GetComponent<Image>().sprite = levelSprite[i].locked;
                btnPlayLevel[i].GetComponent<Button>().enabled = false;
            }
        }
        //switch (iGameLevel)
        //{
        //    case "3":
        //        for (int i = 0; i < btnPlayLevel.GetLength(0); ++i)
        //        {
        //            if (i <= int.Parse(iGameLevel))
        //            {
        //                btnPlayLevel[i].GetComponent<Image>().sprite = levelSprite[i].unlocked;
        //                btnPlayLevel[1].GetComponent<Button>().enabled = true;
        //            }
        //            else
        //            {
        //                btnPlayLevel[i].GetComponent<Image>().sprite = levelSprite[i].locked;
        //                btnPlayLevel[1].GetComponent<Button>().enabled = false;
        //            }
        //        }
        //        break;
        //    case "2":
        //        for (int i = 0; i < btnPlayLevel.GetLength(0); ++i)
        //        {
        //            if (i < 3)
        //            {
        //                btnPlayLevel[i].GetComponent<Image>().sprite = levelSprite[i].unlocked;
        //                btnPlayLevel[1].GetComponent<Button>().enabled = true;
        //            }
        //            else
        //            {
        //                btnPlayLevel[i].GetComponent<Image>().sprite = levelSprite[i].locked;
        //                btnPlayLevel[1].GetComponent<Button>().enabled = false;
        //            }
        //        }
        //        break;
        //    case "1":
        //        for (int i = 0; i < btnPlayLevel.GetLength(0); ++i)
        //        {
        //            if (i < 2)
        //            {
        //                btnPlayLevel[i].GetComponent<Image>().sprite = levelSprite[i].unlocked;
        //                btnPlayLevel[1].GetComponent<Button>().enabled = true;
        //            }
        //            else
        //            {
        //                btnPlayLevel[i].GetComponent<Image>().sprite = levelSprite[i].locked;
        //                btnPlayLevel[1].GetComponent<Button>().enabled = false;
        //            }
        //        }
        //        break;
        //    case "0":
        //        for (int i = 0; i < btnPlayLevel.GetLength(0); ++i)
        //        {
        //            if (i < 1)
        //            {
        //                btnPlayLevel[i].GetComponent<Image>().sprite = levelSprite[i].unlocked;
        //                btnPlayLevel[1].GetComponent<Button>().enabled = true;
        //            }
        //            else
        //            {
        //                btnPlayLevel[i].GetComponent<Image>().sprite = levelSprite[i].locked;
        //                btnPlayLevel[1].GetComponent<Button>().enabled = false;
        //            }
        //        }
        //        break;
        //    default:
        //        for (int i = 0; i < btnPlayLevel.GetLength(0); ++i)
        //        {
        //            if (i < 1)
        //            {
        //                btnPlayLevel[i].GetComponent<Image>().sprite = levelSprite[i].unlocked;
        //                btnPlayLevel[1].GetComponent<Button>().enabled = true;
        //            }
        //            else
        //            {
        //                btnPlayLevel[i].GetComponent<Image>().sprite = levelSprite[i].locked;
        //                btnPlayLevel[1].GetComponent<Button>().enabled = false;
        //            }
        //        }
        //        break;
        ////}
        btnPlayLevel[0].GetComponent<Button>().onClick.AddListener(delegate ()
        {
            OnClickPlayLevel0();
        });
        btnPlayLevel[1].GetComponent<Button>().onClick.AddListener(delegate ()
        {
            OnClickPlayLevel1();
        });
        btnPlayLevel[2].GetComponent<Button>().onClick.AddListener(delegate ()
        {
            OnClickPlayLevel2();
        });
        btnPlayLevel[3].GetComponent<Button>().onClick.AddListener(delegate ()
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
