using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class TimeManager
{
  private static float defaultFixedDeltaTime = Time.fixedDeltaTime;

   public static void LaunchBulletTime(float _bulletTimeScale)
    {

        EventManager.Send(EventName.EnterBulletTime);
        Time.timeScale = _bulletTimeScale;
        Time.fixedDeltaTime = defaultFixedDeltaTime*_bulletTimeScale;
    }
    public static void StopBulletTime()
    {
        EventManager.Send(EventName.ExitBulletTime);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = defaultFixedDeltaTime;
    }
    public static void TimeStop()
    {
        Time.timeScale = 0;
    }
    public static IEnumerator  StopTimeAfterSecond(float _second)
    {
        yield return new WaitForSecondsRealtime(_second);
        TimeStop();
    }
    public static void NormalTime()
    {
        Time.timeScale = 1f;
    }
  
}

