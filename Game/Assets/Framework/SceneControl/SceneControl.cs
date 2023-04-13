using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景管理器
/// </summary>
public class SceneControl : SingletonMono<SceneControl>
{
    [HideInInspector]
    public int level;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;
    private bool isFade;
    protected override void Awake()
    {
        base.Awake();
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
       
    }
    /// <summary>
    /// 场景转换
    /// </summary>
    /// <param name="_from">起始场景</param>
    /// <param name="_to">转换后的场景</param>
    public void Transition(string _from,string _to)
    {
        StartCoroutine(TransitionScene(_from, _to));
    }
   
    IEnumerator TransitionScene(string _from,string _to)
    {

        //卸载from场景
       // yield return Fade(1f);
        EventManager.Send(EventName.EnterScene);
        yield return SceneManager.UnloadSceneAsync(_from);
        //加载to场景
        yield return SceneManager.LoadSceneAsync(_to,LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
        if (_to == "Gameplay") EventManager.Send<int>(EventName.LoadLevel, level);
        EventManager.Send(EventName.ExitScene);
       // if (_from != "Gameplay") yield return Fade(0f);
    }

    IEnumerator Fade(float targetAlpha)
    {
        isFade = true;
        //阻止射线碰撞
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(targetAlpha - fadeCanvasGroup.alpha) / fadeDuration;
        //当透明度值与目标值相同(相似)时,结束渐变，恢复射线碰撞
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            //渐变操作
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;

    }
}
