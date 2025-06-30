using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L100_Item : BaseDraggableObject
{

    public bool CheckPositionCorrect()
    {
        float distance = Vector2.Distance(transform.localPosition, posCorrect);
        if(distance < 0.2f)
        {
            transform.DOLocalMove(posCorrect, 0.3f).SetEase(Ease.Linear);
            objectCollider.enabled = false;
            spriteRenderer.sortingOrder = orderIndex - 1;
            return true;

        }
        return false;
    }

    public override void ReturnToOriginalPosition()
    {
        
    }
    public override void InitAfter()
    {
        base.InitAfter();
        posCorrect = transform.localPosition;
    }

}
