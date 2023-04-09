using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;
using System;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    private Rigidbody2D ballRB;
    private Collider2D ballCollider;
    [SerializeField]
    private BallData_SO data;
    [SerializeField]
    private Text CountText;
    private bool canShoot = true;
    private WaitForSeconds spawnFlashDeltaTime = new WaitForSeconds(0.05f);
    private void Awake()
    {
        ballRB = GetComponent<Rigidbody2D>();
        ballCollider = GetComponent<Collider2D>();
        Initialize();
    }

    private void OnEnable()
    {
        EventManager.Register<bool>(EventName.CanShoot, OnCanShoot);
        EventManager.Register<int>(EventName.ShootCountInit, OnShootCountInit);
        PoolManager.Instance.CreateNewPool(Resources.Load<GameObject>("Prefabs/Projectile"), data.count, PoolName.ProjectilePool);
        PoolManager.Instance.CreateNewPool(Resources.Load<GameObject>("Prefabs/Flash"), data.count, PoolName.FlashPool);

        StartCoroutine(SpawnFlash());

    }

    private void OnShootCountInit(int _shootCount)
    {
      data.count = _shootCount;
        CountText.text = data.count.ToString();

    }

    private void Start()
    {
        
    }
    private void OnDisable()
    {
        PoolManager.Instance.Clear(PoolName.ProjectilePool);
        PoolManager.Instance.Clear(PoolName.FlashPool);
        EventManager.Remove<bool>(EventName.CanShoot, OnCanShoot);
        EventManager.Remove<int>(EventName.ShootCountInit, OnShootCountInit);
    }

    private void OnCanShoot(bool _canShoot)
    {
        canShoot = _canShoot;
        if(!_canShoot)CountText.color = Color.clear;
    }
    private void Update()
    {

        if (InputManager.ShootPress&&canShoot) TimeManager.LaunchBulletTime(0.1f);
        if (InputManager.ShootRelease&&canShoot)
        {
            TimeManager.StopBulletTime();
            Shoot();
           
        }
    }

    IEnumerator SpawnFlash()
    {
        while (true) { 
            var newFlash = PoolManager.Instance.GetFromPool(PoolName.FlashPool);
            newFlash.transform.position = transform.position;
            yield return spawnFlashDeltaTime;
        }
         

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        EventManager.Send<Collision2D>(EventName.BallHit, other);
        
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
        var newProjectile = PoolManager.Instance.GetFromPool(PoolName.ProjectilePool);
        newProjectile.GetComponent<ProjectileController>().SetAngle(-dir);
        newProjectile.transform.position = transform.position;
        ballRB.velocity = Vector2.zero;

        ballRB.AddForce(dir * data.backlashForce, ForceMode2D.Force);

        data.count--;
        CountText.text = data.count.ToString();

    }
}
