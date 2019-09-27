using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public GameObject backgroundInst;
    public float slideSpeed;
    [Serializable]
    private struct UIConfig
    {
        [SerializeField]
        public int order;
        [SerializeField]
        public int idx;
        [SerializeField]
        public GameObject prefab;
    }
    [SerializeField]
    private UIConfig[] uiConfig;

    private MultiDictionary<int, int, GameObject> prefabMap;
    private GameObject curUIInst;
    private int curUIOrder;
    private int curUIIdx;
    // Start is called before the first frame update
    void Start()
    {
        prefabMap = new MultiDictionary<int, int, GameObject>();
        foreach (UIConfig cfg in uiConfig)
        {
            prefabMap.Set(cfg.order, cfg.idx, cfg.prefab);
        }
        curUIOrder = 0;
        curUIIdx = 0;
        GameObject prefab = prefabMap.Get(curUIOrder, curUIIdx);
        curUIInst = Instantiate(prefab);
        curUIInst.transform.SetParent(gameObject.transform);
        curUIInst.GetComponent<LobbyUIBase>().SetLobbyInst(this);
    }

    public void OnClickLeft(int idx)
    {
        int nextUIOrder = curUIOrder - 1;
        GameObject prefab = prefabMap.Get(nextUIOrder, idx);
        if (prefab == null)
        {
            return;
        }

        GameObject nextUIInst = Instantiate(prefab);
        SlideInOutComponent curUIInstSlideComponent = curUIInst.GetComponent<SlideInOutComponent>();
        SlideInOutComponent nextUIInstSlideComponent = nextUIInst.GetComponent<SlideInOutComponent>();
        SlideInOutComponent backgroundInstSlideComponent = backgroundInst.GetComponent<SlideInOutComponent>();
        if (curUIInstSlideComponent == null || nextUIInstSlideComponent == null)
        {
            return;
        }

        nextUIInst.transform.SetParent(gameObject.transform);
        nextUIInst.GetComponent<LobbyUIBase>().SetLobbyInst(this);

        curUIInstSlideComponent.SlideOutLeft(slideSpeed, true);
        backgroundInstSlideComponent.SlideOutLeft(slideSpeed, false);
        nextUIInstSlideComponent.SlideInLeft(slideSpeed);

        curUIOrder = nextUIOrder;
        curUIIdx = idx;
        curUIInst = nextUIInst;
    }

    public void OnClickRight(int idx)
    {
        int nextUIOrder = curUIOrder + 1;
        GameObject prefab = prefabMap.Get(nextUIOrder, idx);
        if (prefab == null)
        {
            return;
        }

        GameObject nextUIInst = Instantiate(prefab);
        SlideInOutComponent curUIInstSlideComponent = curUIInst.GetComponent<SlideInOutComponent>();
        SlideInOutComponent nextUIInstSlideComponent = nextUIInst.GetComponent<SlideInOutComponent>();
        SlideInOutComponent backgroundInstSlideComponent = backgroundInst.GetComponent<SlideInOutComponent>();
        if (curUIInstSlideComponent == null || nextUIInstSlideComponent == null)
        {
            return;
        }

        nextUIInst.transform.SetParent(gameObject.transform);
        nextUIInst.GetComponent<LobbyUIBase>().SetLobbyInst(this);

        curUIInstSlideComponent.SlideOutRight(slideSpeed, true);
        backgroundInstSlideComponent.SlideOutRight(slideSpeed, false);
        nextUIInstSlideComponent.SlideInRight(slideSpeed);

        curUIOrder = nextUIOrder;
        curUIIdx = idx;
        curUIInst = nextUIInst;
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("MainScene").allowSceneActivation = true;
    }
}
