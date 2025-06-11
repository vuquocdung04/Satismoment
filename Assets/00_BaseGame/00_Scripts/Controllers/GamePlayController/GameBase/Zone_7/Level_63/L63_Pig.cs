using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L63_Pig : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> lsSprites;


    public void DoChangingSpritePig(int index)
    {
        if (index > lsSprites.Count) return;
        spriteRenderer.sprite = lsSprites[index];
    }
}
