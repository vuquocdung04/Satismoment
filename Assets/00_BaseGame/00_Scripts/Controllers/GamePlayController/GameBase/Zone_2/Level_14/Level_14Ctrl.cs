using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_14Ctrl : BaseDragController<L14_Nozzle>
{
    public int winProgress = 0;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
        draggableComponent.SpawnWater();
    }

    protected override void OnDragEnded()
    {
        base.OnDragEnded();
        draggableComponent.transform.position = new Vector3(-1,-3);
        if(winProgress > 3)
        {
            WinBox.SetUp().Show();
        }
    }

    protected override void OnDragStarted()
    {
        base.OnDragStarted();
    }


}
