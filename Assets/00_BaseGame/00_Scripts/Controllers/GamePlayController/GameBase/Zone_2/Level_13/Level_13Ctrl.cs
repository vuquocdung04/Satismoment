using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_13Ctrl : BaseDragController<L13_Pen>
{
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.eulerAngles = Vector3.zero;
        draggableComponent.transform.Translate(mouseDelta);
    }
}
