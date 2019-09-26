using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField]
    private Slider loadingProgress;
    [SerializeField]
    private GraphicRaycaster raycaster;
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation async;
        float progressValue = 0f;

        async = SceneManager.LoadSceneAsync("MainScene");
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress < 0.9f)
            {
                progressValue = async.progress;
            }
            else
            {
                progressValue = 1.0f;
            }

            loadingProgress.value = progressValue;

            if (progressValue >= 0.9)
            {
                async.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void OnSlideEnd()
    {
        if (raycaster != null)
        {
            raycaster.enabled = true;
        }
    }

    public void OnSlideBegin()
    {
        if (raycaster != null)
        {
            raycaster.enabled = false;
        }
    }
}
