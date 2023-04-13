using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{

   public CanvasGroup canvasGroup;
    private void Start()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    private void OnEnable()
    {
        EventManager.Register<Vector2>(EventName.BallDead, OnBallDead);
    }

    private void OnBallDead(Vector2 _position)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        //TimeManager.TimeStop();
        StartCoroutine(TimeManager.StopTimeAfterSecond(0.5f));
    }

    private void OnDisable()
    {
        EventManager.Remove<Vector2>(EventName.BallDead, OnBallDead);
    }
}
