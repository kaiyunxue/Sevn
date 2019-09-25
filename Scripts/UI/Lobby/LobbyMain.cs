using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMain : LobbyUIBase
{
    private int leftIdx;
    private int rightIdx;
    [SerializeField]
    private Button btnLeft;
    [SerializeField]
    private Button btnRight;
    [SerializeField]
    private InputField inputTextLeft;
    [SerializeField]
    private InputField inputTextRight;
    // Start is called before the first frame update
    void Start()
    {
        btnLeft.onClick.AddListener(delegate ()
        {
            OnClickLeft();
        });

        btnRight.onClick.AddListener(delegate ()
        {
            OnClickRight();
        });

        inputTextLeft.onValueChanged.AddListener(delegate (string valueStr)
        {
            SetNextIdxLeft(int.Parse(valueStr));
        });

        inputTextRight.onValueChanged.AddListener(delegate (string valueStr)
        {
            SetNextIdxRihgt(int.Parse(valueStr));
        });
    }
}
