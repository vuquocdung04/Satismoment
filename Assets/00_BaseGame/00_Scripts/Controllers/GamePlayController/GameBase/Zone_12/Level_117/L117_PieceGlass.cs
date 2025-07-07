using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L117_PieceGlass : BaseDraggableObject
{
    public bool CheckCorrectPosition()
    {
        float distance = Vector2.Distance(transform.position, posCorrect);
        if(Mathf.Abs(distance) < 0.3f)
        {
            transform.DOMove(posCorrect, 0.15f).SetEase(Ease.Linear).OnComplete(delegate
            {
                spriteRenderer.sortingOrder = orderIndex - 1;
            });
            objectCollider.enabled = false;
            return true;
        }
        return false;
    }

    

    public override void ReturnToOriginalPosition()
    {
        
    }
}
