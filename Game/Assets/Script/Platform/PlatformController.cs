using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    private void Start()
    {
        InputManager.OnEnable();
    }

    private void Update()
    {
        Debug.Log(InputManager.inputTest);
    }
}
