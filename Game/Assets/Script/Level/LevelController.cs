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
        
        StopAllCoroutines();
        TimeManager.NormalTime();
        if(_level<=0)return;
        EventManager.Send<bool>(EventName.CanShoot, levelDatas[_level - 1].canShoot);
        EventManager.Send<int>(EventName.ShootCountInit, levelDatas[_level-1].shootCount);
        //º”‘ÿµØ«Ú
        if(GameObject.FindGameObjectWithTag(Tags.Ball)!=null) GameObject.FindGameObjectWithTag(Tags.Ball).transform.position =  levelDatas[_level - 1].ballPosition;
        //º”‘ÿ◊©øÈ
        foreach (var item in levelDatas[_level-1].bricks)
        {
           var newBrick = Instantiate(item.brick, item.pos, Quaternion.identity);
            newBrick.GetComponent<BrickController>().data.count = item.data.count;
            newBrick.GetComponent<BrickController>().data.maxCount = item.data.count;
            newBrick.GetComponent<BrickController>().data.brickColor = item.data.brickColor;
            newBrick.GetComponent<BrickController>().data.riftCount = item.data.riftCount;
        }
    }
    private void Update()
    {
        if (!GameObject.FindObjectOfType<BrickController>())
        {
            EventManager.Send(EventName.GamePass);
        }
    }
    private void OnDisable()
    {
        EventManager.Remove<int>(EventName.LoadLevel, OnLoadLevel);
    }
}

