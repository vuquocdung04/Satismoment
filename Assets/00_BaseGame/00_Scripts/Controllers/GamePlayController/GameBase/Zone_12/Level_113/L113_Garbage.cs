using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L113_Garbage : BaseDraggableObject
{
    public int id;

    public bool CheckCorrectWithGarbageCan(L113_GarbageCan garbageCan)
    {
        if (objectCollider.IsTouching(garbageCan.objCollider) && id == garbageCan.id)
        {
            HandleCorrectPosition();
            return true;
        }
        return false;
    }
    void HandleCorrectPosition()
    {
        objectCollider.enabled = false;
        transform.DOMoveY(transform.position.y - 1.5f,0.5f).SetEase(Ease.Linear);
        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
    }

    public override void ReturnToOriginalPosition()
    {
        transform.DOMove(posDefault, 0.3f).SetEase(Ease.OutBack);
    }
}
