using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public void UpdateSkillButton(int turn)
    {
        int status = (turn / 2) % 3;
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
    }
    public void OnClick()
    {
        GameManager.Instance.aiController.MakeAIStupid();
    }
}
