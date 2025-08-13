using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_139Ctrl : BaseDragControllerVer2<L139_ToyPiece>
{
    public int amountStage = 0;
    public Transform toyFrame;
    protected override void OnDragEnded()
    {
        draggableComponent.HandleCorrectPosition(this, delegate
        {
            isWin = true;
            StartCoroutine(HandleWinCondition());
        });
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        if (draggableComponent.isDraggable)
        {
            draggableComponent.transform.position += mouseDelta;
        }
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag(this, delegate
        {
            MoveToNextStage();
        });
    }

    void MoveToNextStage()
    {
        toyFrame.transform.DOMoveX(-6f, 0.5f).SetEase(Ease.Linear).OnComplete(delegate
        {
            foreach(var item in this.lsT_ItemDragables)
            {
                item.objectCollider.enabled = true;
            }
        });

    }

    


    protected override void SetupComponent_PositionCorrect()
    {
        foreach(var item in this.lsT_ItemDragables) item.InitCorrect();
    }

    protected override void SetupPositionDefault()
    {
        foreach (var item in this.lsT_ItemDragables) item.InitDefault();
    }
}
