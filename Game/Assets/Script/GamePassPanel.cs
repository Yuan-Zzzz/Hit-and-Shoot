using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePassPanel : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private void OnEnable()
    {
        EventManager.Register(EventName.GamePass, OnGamePass);

    }
    private void Start()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    private void OnGamePass()
    {

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
      StartCoroutine(TimeManager.StopTimeAfterSecond(0.5f));

    }

    private void OnDisable()
    {

        EventManager.Remove(EventName.GamePass, OnGamePass);
    }
}
