using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L162_Item : BaseDraggableObject
{
    public Rigidbody2D rb;
    public int id;
    public L162_AdhesiveHook curHook;

    public override void OnStartDrag()
    {
        base.OnStartDrag();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        
    }

    public override void OnEndDrag()
    {
        base.OnEndDrag();
        rb.bodyType = RigidbodyType2D.Dynamic;
        curHook = null;
    }

    public override void InitAfter()
    {
        base.InitAfter();
        rb = GetComponent<Rigidbody2D>();
    }
    public override void ReturnToOriginalPosition()
    {
        
    }
}
