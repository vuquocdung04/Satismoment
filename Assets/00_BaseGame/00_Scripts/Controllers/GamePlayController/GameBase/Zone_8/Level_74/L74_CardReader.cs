using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L74_CardReader : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;

    public void ChangeSpriteLed()
    {
        spriteRenderer.sprite = sprite;
    }
}
