using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [SerializeField] GameObject SkillCharged;
    [SerializeField] GameObject SkillUncharge;
    [SerializeField] GameObject SkillCharge1;
    [SerializeField] GameObject SkillCharge2;
    [SerializeField] GameObject SkillCharge3;
    int round = 0;
    bool isUsed = false;
    public void UpdateSkillButton()
    {
        if (isUsed) return;
        int status = (round / 2) % 3;
        if (round >= 6)
            status = 2;
        Debug.Log("" + status);
        switch (status)
        {
            case 0:
                //GetComponent<Image>().fillAmount = 1.0f / 3;
                GetComponent<Button>().enabled = false;
                SkillCharged.SetActive(false);
                SkillUncharge.SetActive(true);
                SkillCharge1.SetActive(true);
                SkillCharge2.SetActive(false);
                SkillCharge3.SetActive(false);
                break;
            case 1:
                //GetComponent<Image>().fillAmount = 1.0f / 3;
                GetComponent<Button>().enabled = false;
                SkillCharged.SetActive(false);
                SkillUncharge.SetActive(true);
                SkillCharge1.SetActive(true);
                SkillCharge2.SetActive(true);
                SkillCharge3.SetActive(false);
                break;
            case 2:
                //GetComponent<Image>().fillAmount = 1.0f / 3;
                GetComponent<Button>().enabled = true;
                SkillCharged.SetActive(true);
                SkillUncharge.SetActive(false);
                SkillCharge1.SetActive(true);
                SkillCharge2.SetActive(true);
                SkillCharge3.SetActive(true);
                break;
        }
        round++;
    }
    public void OnClick()
    {
        GameManager.Instance.aiController.MakeAIStupid();
        round = 0;
        SkillCharged.SetActive(false);
        SkillUncharge.SetActive(true);
        SkillCharge1.SetActive(false);
        SkillCharge2.SetActive(false);
        SkillCharge3.SetActive(false);
        isUsed = true;
        UpdateSkillButton();
    }
}
