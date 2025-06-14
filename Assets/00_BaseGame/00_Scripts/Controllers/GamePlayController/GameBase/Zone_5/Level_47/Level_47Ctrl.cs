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


        Vector3 pivotToMouse = currentMousePosition - draggableComponent.transform.position;
        float angleZ = Mathf.Atan2(pivotToMouse.y, pivotToMouse.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angleZ + 180f);

        draggableComponent.transform.rotation = Quaternion.Slerp(
            draggableComponent.transform.rotation,
            targetRotation,
            Time.deltaTime * 12f
        );
    }

    protected override void OnDragStarted()
    {
        currentObjectPosition = draggableComponent.transform.position;
    }
}
