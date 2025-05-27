using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_22Ctrl : BaseDragController<L22_ChargingCable>
{
    public L22_SmartPhone smartPhone;
    public float followSpeed = 5f;

    public Vector3 objToMouseOffset;
    Vector3 targetPosition;
    protected override void OnDragStarted()
    {

        objToMouseOffset = draggableComponent.transform.position - this.mouseWorldPos;
    }
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        targetPosition = currentMousePosition + objToMouseOffset;
        draggableComponent.transform.position = Vector3.Lerp(
            draggableComponent.transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
    }

    protected override void OnDragEnded()
    {

    }



}
