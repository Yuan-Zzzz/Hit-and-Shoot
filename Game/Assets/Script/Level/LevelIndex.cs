using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIndex : MonoBehaviour
{
  public void SetLevelIndex(int _level)
    {
        SceneControl.Instance.level = _level;
    }
}
