using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_123Ctrl : BaseDragController<L123_HairClipper>
{
    public L123_Cat cat;

    protected override void OnDragEnded()
    {
        draggableComponent.OnStateEnd();
        cat.ChangeSpriteDefault();

    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStateStart();
        cat.ChangeSpriteSuprise();
    }
}
