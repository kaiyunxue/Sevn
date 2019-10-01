using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoComponent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] Button skipButton;
    [SerializeField] float doubleClickIntervalTime;
    float firstClickTime;
    UnityAction onVideoEnd;

    void Start()
    {
        firstClickTime = 0;
        skipButton.onClick.AddListener(delegate()
        {
            OnClickSkipButton();
        });
        videoPlayer.loopPointReached += OnVideoPlayerEnd;
    }
    private void OnClickSkipButton()
    {
        float curTime = Time.time;
        if (firstClickTime == 0 || curTime - firstClickTime > doubleClickIntervalTime)
        {
            firstClickTime = Time.time;
        }
        else
        {
            StopVideo();
        }
    }

    private void OnVideoPlayerEnd(VideoPlayer vp)
    {
        OnVideoEnd();
    }

    public void PlayVideo(VideoClip video, UnityAction videoEndCallback = null)
    {
        firstClickTime = 0;
        onVideoEnd = videoEndCallback;
        videoPlayer.clip = video;
        videoPlayer.frame = 0;
        videoPlayer.Play();
    }

    public void ResumeVideo()
    {
        videoPlayer.Play();
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
        OnVideoEnd();
    }

    private void OnVideoEnd()
    {
        if (onVideoEnd != null)
        {
            onVideoEnd.Invoke();
            onVideoEnd = null;
        }
    }
}
