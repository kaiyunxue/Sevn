using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField]
    private Slider loadingProgress;

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
}
