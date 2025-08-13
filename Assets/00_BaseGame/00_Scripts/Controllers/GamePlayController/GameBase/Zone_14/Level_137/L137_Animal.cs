using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L137_Animal : BaseDraggableObject
{
    public int id = 0;
    public bool CheckTheCorrectPosition()
    {
        float distance = Vector2.Distance(transform.position, posCorrect);
        if(Mathf.Abs(distance) < 0.5f)
        {
            return true;
        }
        return false;
    }
    public void HandleTheCorrectCondition(Transform glass)
    {
        transform.DOMove(posCorrect, 0.3f).SetEase(Ease.Linear).OnComplete(delegate
        {
            glass.gameObject.SetActive(true);
        });
        objectCollider.enabled = false;
        spriteRenderer.sortingOrder = orderIndex;
    }


    protected override void ReturnToOriginalPosition()
    {
        transform.DOMove(posDefault, 0.5f).SetEase(Ease.OutBack);
    }
}
