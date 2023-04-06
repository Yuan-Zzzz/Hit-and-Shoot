using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Brick : MonoBehaviour
{
   
    private BrickData data = new BrickData();

    private void Start()
    {
        data.UpdateBrick(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Tags.Ball))
        {
            data.count--;
            transform.DOPunchScale(new Vector2(0.3f, 0.3f), 0.5f);
            data.UpdateBrick(this.gameObject);
            if (data.count == 0)
            StartCoroutine(Destory());
        }
    }

    IEnumerator Destory()
    {
        Tweener punchScale = transform.DOScale(new Vector2(0f,0f),0.15f);
        yield return punchScale.WaitForCompletion();  
        Destroy(gameObject);
    }
}
