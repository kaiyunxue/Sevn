using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct RoundTipsStruct
{
    public Sprite player1Turn;
    public Sprite player2Turn;
}
public class UIManager : MonoBehaviour
{
    [Header("PVE")]
    public RoundTipsStruct roundSpritePVE;
    [Header("PVP")]
    public RoundTipsStruct roundSpritePVP;

    public UIScorePanel panels;
    public SkillButton sBtn;
    public UIResult resultUI;
    public Image player1TurnImage;
    public Image player2TurnImage;
    public void SetRound(int round)
    {
        if (round % 2 == 0)
        {
            player1TurnImage.gameObject.SetActive(true);
            player2TurnImage.gameObject.SetActive(false);
        }
        else
        {
            player1TurnImage.gameObject.SetActive(false);
            player2TurnImage.gameObject.SetActive(true);
        }
    }
    public void Init(int length, PrefabsConfig pConfig)
    {
        resultUI.gameObject.SetActive(false);
        panels.Init(length, pConfig);
        GamePlayMode gpm = GameManager.Instance.gamePlayMode;
        switch (gpm.gameMode)
        {
            case GameMode.VSAI:
                player1TurnImage.sprite = roundSpritePVE.player1Turn;
                player2TurnImage.sprite = roundSpritePVE.player2Turn;
                break;
            default:
                player1TurnImage.sprite = roundSpritePVP.player1Turn;
                player2TurnImage.sprite = roundSpritePVP.player2Turn;
                break;
        }
        SetRound(0);
        if (!gpm.doUseSkill)
        {
            sBtn.gameObject.SetActive(false);
        }
        OnGameStart();
    }
    void OnGameStart()
    {
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image i in images)
        {
            StartCoroutine(GoAppear(i));
        }
    }
    public void ActionsOnGameEnd()
    {
        Image[] images = GetComponentsInChildren<Image>();
        //懒
        sBtn.GetComponent<Button>().enabled = false;
        foreach(Image i in images)
        {
            StartCoroutine(GoDisappear(i));
        }
        StartCoroutine(ShowResult());
    }
    IEnumerator GoDisappear(Image image)
    {
        while(image.color.a > 0)
        {
            Color c = image.color;
            c.a -= Time.deltaTime;
            image.color = c;
            yield return new WaitForEndOfFrame();
        }
        if(image.gameObject != gameObject)
        {
            image.gameObject.SetActive(false);
        }
    }
    IEnumerator GoAppear(Image image)
    {
        Color c = image.color;
        c.a = 0;
        image.color = c;
        while (image.color.a < 1)
        {
            c = image.color;
            c.a += Time.deltaTime;
            image.color = c;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(1);
        resultUI.gameObject.SetActive(true);
        resultUI.Init(GameManager.Instance.isWin);
    }
}
