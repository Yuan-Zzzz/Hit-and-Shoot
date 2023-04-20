using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class BrickController : MonoBehaviour
{

    public BrickData data = new BrickData();
    protected GameObject hitObject;

    private void Start()
    {
        data.UpdateBrick(gameObject);

    }
    private void OnEnable()
    {
        EventManager.Register<Collision2D, GameObject>(EventName.BallHit, OnBallHit);
        EventManager.Register<Collider2D>(EventName.ProjectileHit, OnProjectileHit);

    }


    public virtual void OnBallHit(Collision2D other, GameObject ball)
    {
        if (other.gameObject == this.gameObject)
        {
            hitObject = other.gameObject;
            Hitted();
            //改变球的颜色
            if (ball.GetComponent<SpriteRenderer>() != null)
            {
                ball.GetComponent<SpriteRenderer>().DOBlendableColor(
                new Color(
                GetComponent<SpriteRenderer>().color.r,
               GetComponent<SpriteRenderer>().color.g,
                GetComponent<SpriteRenderer>().color.b, 1), 0.5f);
            }
          
        }
    }
    private void OnProjectileHit(Collider2D other)
    {
        if (other.gameObject == this.gameObject)
        {
            hitObject = other.gameObject;
            Hitted();
        }
    }

    public virtual void Hitted()
    {
        data.count--;
        transform.DOPunchScale(new Vector2(0.3f, 0.3f), 0.5f);
        data.UpdateBrick(this.gameObject);
        if (data.count == 0)
            StartCoroutine(Destory());
    }

    private void OnDisable()
    {
        EventManager.Remove<Collision2D, GameObject>(EventName.BallHit, OnBallHit);
        EventManager.Remove<Collider2D>(EventName.ProjectileHit, OnProjectileHit);
    }



    IEnumerator Destory()
    {
        GetComponent<Collider2D>().enabled = false;
        Tweener punchScale = transform.DOScale(new Vector2(0f, 0f), 0.15f);
        yield return punchScale.WaitForCompletion();
        Destroy(gameObject);
    }
}
