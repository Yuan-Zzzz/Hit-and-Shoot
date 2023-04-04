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

    }

    IEnumerator TransitionScene(string _from,string _to)
    {
       yield return null;
    }
}
