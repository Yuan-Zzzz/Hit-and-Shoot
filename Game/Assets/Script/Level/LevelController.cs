using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{


    public List<LevelData_SO> levelDatas = new List<LevelData_SO>();

    private void OnEnable()
    {
        EventManager.Register<int>(EventName.LoadLevel, OnLoadLevel);
    }

    private void OnLoadLevel(int _level)
    {
        foreach (var item in levelDatas[_level-1].bricks)
        {
            Instantiate(item.brick, item.pos, Quaternion.identity);
        }
    }

    private void OnDisable()
    {
        EventManager.Remove<int>(EventName.LoadLevel, OnLoadLevel);
    }
}

