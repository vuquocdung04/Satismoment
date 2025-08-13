using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L178_BrokenRecord : BaseDraggableObject
{

    public void CheckCorrectToPosition(System.Action callback = null)
    {
        float distance = Vector2.Distance(transform.position, posCorrect);
        if(Mathf.Abs(distance) < 0.3f)
        {
            objectCollider.enabled = false;
            spriteRenderer.sortingOrder = 1;
            transform.DOMove(posCorrect, 0.2f).SetEase(Ease.Linear);
            callback?.Invoke();
        }
        else
        {
            OnEndDrag();
        }
    }

    protected override void ReturnToOriginalPosition()
    {
        
    }
}
