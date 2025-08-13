using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L99_Item : BaseDraggableObject
{
    public bool CheckPositionCorrect()
    {
        float distance = Vector2.Distance(transform.position, posCorrect);
        if(distance < 0.3f)
        {
            transform.DOMove(posCorrect,0.2f).SetEase(Ease.Linear);
            objectCollider.enabled = false;
            return true;
        }
        return false;
    }

    protected override void ReturnToOriginalPosition()
    {
        transform.DOMoveY(-2.5f,0.3f).SetEase(Ease.Linear);
    }
}
