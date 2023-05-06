using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer.material.SetFloat("_WaveDistanceFromCenter", -0.1f);
        StartCoroutine(Wave(spriteRenderer));
    }
    IEnumerator Wave(SpriteRenderer waveSpr)
    {
       
        while (waveSpr.material.GetFloat("_WaveDistanceFromCenter")<1)
        {
            waveSpr.material.SetFloat("_WaveDistanceFromCenter", waveSpr.material.GetFloat("_WaveDistanceFromCenter") + 0.035f);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("ssssss");
        spriteRenderer.material.SetFloat("_WaveDistanceFromCenter", -0.1f);
     
    }
}
