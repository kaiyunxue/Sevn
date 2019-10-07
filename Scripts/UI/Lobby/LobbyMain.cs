using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMain : LobbyUIBase
{
    [Header("引用")]
    public AudioSource audioSource;
    [Header("配置")]
    public AudioClip[] modelAudios;
    public float firstIntervalSecond;
    public float audioIntervalSecond;
    protected override void Start()
    {
        base.Start();
        StartCoroutine(PlayModelAudio());
    }
    
    private IEnumerator PlayModelAudio()
    {
        yield return new WaitForSeconds(firstIntervalSecond);
        while (true)
        {
            int randIdx = Random.Range(0, modelAudios.Length);
            audioSource.clip = modelAudios[randIdx];
            audioSource.Play();
            yield return new WaitForSeconds(audioIntervalSecond);
        }
    }
}
