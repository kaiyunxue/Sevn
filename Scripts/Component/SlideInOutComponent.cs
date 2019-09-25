using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideInOutComponent : MonoBehaviour
{
    private Vector3 objPos;
    private Vector2 viewportSize;
    [SerializeField]
    private GraphicRaycaster raycaster;
    void Awake()
    {
        if (raycaster)
        {
            raycaster.enabled = true;
        }
        objPos = gameObject.transform.position;
        viewportSize = new Vector2(Screen.width, Screen.height);
    }

    public void SlideInLeft(float speed)
    {
        Vector3 bgPos = new Vector3(objPos.x - viewportSize.x, objPos.y, objPos.z);
        gameObject.transform.position = bgPos;
        StartCoroutine(MoveToPosition(objPos, speed, false));
    }
    public void SlideInRight(float speed)
    {
        Vector3 bgPos = new Vector3(viewportSize.x + objPos.x, objPos.y, objPos.z);
        gameObject.transform.position = bgPos;
        StartCoroutine(MoveToPosition(objPos, speed, false));
    }
    public void SlideInUp(float speed)
    {
        Vector3 bgPos = new Vector3(objPos.x, objPos.y - viewportSize.y, objPos.z);
        gameObject.transform.position = bgPos;
        StartCoroutine(MoveToPosition(objPos, speed, false));
    }
    public void SlideInDown(float speed)
    {
        Vector3 bgPos = new Vector3(objPos.x, viewportSize.y + objPos.y, objPos.z);
        gameObject.transform.position = bgPos;
        StartCoroutine(MoveToPosition(objPos, speed, false));
    }

    public void SlideOutLeft(float speed, bool isDestory)
    {
        Vector3 bgPos = gameObject.transform.position;
        Vector3 targetPos = new Vector3(viewportSize.x + bgPos.x, bgPos.y, bgPos.z);
        StartCoroutine(MoveToPosition(targetPos, speed, isDestory));
    }
    public void SlideOutRight(float speed, bool isDestory)
    {
        Vector3 bgPos = gameObject.transform.position;
        Vector3 targetPos = new Vector3(bgPos.x - viewportSize.x, bgPos.y, bgPos.z);
        StartCoroutine(MoveToPosition(targetPos, speed, isDestory));
    }
    public void SlideOutUp(float speed, bool isDestory)
    {
        Vector3 bgPos = gameObject.transform.position;
        Vector3 targetPos = new Vector3(objPos.x, viewportSize.y + bgPos.y, objPos.z);
        StartCoroutine(MoveToPosition(targetPos, speed, isDestory));
    }
    public void SlideOutDown(float speed, bool isDestory)
    {
        Vector3 bgPos = gameObject.transform.position;
        Vector3 targetPos = new Vector3(objPos.x, bgPos.y - viewportSize.y, objPos.z);
        StartCoroutine(MoveToPosition(targetPos, speed, isDestory));
    }

    IEnumerator MoveToPosition(Vector3 targetPos, float speed, bool isDestory)
    {
        if (raycaster != null)
        {
            raycaster.enabled = false;
        }
        while (gameObject.transform.position != targetPos)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
            yield return 0;
        }
        if (raycaster != null) 
        {
            raycaster.enabled = true;
        }
        if (isDestory)
        {
            Destroy(gameObject);
        }
    }
}
