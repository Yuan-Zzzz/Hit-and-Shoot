using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelData_SO", menuName = "Data/LevelData_SO")]
public class LevelData_SO : ScriptableObject
{
    [Header("当前关卡是否可以射击")]
    public bool canShoot;
    [Header("当前关卡初始的射击次数")]
    public int shootCount;
    //当前关卡的砖块
    public List<SingleBrickData> bricks = new List<SingleBrickData>();
   
}
[System.Serializable]
public class SingleBrickData
{

    public GameObject brick;
    public BrickData data;
    public Vector2 pos;

}
