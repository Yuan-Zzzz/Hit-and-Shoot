using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;
using System;

public class BallController : MonoBehaviour
{
    private Rigidbody2D ballRB;
    private Collider2D ballCollider;
    [SerializeField]
    private BallData_SO data;


    private void Awake()
    {
        ballRB = GetComponent<Rigidbody2D>();
        ballCollider = GetComponent<Collider2D>();
        Initialize();
    }

    private void Update()
    {

        if (InputManager.ShootPress) TimeManager.LaunchBulletTime(0.1f);
        if (InputManager.ShootRelease)
        {
            TimeManager.StopBulletTime();
            Shoot();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        EventManager.Send(EventName.BallHit, other);
        if (other.gameObject.CompareTag(Tags.Platform))
        {
            float x = HitFactor(transform.position, other.transform.position, other.collider.bounds.size.x);

            ballRB.velocity = new Vector2(x, 1).normalized * data.maxSpeed;
        }

        transform.DOPunchScale(new Vector2(0.2f, 0.2f), 0.1f);
        Camera.main.transform.DOShakePosition(0.1f, 0.2f);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.DOScale(new Vector3(1, 1, 1), 0.1f);
    }
    private float HitFactor(Vector2 ballPos, Vector2 platformPos, float platformWidth)
    {
        return (ballPos.x - platformPos.x) / platformWidth;
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    private void Initialize()
    {
        ballRB.gravityScale = data.gravity;
        ballCollider.sharedMaterial.bounciness = data.bounciness;
    }

    private void Shoot()
    {
        Vector2 dir = ((Vector2)(transform.position - Camera.main.ScreenToWorldPoint(InputManager.MousePos))).normalized;

        ballRB.velocity = Vector2.zero;

        ballRB.AddForce(dir * data.backlashForce, ForceMode2D.Force);


    }
}
