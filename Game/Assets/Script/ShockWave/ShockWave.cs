using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Wave(GetComponent<SpriteRenderer>()));
    }

    IEnumerator Wave(SpriteRenderer waveSpr)
    {
        waveSpr.material.SetFloat("_WaveDistanceFromCenter", -0.1f);
        while (waveSpr.material.GetFloat("_WaveDistanceFromCenter")<1)
        {
            waveSpr.material.SetFloat("_WaveDistanceFromCenter", waveSpr.material.GetFloat("_WaveDistanceFromCenter") + 0.035f);
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(this.gameObject);
    }
}
