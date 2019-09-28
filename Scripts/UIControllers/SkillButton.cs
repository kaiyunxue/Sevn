using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    int round = 0;
    public void UpdateSkillButton()
    {
        int status = (round / 2) % 3;
        if (round >= 6)
            status = 2;
        Debug.Log("" + status);
        switch(status)
        {
            case 0:
                GetComponent<Button>().enabled = false;
                GetComponent<Image>().fillAmount = 1.0f / 3;
                break;
            case 1:
                GetComponent<Button>().enabled = false;
                GetComponent<Image>().fillAmount = 2.0f/ 3;
                break;
            case 2:
                GetComponent<Button>().enabled = true;
                GetComponent<Image>().fillAmount = 1;
                break;
        }
        round++;
    }
    public void OnClick()
    {
        GameManager.Instance.aiController.MakeAIStupid();
        round = 0;
    }
}
