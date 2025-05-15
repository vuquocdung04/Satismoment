using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L11_Item : LoadAutoComponents
{
    public SpriteRenderer spriteRenderer;
    public int index;
    public int id;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        spriteRenderer = GetComponent<SpriteRenderer>();
        index = spriteRenderer.sortingOrder;

    }
}
