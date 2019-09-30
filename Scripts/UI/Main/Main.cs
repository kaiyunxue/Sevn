using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private Button dialogQuitConfirmButton;
    [SerializeField]
    private Button dialogQuitCancelButton;
    [SerializeField]
    private GameObject dialog;
    // Start is called before the first frame update
    void Start()
    {
        quitButton.onClick.AddListener(delegate ()
        {
            OnClick_Button_back();
        });

        dialogQuitConfirmButton.onClick.AddListener(delegate ()
        {
            OnClick_Button_Dialog_Quit_Confirm();
        });

        dialogQuitCancelButton.onClick.AddListener(delegate ()
        {
            OnClick_Button_Dialog_Quit_Cancel();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick_Button_back()
    {
        dialog.SetActive(true);
    }

    public void OnClick_Button_Dialog_Quit_Confirm()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void OnClick_Button_Dialog_Quit_Cancel()
    {
        dialog.SetActive(false);
    }
}
