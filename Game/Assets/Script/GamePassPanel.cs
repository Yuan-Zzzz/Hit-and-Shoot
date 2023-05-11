using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePassPanel : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private float levelTime;
    public Text timeText;
    public Text timeTextInPanel;
    private void OnEnable()
    {
        EventManager.Register(EventName.GamePass, OnGamePass);
        levelTime = 0f;

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
        //AudioManager.Instance.Pause(AudioName.BGM);
        //AudioManager.Instance.Play(AudioName.GamePass);
        StartCoroutine(TimeManager.StopTimeAfterSecond(0f));
        timeTextInPanel.text = "Time:"+levelTime;

    }
    private void Update()
    {
        levelTime+=Time.deltaTime;
        timeText.text = levelTime.ToString();
    }
    private void OnDisable()
    {

        EventManager.Remove(EventName.GamePass, OnGamePass);
    }
}
