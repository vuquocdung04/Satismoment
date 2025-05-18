using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class L16_Item : LoadAutoComponents
{
    public int idItem;
    public L16Type type;
    public float angleDefault;
    public Vector2 posDefault;
    public SpriteRenderer spriteRenderer;
    public int orderInLayer;
    public float posDrop;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        angleDefault = this.transform.eulerAngles.z;
        posDefault = this.transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        orderInLayer = spriteRenderer.sortingOrder;
    }


}
