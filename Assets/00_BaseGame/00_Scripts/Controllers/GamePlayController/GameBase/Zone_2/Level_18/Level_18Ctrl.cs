using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_18Ctrl : BaseDragController<L18_Piece>
{
    public float snapDistance = 0.2f;
    public int winProgress = 0;
    public List<L18_Piece> lsPieces;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
        draggableComponent.spriteRenderer.sortingOrder = 10;
    }

    protected override void OnDragStarted()
    {
        base.OnDragStarted();
    }

    protected override void OnDragEnded()
    {
        base.OnDragEnded();
        draggableComponent.spriteRenderer.sortingOrder = draggableComponent.order;
        float distanceToTarget = Vector3.Distance(draggableComponent.transform.position, draggableComponent.trans.position);
        Debug.LogError(distanceToTarget);
        if(distanceToTarget < snapDistance)
        {
            draggableComponent.transform.position = draggableComponent.trans.position;
            draggableComponent._collider.enabled = false;
            HandleWin();
        }
        else
        {
            draggableComponent.SnapBackPostion(Ease.InQuad);
        }
    }

    void HandleWin()
    {
        winProgress++;
        if(winProgress > 2)
        {
            StartCoroutine(DoAnimBeforeWin());
            DOVirtual.DelayedCall(2f, () => WinBox.SetUp().Show());
        }

        IEnumerator DoAnimBeforeWin()
        {
            var timeDelay = new WaitForSeconds(0.5f);
            yield return timeDelay;
            int i = 0;
            while (i < lsPieces.Count)
            {
                lsPieces[i].spriteRenderer.sprite = lsPieces[i].spWin;
                i++;
                yield return timeDelay;
            }
        }
    }


}
