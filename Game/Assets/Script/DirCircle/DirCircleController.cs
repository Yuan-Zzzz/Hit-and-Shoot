using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirCircle : MonoBehaviour
{

    private bool canShoot;

    private void OnEnable()
    {
        EventManager.Register<bool>(EventName.CanShoot, OnCanShoot);
    }

    private void OnCanShoot(bool _canShoot)
    {
        if (!_canShoot) GetComponent<SpriteRenderer>().color = Color.clear;
    }

    private void OnDisable()
    {
        EventManager.Remove<bool>(EventName.CanShoot, OnCanShoot);
    }
    void Update()
    {
        if (InputManager.ShootPress)
        {
            transform.DOScale(new Vector2(1.2f, 1.2f), 0.05f);
        }
        if(InputManager.ShootRelease) transform.DOScale(new Vector2(1f, 1f), 0.1f);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(InputManager.MousePos);
        Vector2 direction = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
