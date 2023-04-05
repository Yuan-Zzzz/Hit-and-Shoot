using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Tags.Platform))
        {
            float x = HitFactor(transform.position, other.transform.position, other.collider.bounds.size.x);
            
            ballRB.velocity = new Vector2(x, 1).normalized * data.maxSpeed;

            //other.gameObject.transform.DOScale(new Vector3(1, 2, 1), 0.1f);
            other.transform.DOShakeScale(0.2f, new Vector2(0.1f, 0.6f));
            transform.DOShakeScale(0.2f, new Vector2(0.1f, 0.6f));

        }
        
        Camera.main.transform.DOShakePosition(0.1f, 0.2f);
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

}
