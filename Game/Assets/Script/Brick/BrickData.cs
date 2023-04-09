using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BrickData
{
    public int count = 1;
    [HideInInspector]
    public int maxCount;
    public Color brickColor;
    
    /// <summary>
    /// 更新砖块
    /// </summary>
    /// <param name="currentBrick">当前砖块</param>
    public void UpdateBrick(GameObject currentBrick)
    {
        var brickSpriteRenderer = currentBrick.GetComponent<SpriteRenderer>();
        if (brickSpriteRenderer == null) return;
        //更新颜色
        brickSpriteRenderer.color = brickColor;
        brickSpriteRenderer.color = new Color(brickSpriteRenderer.color.r, brickSpriteRenderer.color.g, brickSpriteRenderer.color.b, count / (float)maxCount);
        if (brickSpriteRenderer.color.a == 0) brickSpriteRenderer.color = brickSpriteRenderer.color = new Color(brickSpriteRenderer.color.r, brickSpriteRenderer.color.g, brickSpriteRenderer.color.b, 0.1f);
    }
}