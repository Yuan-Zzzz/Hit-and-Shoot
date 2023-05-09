using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private void OnEnable()
    {

        EventManager.Register(EventName.ExitBulletTime, OnExitBulletTime);
    }

    private void OnExitBulletTime()
    {

      
        var ball = GameObject.FindObjectOfType<BallController>().gameObject;
        transform.position = ball.transform.position;
        spriteRenderer.material.SetFloat("_WaveDistanceFromCenter", -0.1f);
        StartCoroutine(Wave(spriteRenderer));
    }
    IEnumerator Wave(SpriteRenderer waveSpr)
    {

        while (waveSpr.material.GetFloat("_WaveDistanceFromCenter") < 1)
        {
            waveSpr.material.SetFloat("_WaveDistanceFromCenter", waveSpr.material.GetFloat("_WaveDistanceFromCenter") + 0.02f);
            yield return new WaitForSeconds(0.01f);
        }

    }
    private void OnDisable()
    {
        EventManager.Remove(EventName.ExitBulletTime, OnExitBulletTime);
    }

   
}
