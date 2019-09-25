using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null)
            return;
        GetComponent<Image>().fillAmount = GameManager.Instance.timer.GetTurnTimeRemainPercent();
    }
}
