using DG.Tweening;
using System.Collections;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public enum L16Type
{
    Coin, Card, Money
}


public class Level_16Ctrl : BaseDragController<L16_Item>
{
    public int winProgress;
    public float dropAnimationDuration = 0.4f;
    public L16_Compartment[] allWalletSlots;
    private L16_Item currentDraggItem;

    [Header("Sound"), Space(10)] 
    [SerializeField] AudioClip coinSound;
    [SerializeField] AudioClip moneySound;
    [SerializeField] AudioClip cardSound;
    protected override void OnDragStarted()
    {
        currentDraggItem = draggableComponent;
        draggableComponent.transform.DORotate(Vector3.zero, dropAnimationDuration);
        draggableComponent.spriteRenderer.sortingOrder = 20;

        switch (currentDraggItem.type)
        {
            case L16Type.Coin:
                GameController.Instance.musicManager.PlaySingle(coinSound);
                break;
            case L16Type.Card:
                GameController.Instance.musicManager.PlaySingle(cardSound);
                break;
            case L16Type.Money:
                GameController.Instance.musicManager.PlaySingle(moneySound);
                break;
        }
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
            }
        }
        if(targetSlot != null)
        {
            Vector3 dropPos; // Lấy vị trí thả từ compartment
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
            StartCoroutine(HandleWin());
        }

        if (!successfullyDropped)
        {
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
            item.transform.DORotate(new Vector3(0,0,item.angleDefault), dropAnimationDuration);
        }
    }

    private void ResetItemSortingOrder(L16_Item item)
    {
        if (item != null && item.spriteRenderer != null)
        {
            item.spriteRenderer.sortingOrder = item.orderInLayer;
        }
    }

    private IEnumerator HandleWin()
    {
        winProgress++;
        if (winProgress > 13)
        {
            yield return new WaitForSeconds(0.5f);
            WinBox.SetUp().Show();
        }
    }
}
