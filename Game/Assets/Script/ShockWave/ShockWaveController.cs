using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveController : MonoBehaviour
{

    private void OnEnable()
    {

        EventManager.Register(EventName.ExitBulletTime, OnExitBulletTime);
    }

    private void OnExitBulletTime()
    {
        BallController[] balls = GameObject.FindObjectsOfType<BallController>();
        foreach (BallController ball in balls)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/ShockWave"), ball.transform.position, Quaternion.identity);
        }
       
    }

    private void OnDisable()
    {
        EventManager.Remove(EventName.ExitBulletTime, OnExitBulletTime);
    }

   
}
