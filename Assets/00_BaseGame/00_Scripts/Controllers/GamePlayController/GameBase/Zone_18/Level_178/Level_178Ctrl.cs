using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_178Ctrl : BaseDragControllerVer2<L178_BrokenRecord>
{
    protected override void OnDragEnded()
    {
        draggableComponent.CheckCorrectToPosition(delegate
        {
            winProgress++;
            if(winProgress == lsT_ItemDragables.Count)
            {
                StartCoroutine(HandleWinCondition());
            }
        });
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
    }

    protected override void SetupComponent_PositionCorrect()
    {
        foreach(var brokenRecord in this.lsT_ItemDragables) brokenRecord.InitCorrect();
    }

    protected override void SetupPositionDefault()
    {
        foreach(var brokenRecord in this.lsT_ItemDragables) brokenRecord.InitDefault();
    }
}
