using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayAudioEventSO", menuName = "Event/PlayAudioEventSO")]
public class PlayAudioEventSO : ScriptableObject 
{
    public UnityAction<AudioClip> OnEventRaised;

    public void Raised(AudioClip audioClip)
    {
        OnEventRaised?.Invoke(audioClip);
    }

}