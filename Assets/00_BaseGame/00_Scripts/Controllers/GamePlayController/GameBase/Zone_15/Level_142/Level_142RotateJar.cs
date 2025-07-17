using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_142RotateJar : BaseDragController<L142_CeramicJar>
{
    protected override void OnDragEnded()
    {
        
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.RotateJar(mouseDelta);
    }

    protected override void OnDragStarted()
    {
        
    }
}
