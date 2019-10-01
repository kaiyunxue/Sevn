using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

[System.Serializable]
public struct LevelConfigStruct
{
    public Sprite unlockedSprite;
    public Sprite lockedSprite;
    public VideoClip videoClip;
}
public class PVEUI : LobbyUIBase
{
    [SerializeField]
    private GameObject[] btnPlayLevel;
    [SerializeField] LevelConfigStruct[] levelConfig;
    [SerializeField] VideoComponent videoComponent;
    Dictionary<string, int> buttonMap;
    protected override void Start()
    {
        base.Start();
        buttonMap = new Dictionary<string, int>();
        string iGameLevel = CacheTool.Get("iGameLevel");
        Debug.Log(iGameLevel);
        for (int i = 0; i < btnPlayLevel.GetLength(0); ++i)
        {
            if (i <= int.Parse(iGameLevel))
            {
                btnPlayLevel[i].GetComponent<Image>().sprite = levelConfig[i].unlockedSprite;
                btnPlayLevel[i].GetComponent<Button>().enabled = true;
                btnPlayLevel[i].GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    OnClickPlayLevel();
                });
                buttonMap.Add(btnPlayLevel[i].name, i);
            }
            else
            {
                btnPlayLevel[i].GetComponent<Image>().sprite = levelConfig[i].lockedSprite;
                btnPlayLevel[i].GetComponent<Button>().enabled = false;
            }

        }
    }

    void OnClickPlayLevel()
    {
        string btnName = EventSystem.current.currentSelectedGameObject.name;
        int level = buttonMap[btnName];
        Debug.Log("OnClickPlayLevel(int level) level: " + level);
        UnityAction PlayGame = new UnityAction(delegate ()
        {
            CacheService.Set("playMode", "PVE");
            CacheService.Set("iCurrentLevelID", level.ToString());
            lobby.PlayGame();
        });
        if (levelConfig[level].videoClip != null)
        {
            videoComponent.gameObject.SetActive(true);
            videoComponent.PlayVideo(levelConfig[level].videoClip, PlayGame);
            GameObject mainCamera = GameObject.Find("Main Camera");
            AudioSource audioSource = mainCamera.GetComponent<AudioSource>();
            audioSource.Pause();
        }
        else
        {
            PlayGame();
        }
    }
}
