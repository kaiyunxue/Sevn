using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FanStatus : MonoBehaviour
{
    [SerializeField]
    private GameObject activedObj;
    [SerializeField]
    private GameObject deactivedObj;

    public void Activate()
    {
        activedObj.SetActive(true);
        deactivedObj.SetActive(false);
    }

    public void Deactivate()
    {
        activedObj.SetActive(false);
        deactivedObj.SetActive(true);
    }
}
