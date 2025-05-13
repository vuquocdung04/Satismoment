using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CakeType
{
    type1, type2, type3, type4, type5
}

public class L2_CakeItem : LoadAutoComponents
{
    public CakeType cakeType;
    public SpriteRenderer spriteRenderer;
    public Sprite iconStart;
    public Sprite iconDrag;
    public Vector2 pos;
    public CircleCollider2D circleCollider;
    public void Init()
    {
        pos = transform.position;
    }

    public void HandleIconDrag()
    {
        spriteRenderer.sprite = iconDrag;
    }

    public void HandleIconStart()
    {
        spriteRenderer.sprite = iconStart;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
}
