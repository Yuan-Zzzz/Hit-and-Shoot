using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private ProjectileData data = new ProjectileData();
    private void OnEnable()
    {
      
    }
    private void Update()
    {
        transform.Translate(Vector2 .right*data.moveSpeed*Time.deltaTime);
    }
    public void SetSpeed(float _speed)
    {
        data.moveSpeed = _speed;
    }
    public float GetSpeed()
    {
        return data.moveSpeed;
    }
    public void SetDirection(Vector2 _dir)
    {
        float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
