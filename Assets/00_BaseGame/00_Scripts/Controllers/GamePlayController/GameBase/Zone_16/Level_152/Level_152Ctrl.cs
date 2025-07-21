using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_152Ctrl : BaseDragControllerVer2<L152_Item>
{
    public Transform valiLid;
    protected override void OnDragEnded()
    {
        draggableComponent.HandleCorrectPosition(this);
        if(winProgress == lsItems.Count)
        {
            isWin = true;
            StartCoroutine(HandleWinCondition());
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
    }

    public override IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.5f);
        valiLid.gameObject.SetActive(true);
        yield return base.HandleWinCondition();
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
