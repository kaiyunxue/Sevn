using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public struct GallerySpriteCfgStruct
{
    public Sprite lockedSprite;
    public Sprite unlockedSprite;
    public Sprite maximumSprite;
}
public class GalleryUI : LobbyUIBase
{
    public GallerySpriteCfgStruct[] gallerySpriteCfg;
    public Button[] btnGalleryLevel;
    public GameObject maximumSpritePanel;
    public Image maximumSprite;
    public Button btnCloseMaximumSpritePanel;
    Dictionary<string, int> galleryMap;
    protected override void Start()
    {
        base.Start();

        galleryMap = new Dictionary<string, int>();
        string iGameLevel = CacheTool.Get("iGameLevel");
        for (int i = 0; i < btnGalleryLevel.GetLength(0); ++i)
        {
            if (i < int.Parse(iGameLevel))
            {
                btnGalleryLevel[i].GetComponent<Image>().sprite = gallerySpriteCfg[i].unlockedSprite;
                btnGalleryLevel[i].GetComponent<Button>().enabled = true;
                btnGalleryLevel[i].GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    OnClickGalleryBtn();
                });
                galleryMap.Add(btnGalleryLevel[i].name, i);
            }
            else
            {
                btnGalleryLevel[i].GetComponent<Image>().sprite = gallerySpriteCfg[i].lockedSprite;
                btnGalleryLevel[i].GetComponent<Button>().enabled = false;
            }

        }
        btnCloseMaximumSpritePanel.onClick.AddListener(OnClickCloseMaximumSpritePanel);
    }

    private void OnClickGalleryBtn()
    {
        string btnName = EventSystem.current.currentSelectedGameObject.name;
        int level = galleryMap[btnName];

        maximumSpritePanel.SetActive(true);
        maximumSprite.sprite = gallerySpriteCfg[level].maximumSprite;
    }

    private void OnClickCloseMaximumSpritePanel()
    {
        maximumSpritePanel.SetActive(false);
    }
}
