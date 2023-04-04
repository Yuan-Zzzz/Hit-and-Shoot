using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    [SceneName]public string from1;
    [SceneName]public string to1;

    [SceneName]public string from2;
    [SceneName]public string to2;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneControl.Instance.Transition(from1,to1);

        }

        if (Input.GetMouseButtonDown(1))
        {
            SceneControl.Instance.Transition(from2, to2);
        }
    }
}
