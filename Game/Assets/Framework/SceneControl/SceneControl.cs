using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景管理器
/// </summary>
public class SceneControl : SingletonMono<SceneControl>
{
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
        EventManager.Send(EventName.EnterScene);
        yield return SceneManager.UnloadSceneAsync(_from);
        //加载to场景
        yield return SceneManager.LoadSceneAsync(_to,LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
        EventManager.Send(EventName.ExitScene);
    }
}
