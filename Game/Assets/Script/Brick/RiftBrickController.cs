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
                if (hitObject != null)
                {
                    var newBall = Instantiate(Resources.Load<GameObject>("Prefabs/Ball"), transform.position, Quaternion.identity);
                    newBall.GetComponent<BallController>().count = GameObject.FindGameObjectWithTag(Tags.Ball).GetComponent<BallController>().count;
                    newBall.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
                    newBall.GetComponent<BallController>().UpdateCountText();
                }
            }
        }
    }

   


}
