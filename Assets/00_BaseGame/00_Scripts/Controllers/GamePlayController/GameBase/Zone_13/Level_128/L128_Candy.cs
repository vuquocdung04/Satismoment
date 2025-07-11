using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L128_Candy : BaseDraggableObject
{
    public int id;
    public bool CheckleCorrectToPosition(L128_Compartment compartment)
    {
        if (objectCollider.IsTouching(compartment.objCollider))
        {
            objectCollider.enabled = false;
            spriteRenderer.sortingOrder = orderIndex - 1;
            transform.DOScale(new Vector3(0.8f,0.8f,0.8f),0.2f).SetEase(Ease.InElastic);
            return true;
        }
        return false;
    }

    public override void ReturnToOriginalPosition()
    {
        
    }
}
