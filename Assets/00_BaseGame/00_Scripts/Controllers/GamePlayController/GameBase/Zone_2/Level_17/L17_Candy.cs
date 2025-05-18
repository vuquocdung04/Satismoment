using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L17_Candy : LoadAutoComponents
{
    public Vector2 posDefault;
    public CircleCollider2D candyCollider;
    public SpriteRenderer spriteRenderer;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.posDefault = transform.position;
        candyCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
