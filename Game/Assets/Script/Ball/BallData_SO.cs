using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BallData_SO", menuName = "Data/BallData_SO")]
public class BallData_SO : ScriptableObject
{
    public float bounciness;
    public float gravity;
    public float backlashForce;
    public float maxSpeed;
    public int count;
}
