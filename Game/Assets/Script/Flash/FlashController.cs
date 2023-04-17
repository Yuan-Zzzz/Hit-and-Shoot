using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashController : MonoBehaviour
{
    SpriteRenderer flashSpriteRenderer;
    public SpriteRenderer followSpriteRenderer;
    private void Awake()
    {
        flashSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {

        //EventManager.Register<Collision2D>(EventName.BallHit, OnBallHit);
        if(followSpriteRenderer != null)flashSpriteRenderer.color = followSpriteRenderer.color;

        flashSpriteRenderer.DOColor(new Color(flashSpriteRenderer.color.r, flashSpriteRenderer.color.g, flashSpriteRenderer.color.b, 0), 0.5f);
        transform.localScale = new Vector2(1, 1);
        transform.DOScale(new Vector2(0f, 0f), 0.5f);
        // StartCoroutine(Fade());
    }

    //private void OnBallHit(Collision2D arg0)
    //{
    //    flashSpriteRenderer.DOColor(new Color(flashSpriteRenderer.color.r, flashSpriteRenderer.color.g, flashSpriteRenderer.color.b, 0), 0.1f);
    //}
    private void Update()
    {
        if(flashSpriteRenderer.color.a ==0) PoolManager.Instance.ReturnPool(PoolName.FlashPool, this.gameObject);
    }
    private void OnDisable()
    {
       // EventManager.Remove<Collision2D>(EventName.BallHit, OnBallHit);
    }
    //IEnumerator Fade()
    //{
    //    while (flashSpriteRenderer.color.a > 0)
    //    {
    //        flashSpriteRenderer.color = new Color(flashSpriteRenderer.color.r, flashSpriteRenderer.color.g, flashSpriteRenderer.color.b,
    //            Mathf.MoveTowards(flashSpriteRenderer.color.a,0,0.1f));
    //        transform.localScale = new Vector2(Mathf.MoveTowards(transform.localScale.x, 0, 0.1f), Mathf.MoveTowards(transform.localScale.y, 0, 0.1f));
    //        yield return fadeDeltaTime;
    //    }

    //    PoolManager.Instance.ReturnPool(PoolName.FlashPool,this.gameObject);
    //}
}
