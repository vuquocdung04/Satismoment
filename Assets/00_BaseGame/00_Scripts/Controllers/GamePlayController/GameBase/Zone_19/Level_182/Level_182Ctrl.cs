
using UnityEngine;

public class Level_182Ctrl : BaseDragController<L182_Piece>
{
    protected override void OnDragStarted()
    {
        
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragEnded()
    {
        
    }
}
