using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBrickController : BrickController
{

    public override void OnBallHit(Collision2D other, GameObject ball)
    {
       if(other.gameObject == this.gameObject)
        {
            hitObject = ball;

            //¸Ä±äÇ½ÑÕÉ«
            GetComponent<SpriteRenderer>().DOBlendableColor(
             ball.GetComponent<SpriteRenderer>().color, 0.5f);
        }
    }
}
