using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L18_Piece : BaseSnapBack
{
    public SpriteRenderer spriteRenderer;
    public Transform trans;
    public Sprite spWin;
    public int order;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        spriteRenderer = GetComponent<SpriteRenderer>();
        order = spriteRenderer.sortingOrder;
    }
}
