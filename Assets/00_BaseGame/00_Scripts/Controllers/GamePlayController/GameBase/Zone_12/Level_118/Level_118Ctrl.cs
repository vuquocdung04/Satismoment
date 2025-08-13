using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_118Ctrl : BaseDragControllerVer2<L118_PicturePiece>
{
    
    protected override void OnDragEnded()
    {
        if (draggableComponent.IsAtZeroDegree())
        {
            winProgress++;
            if (winProgress == lsT_ItemDragables.Count)
                StartCoroutine(HandleWinCondition());
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        
    }

    protected override void OnDragStarted()
    {
        draggableComponent.Rotate();
    }
    protected override void SetupComponent_PositionCorrect()
    {
        foreach (var item in this.lsT_ItemDragables) item.Init();
    }

    protected override void SetupPositionDefault()
    {
        // K lam gi
    }
}
