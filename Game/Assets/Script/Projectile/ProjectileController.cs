using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private ProjectileData data = new ProjectileData();
    private bool isHit;
    private void Update()
    {
        
        transform.Translate(Vector2 .right*data.moveSpeed*Time.deltaTime);
    }
    private void OnEnable()
    {
        isHit = false;
        GetComponent<SpriteRenderer>().color = new Color(
            GetComponent<SpriteRenderer>().color.r,
            GetComponent<SpriteRenderer>().color.g,
            GetComponent<SpriteRenderer>().color.b, 1f);
    }
    private void FixedUpdate()
    {
        if(CheckCollision(out Collider2D _ohter)&&!isHit)
        {

            EventManager.Send<Collider2D>(EventName.ProjectileHit, _ohter);
            if (!_ohter.gameObject.CompareTag(Tags.Ball))
            {
                transform.DOShakePosition(0.1f, 0.2f);
                AudioManager.Instance.Play(AudioName.BulletHit_2);
                var newPieces = PoolManager.Instance.GetFromPool(PoolName.PiecesPool);
                newPieces.transform.position = transform.position;
                newPieces.GetComponent<ParticleSystem>().startColor = GetComponent<SpriteRenderer>().color;
                StartCoroutine(ReturnPool());
                isHit = true;
            }
          //_ohter.gameObject.GetComponent<BrickController>()?.Hitted();
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

    IEnumerator ReturnPool()
    {
       
        Tweener punchScale1 = transform.DOScale(new Vector2(1.5f, 1.5f), 0.06f);
        GetComponent<SpriteRenderer>().DOColor(new Color(
            GetComponent<SpriteRenderer>().color.r,
            GetComponent<SpriteRenderer>().color.g,
            GetComponent<SpriteRenderer>().color.b, 0f), 0.06f);
        yield return punchScale1.WaitForCompletion();
       
        PoolManager.Instance.ReturnPool(PoolName.ProjectilePool, this.gameObject);
    }
}
