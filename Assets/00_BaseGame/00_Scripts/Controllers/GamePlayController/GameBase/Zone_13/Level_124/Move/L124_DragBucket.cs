using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L124_DragBucket : BaseDragController<L124_Bucket>
{
    protected override void OnDragEnded()
    {
        draggableComponent.HandleCollisionWithSeed();
        draggableComponent.HandleCollisionWithWaterWell();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {

    }

}
