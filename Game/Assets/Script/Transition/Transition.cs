using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SceneName] public string from;
    [SceneName] public string to;

    public void TransitionToScene()
    {
        SceneControl.Instance.Transition(from, to);
    }
}
