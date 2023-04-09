using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private Rigidbody2D platformRB;
    [SerializeField]
    private PlatformData_SO data;
    private void Awake()
    {
        platformRB = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        EventManager.Register<Collision2D>(EventName.BallHit, OnBallHit);
    }

    private void OnBallHit(Collision2D other)
    {
        if(other.gameObject == this.gameObject)
        {
            transform.DOPunchScale(new Vector2(0.2f, 0.2f), 0.2f);
        }
    }

    private void OnDisable()
    {
        EventManager.Remove<Collision2D>(EventName.BallHit, OnBallHit);
    }
    private void Start()
    {
        //FiXME
        InputManager.OnEnable();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
            transform.DOScale(new Vector3(1, 1, 1), 0.1f);
        
    }
    private void Move()
    {
        platformRB.velocity = Vector2.MoveTowards(platformRB.velocity,new Vector2(data.maxSpeed*InputManager.Move.x,platformRB.velocity.y),data.acceleration);
    }
}
