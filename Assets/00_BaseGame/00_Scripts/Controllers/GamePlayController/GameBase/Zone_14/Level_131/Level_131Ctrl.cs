using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_131Ctrl : BaseDragController<L131_Sponge>
{
    public Transform itemInTheKitchen;
    private float lastApplyTime;
    public float applyInterval = 0.05f;
    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckDrawingCoverage())
        {
            itemInTheKitchen.DOMoveX(itemInTheKitchen.transform.position.x - 6,1f).SetEase(Ease.Linear);
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
        draggableComponent.DrawAtPosition(draggableComponent.transform.position);
        if (Time.time - lastApplyTime > applyInterval)
        {
            draggableComponent.ApplyMaskChanges();
            lastApplyTime = Time.time;
        }

    }

    protected override void OnDragStarted()
    {
        lastApplyTime = Time.time; // Đặt lại thời gian khi bắt đầu kéo

    }


}
