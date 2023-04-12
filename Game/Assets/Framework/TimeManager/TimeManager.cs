using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class TimeManager
{
  private static float defaultFixedDeltaTime = Time.fixedDeltaTime;

   public static void LaunchBulletTime(float _bulletTimeScale)
    {
       
        Time.timeScale = _bulletTimeScale;
        Time.fixedDeltaTime = defaultFixedDeltaTime*_bulletTimeScale;
        
    }
    public static void StopBulletTime()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = defaultFixedDeltaTime;
    }

  
}

