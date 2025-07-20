using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L150_DragSticker : BaseDragControllerVer2<L150_ProduceSticker>
{
    public Sprite defaultSprite;
    public Sprite startDragSprite;
    protected override void OnDragEnded()
    {
        draggableComponent.HandleReachOut(this);
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag(this);
    }
    
    public void HandleWin()
    {
        if(winProgress == lsItems.Count)
        {
            isWin = true;
            StartCoroutine(HandleWinCondition());
        }
    }


    protected override void SetupAfter()
    {
        foreach (var item in this.lsItems) item.InitAfter();
    }

    protected override void SetupBefore()
    {
        foreach (var item in this.lsItems) item.InitBefore();
        
    }
}
