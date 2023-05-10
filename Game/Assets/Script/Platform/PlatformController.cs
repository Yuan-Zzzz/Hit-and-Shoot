using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private Rigidbody2D platformRB;
    [SerializeField]
    private PlatformData_SO data;
    bool isChangeScale = false;
    private void Awake()
    {
        platformRB = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        EventManager.Register<Collision2D, GameObject>(EventName.BallHit, OnBallHit);
        EventManager.Register(EventName.ChangeScale, OnChangeScale);
    }

    private void OnChangeScale()
    {
        isChangeScale = true;
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(new Vector2(2, 2), 0.2f))
         .Append(transform.DOPunchScale(new Vector2(0.7f, 0.7f), 0.05f))
         //此处为持续时间（临时丢个10进去）
         .AppendInterval(10f)
         .AppendCallback(() =>
         {
             transform.DOScale(new Vector2(1, 1), 0.1f);
             isChangeScale = false;
         });
    }

    private Tweener scaleTweener;
    private void OnBallHit(Collision2D other, GameObject ball)
    {
        if (other.gameObject == this.gameObject)
        {
            if (scaleTweener != null) scaleTweener.Kill();
            if (!isChangeScale)
            {
                transform.localScale = Vector3.one;
                scaleTweener = transform.DOPunchScale(new Vector2(0.2f, 0.2f), 0.2f);
            }
            GetComponent<SpriteRenderer>().DOBlendableColor(ball.GetComponent<SpriteRenderer>().color, 1f);
        }
    }

    private void OnDisable()
    {
        EventManager.Remove<Collision2D, GameObject>(EventName.BallHit, OnBallHit);
        EventManager.Remove(EventName.ChangeScale, OnChangeScale);
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

    private void Move()
    {
        platformRB.velocity = Vector2.MoveTowards(platformRB.velocity, new Vector2(data.maxSpeed * InputManager.Move.x, platformRB.velocity.y), data.acceleration);
    }
}
