using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMono<GameManager>
{
  public static GameState gameState;
    protected override void Awake()
    {
        base.Awake();
        gameState = GameState.Pause;
    }
}
