using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_108Ctrl : BaseDragController<L108_Item>
{
    public float maxDistanceX = 1.2f;
    public float maxDistanceY = 2;
    protected override void OnDragEnded()
    {
        draggableComponent.OnDragEnded();
    }

    Vector3 newPos;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.OnDragUpdate(newPos,mouseDelta,maxDistanceX,maxDistanceY);
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnDragStarted();
    }

}
