using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public UIScorePanel panels;
    public SkillButton sBtn;
    public UIResult resultUI;
    public Text roundText;
    public void SetRound(int round)
    {
        GamePlayMode gpm = GameManager.Instance.gamePlayMode;
        switch (gpm.gameMode)
        {
            case GameMode.VSAI:
                if (round % 2 == 0)
                {
                    roundText.text = "你的回合";
                }
                else
                {
                    roundText.text = "对手回合";
                }
                break;
            case GameMode.OneClientTwoPlayers:
                if (round % 2 == 0)
                {
                    roundText.text = "玩家1的回合";
                }
                else
                {
                    roundText.text = "玩家2的回合";
                }
                break;
        }
    }
    public void Init(int length, PrefabsConfig pConfig)
    {
        resultUI.gameObject.SetActive(false);
        panels.Init(length, pConfig);
        GamePlayMode gpm = GameManager.Instance.gamePlayMode;
        SetRound(0);
        if (!gpm.doUseSkill)
        {
            sBtn.gameObject.SetActive(false);
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
    IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(1);
        resultUI.gameObject.SetActive(true);
        resultUI.Init(GameManager.Instance.isWin);
    }
}
