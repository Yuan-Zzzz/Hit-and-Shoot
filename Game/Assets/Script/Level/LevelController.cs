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
           var newBrick = Instantiate(item.brick, item.pos, Quaternion.identity);
            newBrick.GetComponent<BrickController>().data.count = item.data.count;
            newBrick.GetComponent<BrickController>().data.maxCount = item.data.count;
            newBrick.GetComponent<BrickController>().data.brickColor = item.data.brickColor;
        }
    }

    private void OnDisable()
    {
        EventManager.Remove<int>(EventName.LoadLevel, OnLoadLevel);
    }
}

