using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingController : MonoBehaviour
{
    Volume volume;
    List<VolumeComponent> volumes;
    private void Awake()
    {
        volume = GetComponent<Volume>();
        volumes = volume.profile.components;
    }
    private void OnEnable()
    {
        EventManager.Register(EventName.EnterBulletTime, OnEnterBulletTime);
        EventManager.Register(EventName.ExitBulletTime, OnExitBulletTime);
    }

    private void OnExitBulletTime()
    {

        volumes[1].parameters[2].SetValue(new FloatParameter(0f));
        volumes[2].parameters[0].SetValue(new FloatParameter(0f));

    }

    private void OnEnterBulletTime()
    {
     
        volumes[1].parameters[2].SetValue(new FloatParameter(0.2f));
        volumes[2].parameters[0].SetValue(new FloatParameter(1f));
    }
    private void OnDisable()
    {

        EventManager.Remove(EventName.EnterBulletTime, OnEnterBulletTime);
        EventManager.Remove(EventName.ExitBulletTime, OnExitBulletTime);
    }
}
