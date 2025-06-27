using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L92_Food : BaseDraggableObject
{

    public bool CheckPositionCorrect()
    {
        float distance = Vector2.Distance(transform.position, posCorrect);
        if(distance < 0.3f)
        {
            transform.DOMove(posCorrect,0.3f).SetEase(Ease.OutBounce);
            objectCollider.enabled = false;
            spriteRenderer.sortingOrder = orderIndex - 1;
            return true;
        }

        return false;
    }
    public override void ReturnToOriginalPosition()
    {

    }
}
