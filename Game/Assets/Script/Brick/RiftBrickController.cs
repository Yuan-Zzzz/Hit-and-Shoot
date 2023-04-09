using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftBrickController : BrickController
{
    public override void Hitted()
    {
        base.Hitted();
        if (data.count == 0)
        {
            for (int i = 0; i < data.riftCount; i++)
            {
                Instantiate(GameObject.FindGameObjectWithTag(Tags.Ball),transform.position,Quaternion.identity);
            }
           
        }
    }
}
