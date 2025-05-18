using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L16_Compartment : LoadAutoComponents
{
    public L16Type type;
    public int idCompartment;
    public Transform dropPos;
    public Collider2D slotCollider;

    public Vector3 GetDropPostion()
    {
        return dropPos != null ? dropPos.position : transform.position + new Vector3(0,0.4f,0);
    }
    public Vector3 GetDropPostionCoin()
    {
        float rand = Random.Range(-0.4f,0.3f);
        return transform.position + new Vector3(rand,rand);
    }
    public void OnItemDropped(L16_Item item)
    {
        item.transform.SetParent(this.transform);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        slotCollider = GetComponent<Collider2D>();
    }
}
