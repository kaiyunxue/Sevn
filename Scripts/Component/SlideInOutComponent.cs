using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideInOutComponent : MonoBehaviour
{
    private Vector3 objPos;
    private Vector2 viewportSize;
    private bool isSliding;
    void Awake()
    {
        if (isSliding) return;
        isSliding = false;
        objPos = gameObject.transform.position;
        viewportSize = new Vector2(Screen.width, Screen.height);
    }

    public void SlideInLeft(float speed)
    {
        if (isSliding) return;
        Vector3 bgPos = new Vector3(objPos.x - viewportSize.x, objPos.y, objPos.z);
        gameObject.transform.position = bgPos;
        StartCoroutine(MoveToPosition(objPos, speed, false));
    }
    public void SlideInRight(float speed)
    {
        if (isSliding) return;
        Vector3 bgPos = new Vector3(viewportSize.x + objPos.x, objPos.y, objPos.z);
        gameObject.transform.position = bgPos;
        StartCoroutine(MoveToPosition(objPos, speed, false));
    }
    public void SlideInUp(float speed)
    {
        if (isSliding) return;
        Vector3 bgPos = new Vector3(objPos.x, objPos.y - viewportSize.y, objPos.z);
        gameObject.transform.position = bgPos;
        StartCoroutine(MoveToPosition(objPos, speed, false));
    }
    public void SlideInDown(float speed)
    {
        if (isSliding) return;
        Vector3 bgPos = new Vector3(objPos.x, viewportSize.y + objPos.y, objPos.z);
        gameObject.transform.position = bgPos;
        StartCoroutine(MoveToPosition(objPos, speed, false));
    }

    public void SlideOutLeft(float speed, bool isDestory)
    {
        if (isSliding) return;
        Vector3 bgPos = gameObject.transform.position;
        Vector3 targetPos = new Vector3(viewportSize.x + bgPos.x, bgPos.y, bgPos.z);
        StartCoroutine(MoveToPosition(targetPos, speed, isDestory));
    }
    public void SlideOutRight(float speed, bool isDestory)
    {
        if (isSliding) return;
        Vector3 bgPos = gameObject.transform.position;
        Vector3 targetPos = new Vector3(bgPos.x - viewportSize.x, bgPos.y, bgPos.z);
        StartCoroutine(MoveToPosition(targetPos, speed, isDestory));
    }
    public void SlideOutUp(float speed, bool isDestory)
    {
        if (isSliding) return;
        Vector3 bgPos = gameObject.transform.position;
        Vector3 targetPos = new Vector3(objPos.x, viewportSize.y + bgPos.y, objPos.z);
        StartCoroutine(MoveToPosition(targetPos, speed, isDestory));
    }
    public void SlideOutDown(float speed, bool isDestory)
    {
        if (isSliding) return;
        Vector3 bgPos = gameObject.transform.position;
        Vector3 targetPos = new Vector3(objPos.x, bgPos.y - viewportSize.y, objPos.z);
        StartCoroutine(MoveToPosition(targetPos, speed, isDestory));
    }

    IEnumerator MoveToPosition(Vector3 targetPos, float speed, bool isDestory)
    {
        isSliding = true;
        while (gameObject.transform.position != targetPos)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
            yield return 0;
        }
        isSliding = false;
        if (isDestory)
        {
            Destroy(gameObject);
        }
    }
}
