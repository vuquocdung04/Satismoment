using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_168Ctrl : BaseDragController<L168_ItemProduct>
{

    protected override void OnDragEnded()
    {
        RaycastHit2D hit = Physics2D.Raycast(draggableComponent.transform.position, Vector2.zero);
        Debug.LogError(hit.collider.name);
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        
    }
}
