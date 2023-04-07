using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private ProjectileData data = new ProjectileData();
    private void Update()
    {
        transform.Translate(data.dir*data.moveSpeed*Time.deltaTime);
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
        data.dir = _dir;
    }
    public Vector2 GetDirection()
    {
        return data.dir;
    }
}
