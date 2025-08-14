using DG.Tweening;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_11Ctrl : BaseDragController<L11_Item> 
{
    public AudioClip sound;
    public int amount;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.Translate(mouseDelta);
        draggableComponent.spriteRenderer.sortingOrder = 7;
    }

    protected override void OnDragEnded()
    {
        draggableComponent.spriteRenderer.sortingOrder = draggableComponent.index;
    }

    public void CheckWinShowPopup()
    {
        if (amount < 3) return;
        DOVirtual.DelayedCall(1f, () => WinBox.SetUp().Show());
    }

    protected override void OnDragStarted()
    {
        
    }
}
