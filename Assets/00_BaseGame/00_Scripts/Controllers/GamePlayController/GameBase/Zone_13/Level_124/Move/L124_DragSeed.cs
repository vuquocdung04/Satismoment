using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L124_DragSeed : BaseDragController<L124_Seed>
{
    protected override void OnDragEnded()
    {
        draggableComponent.HandleCollisitonWithDir();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        
    }
}
