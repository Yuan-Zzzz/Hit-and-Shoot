using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusBrickController : BrickController
{
    public override void Hitted()
    {
       
        data.count--;
        transform.DOPunchScale(new Vector2(0.3f, 0.3f), 0.5f);
        data.UpdateBrick(this.gameObject);

        //增加射击次数
        foreach (var ball in FindObjectsOfType<BallController>())
        {
            ball.count++;
            ball.UpdateCountText();
        }
        if (data.count == 0)
            StartCoroutine(Destory());

       
    }
}
