using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public UIScorePanel panels;
    public SkillButton sBtn;
    public void Init(int length, PrefabsConfig pConfig)
    {
        panels.Init(length, pConfig);
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
}
