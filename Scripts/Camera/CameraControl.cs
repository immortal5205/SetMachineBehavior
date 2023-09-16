using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;


public class CameraControl : MonoBehaviour
{
   public CinemachineImpulseSource impulseSource;
   public VoidEventSO cameraShackEvent;

   private void OnEnable() 
   {
        cameraShackEvent.OnEventRaised += OncameraShackEvent;
   }
   private void OnDisable() 
   {
        cameraShackEvent.OnEventRaised -= OncameraShackEvent;

   }

    private void OncameraShackEvent()
    {
        impulseSource.GenerateImpulse();
    }
}
