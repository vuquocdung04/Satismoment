using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_131Ctrl : BaseDragController<L131_Sponge>
{
    public Transform itemInTheKitchen;
    public int cleanedItemCount;
    private float lastApplyTime;
    public float applyInterval = 0.05f;
    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckDrawingCoverage())
        {
            cleanedItemCount++;
            if(cleanedItemCount < 3)
            {
                ItemInTheKitchenMoving();
            }
            else
            {
                StartCoroutine(HandleWinCondition());
            }
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

    void ItemInTheKitchenMoving()
    {
        itemInTheKitchen.DOMoveX(itemInTheKitchen.transform.position.x - 6, 0.4f).SetEase(Ease.Linear);
    }
    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }

}
