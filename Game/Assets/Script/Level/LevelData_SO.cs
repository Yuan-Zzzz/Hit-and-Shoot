using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelData_SO", menuName = "Data/LevelData_SO")]
public class LevelData_SO : ScriptableObject
{
    
        public List<SingleBrickData> bricks = new List<SingleBrickData>();

   
}
[System.Serializable]
public class SingleBrickData
{
 
   public GameObject brick;
   public Vector2 pos;
}
