using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// µ¥Àý»ùÀà
/// </summary>
public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    private static T instance;

    public static T Instance => instance;
    private void Awake()
    {
       
        if (instance != null) Destroy(this.gameObject);
        else
        {
            instance = (T)this;
        }
    }
}
