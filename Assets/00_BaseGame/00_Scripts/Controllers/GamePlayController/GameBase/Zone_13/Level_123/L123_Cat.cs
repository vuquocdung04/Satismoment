using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L123_Cat : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite defaultSprite;
    public Sprite supriseSprite;

    public void ChangeSpriteDefault()
    {
        spriteRenderer.sprite = defaultSprite;
    }
    public void ChangeSpriteSuprise()
    {
        spriteRenderer.sprite = supriseSprite;
    }
}
