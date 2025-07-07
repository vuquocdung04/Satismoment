using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L119_TissueBox : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> lsSprites;
    int indexSprite = 0;


    public void ChangeSprite()
    {
        if (lsSprites.Count > 0 && spriteRenderer != null)
        {
            spriteRenderer.sprite = lsSprites[indexSprite];
            indexSprite++;
        }
    }

}
