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
        EventManager.Register<Collider2D>(EventName.ProjectileHit, OnProjectileHit);
        EventManager.Register<Vector2>(EventName.BallDead, OnBallDead);
        EventManager.Register<Vector2>(EventName.PrepareDistoryBall, OnPrepareDistoryBall);
    }

    private void OnProjectileHit(Collider2D other)
    {
        ShakeCamera(0.15f, 1.7f);
    }

    private void RecoveryPosition()
    {
        transform.DOMove(new Vector3(0, 0, transform.position.z), 0.2f);
    }

    private void ShakeCamera(float _duration,float _strength)
    {
        transform.DOShakePosition(_duration, _strength);
    }
    private void Update()
    {
        Invoke("RecoveryPosition", 1f);
    }
    private void OnBallHit(Collision2D _other)
    {
        ShakeCamera(0.15f, 1.7f);
    }
    private void OnBallDead(Vector2 _position)
    {
        transform.DOMove(new Vector3(0, 0, transform.position.z), 0.1f);
        //Camera.main.orthographicSize = 5;
    }
    private void OnPrepareDistoryBall(Vector2 _position)
    {
        transform.DOMove(new Vector3(_position.x, _position.y, transform.position.z), 0.01f);
       // StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        while (Camera.main.orthographicSize>1f) {
           Camera.main.orthographicSize =  Mathf.MoveTowards(Camera.main.orthographicSize,1f,0.1f);
            yield return new WaitForFixedUpdate();
        }
    }
    private void OnDisable()
    {
        EventManager.Remove<Collision2D>(EventName.BallHit, OnBallHit);
        EventManager.Remove<Collider2D>(EventName.ProjectileHit, OnProjectileHit);
        EventManager.Remove<Vector2>(EventName.BallDead, OnBallDead);
        EventManager.Remove<Vector2>(EventName.PrepareDistoryBall, OnPrepareDistoryBall);
    }

    
}
