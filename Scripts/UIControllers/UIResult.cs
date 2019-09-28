using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIResult : MonoBehaviour
{
    public string nextSceneName;
    public string lobbySceneName;
    public Text text;
    public Button button1;
    public Button button2;
    public void Init(bool isWin = true)
    {
        if (isWin)
        {
            text.text = "胜利";
            button1.onClick.AddListener(new UnityAction(TurnToNextScene));
            button2.onClick.AddListener(new UnityAction(TurnToLobby));
        }
        else
        {
            text.text = "输了彩笔";
            button1.onClick.AddListener(new UnityAction(TurnToThisScene));
            button2.onClick.AddListener(new UnityAction(TurnToLobby));
        }
    }
    void TurnToLobby()
    {
        SceneManager.LoadScene(lobbySceneName);
    }
    void TurnToThisScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void TurnToNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
