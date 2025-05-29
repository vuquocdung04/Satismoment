using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_47Ctrl : BaseDragController<L47_Hand>
{
    protected override void OnDragEnded()
    {
        currentObjectPosition = draggableComponent.transform.position;
    }

    private Vector3 currentObjectPosition;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        currentObjectPosition.x += mouseDelta.x;

        draggableComponent.transform.position = currentObjectPosition;
    }

    protected override void OnDragStarted()
    {
        currentObjectPosition = draggableComponent.transform.position;
    }
}
