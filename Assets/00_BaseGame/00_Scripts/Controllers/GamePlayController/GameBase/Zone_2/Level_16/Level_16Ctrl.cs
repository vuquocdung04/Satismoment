using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum L16Type
{
    Coin, Card, Money
}


public class Level_16Ctrl : BaseDragController<L16_Item>
{
    public int winProgress = 0;
    public float dropAnimationDuration = 0.4f;
    public L16_Compartment[] allWalletSlots;
    private L16_Item currentDraggItem;
    protected override void OnDragStarted()
    {
        currentDraggItem = draggableComponent;
        draggableComponent.transform.DORotate(Vector3.zero, dropAnimationDuration, RotateMode.Fast);
        draggableComponent.spriteRenderer.sortingOrder = 20;
    }
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }
    protected override void OnDragEnded()
    {
        Collider2D itemCollider = currentDraggItem.GetComponent<Collider2D>();

        L16_Compartment targetSlot = null;
        bool successfullyDropped = false;

        foreach(L16_Compartment slot in allWalletSlots)
        {
            if(slot == null || slot.slotCollider == null) continue;

            if (itemCollider.IsTouching(slot.slotCollider))
            {
                if(slot.idCompartment == currentDraggItem.idItem && slot.type == currentDraggItem.type)
                {
                    targetSlot = slot;
                    break;
                }
                else
                {
                    Debug.LogWarning($"Item chạm slot '{slot.gameObject.name}', nhưng ID KHÔNG KHỚP (Item Target: {currentDraggItem.idItem}, Slot ID: {slot.idCompartment}).");
                }
            }
        }
        if(targetSlot != null)
        {
            Vector3 dropPos = Vector2.zero; // Lấy vị trí thả từ compartment
            switch (currentDraggItem.type)
            {
                case L16Type.Coin:
                    dropPos = targetSlot.GetDropPostionCoin();
                    break;
                default:
                    dropPos = targetSlot.GetDropPostion();
                    break;
            }
            currentDraggItem.transform.DOMove(dropPos, dropAnimationDuration)
                    .SetEase(Ease.InQuad);
            successfullyDropped = true;
            HandleWin();
        }

        if (!successfullyDropped)
        {
            Debug.LogWarning($"Không tìm thấy slot cụ thể nào cho item '{currentDraggItem.gameObject.name}' (Target ID: {currentDraggItem.idItem}). Trả về vị trí cũ.");
            ReturnItemToDefault(currentDraggItem);
        }

        ResetItemSortingOrder(currentDraggItem); // Đặt lại sorting order sau khi kéo
        currentDraggItem = null; // Reset biến tạm
    }

    private void ReturnItemToDefault(L16_Item item)
    {
        if (item != null && item.transform != null)
        {
            item.transform.DOMove(item.posDefault, dropAnimationDuration).SetEase(Ease.OutQuad);
            item.transform.DORotate(new Vector3(0,0,item.angleDefault), dropAnimationDuration, RotateMode.Fast);
        }
    }

    private void ResetItemSortingOrder(L16_Item item)
    {
        if (item != null && item.spriteRenderer != null)
        {
            item.spriteRenderer.sortingOrder = item.orderInLayer;
        }
    }

    private void HandleWin()
    {
        winProgress++;
        if (winProgress > 13) WinBox.SetUp().Show();
    }
}
