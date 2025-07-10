using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L124_DragApple : BaseDragController<L124_Apple>
{
    protected override void OnDragEnded()
    {
        draggableComponent.HandleCollisionWithPenguin();

        if (draggableComponent.CheckTochingWithZone())
        {

        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        
    }
}
