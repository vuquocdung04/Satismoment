using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_115DraggSalt : BaseDragController<L115_SaltContainer>
{
    public Level_115Ctrl levelCtrl;
    protected override void OnDragEnded()
    {
        draggableComponent.OnDraggEnded();
    }

    Vector3 newPos;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        levelCtrl.totalSaltTime += Time.deltaTime;
        newPos = draggableComponent.transform.position + new Vector3(mouseDelta.x,0,0);
        newPos.x = Mathf.Clamp(newPos.x, -0.6f, 2.3f);
        draggableComponent.transform.position = newPos;
        CheckDoneState();
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnDraggStarted();
    }

    void CheckDoneState()
    {
        if (levelCtrl.totalSaltTime > 3f)
        {
            isWin = true;
            draggableComponent.Complete();
            levelCtrl.SetIsWin();
        }
    }
}
