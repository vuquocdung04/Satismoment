using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L139_ToyPiece : BaseDraggableObject
{
    public bool isDraggable;
    bool isRemovedFromSprue;

    public void HandleCorrectPosition(Level_139Ctrl levelCtrl, System.Action callback = null)
    {
        if (!isDraggable) return;
        if (!isRemovedFromSprue) return;
        float distance = Vector2.Distance(transform.position, posCorrect);
        if(Mathf.Abs(distance) < 0.3f)
        {
            objectCollider.enabled = false;
            levelCtrl.winProgress++;
            transform.DOMove(posCorrect, 0.2f).SetEase(Ease.InElastic);
            if(levelCtrl.winProgress == levelCtrl.lsT_ItemDragables.Count)
            {
                callback?.Invoke();
            }
        }
        else
        {
            ReturnToOriginalPosition();
        }
    }


    public void OnStartDrag(Level_139Ctrl levelCtrl, System.Action callback = null)
    {
        if (isDraggable) return;
        if (isRemovedFromSprue)
        {
            objectCollider.enabled = false;
            transform.DOMove(posDefault, 0.3f).SetEase(Ease.Linear);
            levelCtrl.amountStage++;
            if(levelCtrl.amountStage == levelCtrl.lsT_ItemDragables.Count)
            {
                callback?.Invoke();
            }
            isDraggable = true;
        }
        else
        {
            transform.DOScale(new Vector3(0.9f, 1.1f, 1f), 0.1f).SetEase(Ease.Linear).OnComplete(delegate
            {
                transform.DOScale(Vector3.one, 0.1f);
            });
            isRemovedFromSprue = true;
        }
    }

    protected override void ReturnToOriginalPosition()
    {
        objectCollider.enabled = false;
        transform.DOMove(posDefault, 0.2f).SetEase(Ease.OutBack).OnComplete(delegate
        {
            objectCollider.enabled = true;
        });
    }
}
