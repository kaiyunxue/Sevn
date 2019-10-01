using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIResult : MonoBehaviour
{
    public Button buttonToNextScene;
    public Button buttonToLobby;
    public Button buttonContinue;
    public Sprite spriteWin;
    public Sprite spriteFail;
    public Image spriteBackground;
    public Image awardImage;

    public GameObject buttonGroup;
    public GameObject resultPanel;
    public GameObject resultAwardPanel;


    public Animator awardAnimator;
    private int nextLevel;
    public Sprite[] resultAwards;
    public void Init(bool isWin = true)
    {
        if (isWin)
        {
            Debug.Log("Win");
            spriteBackground.sprite = spriteWin;
            int curLevel = int.Parse(CacheService.Get("iCurrentLevelID"));
            nextLevel = curLevel;
            if (curLevel < 3)
            {
                nextLevel++;
            }
            ShowAward();

            string iCurrentLevelID = CacheService.Get("iCurrentLevelID");
            string uid = CacheService.Get("uid");
            if (uid != null && iCurrentLevelID != null && GameManager.Instance.gamePlayMode.gameMode == GameMode.VSAI)
            {
                WWWForm form = new WWWForm();
	            form.AddField("uid", uid);
	            form.AddField("iCurrentLevelID", int.Parse(iCurrentLevelID));
	            form.AddField("bWin", 1);
                StartCoroutine(NetService.SendMessage(Message.MSG_ID.MSG_ID_RESULT, form));
            }
            CacheService.Set("iCurrentLevelID", (int.Parse(iCurrentLevelID) + 1).ToString());
        }
        else
        {
            Debug.Log("Fail");
            spriteBackground.sprite = spriteFail;
            int curLevel = int.Parse(CacheService.Get("iCurrentLevelID"));
            nextLevel = curLevel;
            buttonGroup.SetActive(true);
            ShowResult();

            string iCurrentLevelID = CacheService.Get("iCurrentLevelID");
            string uid = CacheService.Get("uid");
            if (uid != null && iCurrentLevelID != null && GameManager.Instance.gamePlayMode.gameMode == GameMode.VSAI)
            {
	            WWWForm form = new WWWForm();
	            form.AddField("uid", uid);
	            form.AddField("iCurrentLevelID", int.Parse(iCurrentLevelID));
	            form.AddField("bWin", 0);
	            StartCoroutine(NetService.SendMessage(Message.MSG_ID.MSG_ID_RESULT, form));
            }
        }
        buttonToNextScene.onClick.AddListener(new UnityAction(TurnToNextScene));
        buttonToLobby.onClick.AddListener(new UnityAction(TurnToLobby));
        buttonContinue.onClick.AddListener(new UnityAction(OnClickContinue));
    }
    void TurnToLobby()
    {
        SceneManager.LoadSceneAsync("LobbyScene").allowSceneActivation = true;
    }
    void TurnToThisScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void TurnToNextScene()
    {
        CacheService.Set("playMode", "PVE");
        CacheService.Set("iCurrentLevelID", nextLevel.ToString());
        SceneManager.LoadSceneAsync("MainScene").allowSceneActivation = true;
    }
    void ShowAward()
    {
        resultAwardPanel.SetActive(true);
        resultPanel.SetActive(false);
        awardAnimator.SetTrigger("fadeIn");
    }
    void ShowResult()
    {
        Debug.Log("ShowResult");
        resultAwardPanel.SetActive(false);
        resultPanel.SetActive(true);
        awardAnimator.SetTrigger("resultFadeIn");
    }

    void OnClickContinue()
    {
        awardAnimator.SetTrigger("fadeOut");
    }
}
