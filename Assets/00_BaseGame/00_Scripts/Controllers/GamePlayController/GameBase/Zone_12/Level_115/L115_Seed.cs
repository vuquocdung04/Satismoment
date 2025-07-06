using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L115_Seed : BaseDraggableObject
{
    public bool HasProperlyDraggedSeedOut()
    {
        float distance = Vector2.Distance(transform.position, posDefault);
        if(Mathf.Abs(distance) > 0.1f)
        {
            SeedFallDown();
            return true;
        }
        return false;
    }

    void SeedFallDown()
    {
        objectCollider.enabled = false;
        transform.DOMoveY(-6f, 1f).SetEase(Ease.InCubic);
    }


    public override void ReturnToOriginalPosition()
    {
        
    }

    
}
