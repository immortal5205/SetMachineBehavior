using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("事件监听")]
    public PlayAudioEventSO BGMEvent;
    public PlayAudioEventSO FXEvent;
    public PlayAudioEventSO bossAttackEvent;
    public PlayAudioEventSO spellEvent;

    public AudioSource BGM;
    public AudioSource FX;
    public AudioSource bossAttack;
    public AudioSource  spell;


    private void OnEnable() 
    {
        BGMEvent.OnEventRaised += OnBGMEvent;
        FXEvent.OnEventRaised += OnFXEvent;
        bossAttackEvent.OnEventRaised += OnbossAttackEvent;
        spellEvent.OnEventRaised += OnspellEvent;
       

        
    }
      private void OnDisable() 
    {
        BGMEvent.OnEventRaised -= OnBGMEvent;
        FXEvent.OnEventRaised -= OnFXEvent;
        bossAttackEvent.OnEventRaised -= OnbossAttackEvent;
        spellEvent.OnEventRaised -= OnspellEvent;
       
    }



    private void OnspellEvent(AudioClip clip)
    {
       spell.clip = clip;
        spell.Play();
    }

    private void OnbossAttackEvent(AudioClip clip)
    {
       bossAttack.clip = clip;
        bossAttack.Play();
    }

    private void OnFXEvent(AudioClip clip)
    {
        FX.clip = clip;
        FX.Play();
    }

    private void OnBGMEvent(AudioClip clip)
    {
       BGM.clip = clip;
       BGM.Play();
    }

  
}
