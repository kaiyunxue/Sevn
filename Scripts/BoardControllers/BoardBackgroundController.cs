using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBackgroundController : MonoBehaviour
{
    public void Init(int boardN)
    {
        transform.localScale = new Vector3(boardN, 0.01f, boardN);
    }
}
