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
        AudioManager.Instance.Play(AudioName.BGM);
        StopAllCoroutines();
        TimeManager.NormalTime();
        if(_level<0)return;
        EventManager.Send<bool>(EventName.CanShoot, levelDatas[_level].canShoot);
        EventManager.Send<int>(EventName.ShootCountInit, levelDatas[_level].shootCount);
        //加载弹球
        if(GameObject.FindGameObjectWithTag(Tags.Ball)!=null) GameObject.FindGameObjectWithTag(Tags.Ball).transform.position =  levelDatas[_level].ballPosition;

     
        //加载砖块
        foreach (var item in levelDatas[_level].bricks)
        {
           var newBrick = Instantiate(item.brick, item.pos, Quaternion.identity);
            newBrick.GetComponent<BrickController>().data.count = item.data.count;
            newBrick.GetComponent<BrickController>().data.maxCount = item.data.count;
            newBrick.GetComponent<BrickController>().data.brickColor = item.data.brickColor;
            newBrick.GetComponent<BrickController>().data.riftCount = item.data.riftCount;
        }

        //判断并加载无限模式
        if (_level == 0)
        {
            //生成无限模式控制器
            GameObject infCtrl = new GameObject(typeof(InfinitudeModeController).Name, typeof(InfinitudeModeController));

        }
    }
   public void NextLevel()
    {
        SceneControl.Instance.level++;
    }
    private void Update()
    {
        if (!GameObject.FindObjectOfType<BrickController>())
        {
            EventManager.Send(EventName.GamePass);
        }
        if (!GameObject.FindObjectOfType<BallController>())
        {
            EventManager.Send(EventName.GameOver);
        }

       

    }

    private void OnDisable()
    {
        EventManager.Remove<int>(EventName.LoadLevel, OnLoadLevel);
    }
}

