using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    public PlayAudioEventSO audioEvent;
    public AudioClip audioClip;
    public bool isPlay;

    private void OnEnable() 
    {
        if(isPlay)
        PlayAudioClip();
    }
   
    public void PlayAudioClip()
    {
        audioEvent.Raised(audioClip);
    }
}
