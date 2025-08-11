using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class L124_DragObjCtrl : BaseDragController<L124_ObjDragable>
{
    protected override void OnDragEnded()
    {
        HandleCheckCollision();
        currentBucket = null;
        currentApple = null;
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        
    }

    L124_Bucket currentBucket;
    L124_Apple currentApple;

    void HandleCheckCollision()
    {
        switch (draggableComponent.objType)
        {
            case L124_ObjType.Bucket:
                draggableComponent.HandleCollisionWithObj();
                currentBucket = (L124_Bucket)draggableComponent;
                currentBucket.HandleCollisionWithWaterWell();
                break;
            case L124_ObjType.Seed:
                draggableComponent.HandleCollisionWithObj();
                break;
            case L124_ObjType.Apple:
                draggableComponent.HandleCollisionWithObj();
                currentApple = (L124_Apple)draggableComponent;
                currentApple.CheckTochingWithZone();
                break;
        }
    }

}
