using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L149_Fruit : BaseDraggableObject
{
    public int id;
    public void HandleCorrectPosition(Level_149Ctrl levelCtrl)
    {
        if (objectCollider.IsTouching(levelCtrl.GetHandById(id).objCollider))
        {
            transform.SetParent(levelCtrl.GetHandById(id).transform);
            objectCollider.enabled = false;
            levelCtrl.GetHandById(id).objCollider.enabled = false;
            levelCtrl.GetHandById(id).hadFruit = true;
            levelCtrl.winProgress++;
        }
        else
        {
            OnEndDrag();
            spriteRenderer.sortingOrder = orderIndex;
        }
    }
    public override void ReturnToOriginalPosition()
    {
        transform.DOMove(posDefault,0.4f).SetEase(Ease.OutBack);
    }
}
