using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L150_ProduceSticker : BaseDraggableObject
{


    public void HandleReachOut(L150_DragSticker drag)
    {
        float distance = Vector2.Distance(transform.localPosition, posDefault);
        if(distance > 0.1f)
        {
            objectCollider.enabled = false;
            transform.SetParent(drag.transform);
            transform.DOMoveY(transform.localPosition.y - 5f, 0.4f).SetEase(Ease.Linear).OnComplete(delegate
            {
                gameObject.SetActive(false);
            });
            drag.winProgress++;
            drag.HandleWin();
        }
        else
        {
            spriteRenderer.sprite = drag.defaultSprite;
            OnEndDrag();
            spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
    }

    public void OnStartDrag(L150_DragSticker drag)
    {
        OnStartDrag();
        spriteRenderer.sprite = drag.startDragSprite;
        spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
    }

    public override void ReturnToOriginalPosition()
    {
        transform.DOMove(posDefault,0.1f).SetEase(Ease.Linear);
    }

    public override void InitBefore()
    {
        base.InitBefore();
        posDefault = transform.localPosition;
    }
}
