using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargenBrickController :BrickController
{
    public override void Hitted()
    {
        base.Hitted();
        EventManager.Send(EventName.ChangeScale);
    }
}
