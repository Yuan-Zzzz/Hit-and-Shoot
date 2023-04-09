using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private ProjectileData data = new ProjectileData();
 
    private void Update()
    {
        transform.Translate(Vector2 .right*data.moveSpeed*Time.deltaTime);
    }
    private void FixedUpdate()
    {
        if(CheckCollision(out Collider2D _ohter))
        {
            Camera.main.transform.DOShakePosition(0.1f, 0.2f);
            if (!_ohter.gameObject.CompareTag(Tags.Ball))
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Pieces"), transform.position, Quaternion.identity);
                PoolManager.Instance.ReturnPool(PoolName.ProjectilePool,this.gameObject);
            }
            _ohter.gameObject.GetComponent<BrickController>()?.Hitted();
        }
    }
    private bool CheckCollision(out Collider2D _other)
    {
        _other = Physics2D.OverlapBox(transform.position, transform.localScale, data.angle); ;
        return Physics2D.OverlapBox(transform.position, transform.localScale, data.angle);
    }
  
    public void SetSpeed(float _speed)
    {
        data.moveSpeed = _speed;
    }
    public float GetSpeed()
    {
        return data.moveSpeed;
    }
    public void SetAngle(Vector2 _dir)
    {
        data.angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(data.angle, Vector3.forward);
    }
    public float GetAngle()
    {
        return data.angle;
    }
}
