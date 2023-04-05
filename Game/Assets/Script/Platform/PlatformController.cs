using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private Rigidbody2D platformRB;
    [SerializeField]
    private PlatformData_SO data;
    private void Awake()
    {
        platformRB = GetComponent<Rigidbody2D>();
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
        platformRB.velocity = Vector2.MoveTowards(platformRB.velocity,new Vector2(data.maxSpeed*InputManager.Move.x,platformRB.velocity.y),data.acceleration);
    }
}
