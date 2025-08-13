using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_117Ctrl : BaseDragControllerVer2<L117_PieceGlass>
{
    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckCorrectPosition())
        {
            winProgress++;
            if (CheckWin())
            {
                StartCoroutine(HandleWinCondition());
            }
        }
        else
        {
            draggableComponent.OnEndDrag();
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

    bool CheckWin()
    {
        if(winProgress == lsT_ItemDragables.Count)
        {
            return true;
        }
        return false;
    }

    protected override IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return base.HandleWinCondition();
    }


    // Setup Odin
    protected override void SetupComponent_PositionCorrect()
    {
        foreach (var piece in this.lsT_ItemDragables) piece.InitCorrect();
    }

    protected override void SetupPositionDefault()
    {
        foreach (var piece in this.lsT_ItemDragables) piece.InitDefault();
    }

    
}
