using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BrickData
{
    public int count = 1;


    private Color color_3 = new Color(0, 0, 0, 1);
    private Color color_2 = new Color(0.5f, 0.5f, 0.5f, 1);
    private Color color_1 = new Color(1, 1,1, 1);

    
    /// <summary>
    /// 更新砖块
    /// </summary>
    /// <param name="currentBrick">当前砖块</param>
    public void UpdateBrick(GameObject currentBrick)
    {
        var brickSpriteRenderer = currentBrick.GetComponent<SpriteRenderer>();
        if (brickSpriteRenderer == null) return;

        switch (count)
        {
            case 1:
                brickSpriteRenderer.color = color_1;
                break;
            case 2:
                brickSpriteRenderer.color = color_2;
                break;
            case 3:
                brickSpriteRenderer.color = color_3;
                break;
            default:
                break;
        }
    }
}
