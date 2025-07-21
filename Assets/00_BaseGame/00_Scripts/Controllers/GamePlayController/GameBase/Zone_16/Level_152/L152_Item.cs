using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L152_Item : BaseDraggableObject
{

    public void HandleCorrectPosition(Level_152Ctrl levelCtrl)
    {
        float distance = Vector2.Distance(transform.position, posCorrect);
        if(Mathf.Abs(distance) < 0.5f)
        {
            transform.DOMove(posCorrect,0.2f).SetEase(Ease.InElastic);
            objectCollider.enabled = false;
            spriteRenderer.sortingOrder = orderIndex - 1;
            levelCtrl.winProgress++;
        }
        else
        {
            OnEndDrag();
        }
    }

    public override void ReturnToOriginalPosition()
    {
        
    }
}
