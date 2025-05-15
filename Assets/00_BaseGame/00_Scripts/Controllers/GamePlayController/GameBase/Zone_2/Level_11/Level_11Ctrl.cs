using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_11Ctrl : BaseDragController<L11_Item> 
{

    public int amount;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.Translate(mouseDelta);
        draggableComponent.spriteRenderer.sortingOrder = 7;
    }

    protected override void OnDragEnded()
    {
        base.OnDragEnded();
        draggableComponent.spriteRenderer.sortingOrder = draggableComponent.index;
    }

    public void CheckWinShowPopup()
    {
        if (amount < 3) return;
        DOVirtual.DelayedCall(1f, () => WinBox.SetUp().Show());
    }
}
