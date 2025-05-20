using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L23_Picture : LoadAutoComponents
{
    public int idPicture;
    public SpriteRenderer spriteRenderer;
    public Vector2 posDefault;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        posDefault = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
