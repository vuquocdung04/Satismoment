using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L180_Coin : BaseDraggableObject
{

    public void CheckCorrectToPosition(BoxCollider2D colli, Level_180Ctrl levelCtrl)
    {
        if (objectCollider.IsTouching(colli))
        {
            objectCollider.enabled = false;
            transform.position = new Vector3(1.55f,-2.48f);
            spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            transform.DOMoveY(-2f, 0.5f).OnComplete(delegate
            {
                spriteRenderer.sortingOrder = 2;
                levelCtrl.ActivePoint();
                levelCtrl.winProgress++;
                StartCoroutine(levelCtrl.CheckWin());
            });
        }
        else
        {
            OnEndDrag();
        }
    }

    protected override void ReturnToOriginalPosition()
    {
        objectCollider.enabled = false;
        transform.DOMove(posDefault, 0.3f).SetEase(Ease.OutBack).OnComplete(delegate
        {
            objectCollider.enabled =true;
        });
    }
}
