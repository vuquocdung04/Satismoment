using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_41Ctrl : BaseDragController<L41_Screw>
{
    public int winProgress;

    protected override void OnDragStarted()
    {

    }
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.DoScalingScrew();
    }

    protected override void OnDragEnded()
    {
        if (draggableComponent.isScale)
        {
            winProgress++;
            Destroy(draggableComponent.gameObject);
        }
        HandleWinCodition();
    }

    void HandleWinCodition()
    {
        if(winProgress > 5)
        {
            WinBox.SetUp().Show();
        }
    }
}
