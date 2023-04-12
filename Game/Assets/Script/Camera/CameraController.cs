using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.Register<Collision2D>(EventName.BallHit, OnBallHit);
        EventManager.Register<Vector2>(EventName.BallDead, OnBallDead);
    }
    private void RecoveryPosition()
    {
        transform.DOMove(new Vector3(0, 0, transform.position.z), 0.2f);
    }
    private void Update()
    {
        Invoke("RecoveryPosition", 1f);
    }
    private void OnBallHit(Collision2D _other)
    {
        transform.DOShakePosition(0.1f, 0.2f);
    }
    private void OnBallDead(Vector2 _position)
    {
        //StartCoroutine(CloseUpShot(_position));

    }
    //IEnumerator CloseUpShot(Vector2 _position)
    //{
    //    transform.DOMove(new Vector3(_position.x, _position.y, transform.position.z), 0.001f);
       
    //    while (GetComponent<Camera>().orthographicSize != 1)
    //    {
            
    //        GetComponent<Camera>().orthographicSize = Mathf.MoveTowards(GetComponent<Camera>().orthographicSize, 1f, 0.1f);
    //        yield return new WaitForEndOfFrame();
    //    }
    //    TimeManager.LaunchBulletTime(0.1f);
    //    while (GetComponent<Camera>().orthographicSize != 5)
    //    {
    //        GetComponent<Camera>().orthographicSize = Mathf.MoveTowards(GetComponent<Camera>().orthographicSize, 5f, 0.1f);
    //        yield return new WaitForEndOfFrame();
    //    }
    //    TimeManager.StopBulletTime();
    //    transform.DOMove(new Vector3(0, 0, transform.position.z), 0.01f);
    //}
    private void OnDisable()
    {
        EventManager.Remove<Collision2D>(EventName.BallHit, OnBallHit);
        EventManager.Remove<Vector2>(EventName.BallDead, OnBallDead);
    }

  
}
