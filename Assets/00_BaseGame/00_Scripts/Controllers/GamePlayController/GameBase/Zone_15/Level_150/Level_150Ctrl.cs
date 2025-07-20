using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_150Ctrl : BaseDragController<L150_Fruit>
{
    protected override void OnDragEnded()
    {
        
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.Drag(mouseDelta);
    }

    protected override void OnDragStarted()
    {
        
    }
}
