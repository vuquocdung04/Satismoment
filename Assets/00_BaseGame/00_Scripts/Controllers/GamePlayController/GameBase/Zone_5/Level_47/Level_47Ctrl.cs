using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_47Ctrl : BaseDragController<L47_Hand>
{
    protected override void OnDragEnded()
    {
        
    }
    Vector3 newPosition;
    
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        newPosition = draggableComponent.transform.position + new Vector3(mouseDelta.x, 0, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, -2, 2);
        draggableComponent.transform.position = newPosition;
    }

    protected override void OnDragStarted()
    {
        
    }
}
