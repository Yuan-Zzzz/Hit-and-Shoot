using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrationBrickController : BrickController
{
    public override void Hitted()
    {
        base.Hitted();
        EventManager.Send<ShootType>(EventName.ChangeShootType,ShootType.Penetration);
    }
}
