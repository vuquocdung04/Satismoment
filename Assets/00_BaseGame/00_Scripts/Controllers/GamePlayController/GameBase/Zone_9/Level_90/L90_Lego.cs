using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L90_Lego : BaseDraggableObject
{
    public Transform pointCorrect;

    public bool IsItemInCorrectCompartment()
    {
        if (objectCollider.IsTouching(pointCorrect.GetComponent<BoxCollider2D>()))
        {
            objectCollider.enabled = false;
            transform.DOJump(transform.position, 0.3f, 1, 0.3f)
                    .SetEase(Ease.OutBounce);
            OnEndDrag();
            return true;
        }
        return false;
    }

    public override void ReturnToOriginalPosition()
    {

    }
}
