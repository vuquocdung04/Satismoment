using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L33_Item : LoadAutoComponents
{
    public Vector2 targetPosition;
    public BoxCollider2D colli;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        targetPosition = transform.position;
    }
}
