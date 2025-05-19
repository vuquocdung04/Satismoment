using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L19_Book : LoadAutoComponents
{
    public int idBook;
    public float bookWidth;
    public SpriteRenderer spriteRenderer;
    public Vector2 posDefault;
    public BoxCollider2D colli;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bookWidth = spriteRenderer.bounds.size.x;
        posDefault = transform.position;
        colli = GetComponent<BoxCollider2D>();
    }
}
