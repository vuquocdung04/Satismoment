using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_142Ctrl : BaseDragControllerVer2<L142_CeramicPiece>
{
    public L142_CeramicJar jar;
    protected override void OnDragEnded()
    {
        draggableComponent.HandleCorrectPosition(this, delegate
        {
            isWin = true;
            StartCoroutine(HandleWinCondition());
        });
        
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
    }

    protected override void SetupComponent_PositionCorrect()
    {
        foreach (var item in this.lsT_ItemDragables) item.InitCorrect();
    }

    protected override void SetupPositionDefault()
    {
        foreach (var item in this.lsT_ItemDragables) item.InitDefault();
    }
    [Button("Set position Correct",ButtonSizes.Large)]
    void SetupCorrect()
    {
        for(int i = 0; i < lsT_ItemDragables.Count; i++)
        {
            lsT_ItemDragables[i].transform.position = lsT_ItemDragables[i].posCorrect;
            jar.lsPoints[i].transform.position = lsT_ItemDragables[i].transform.position;
            lsT_ItemDragables[i].id = i;
            jar.lsPoints[i].id = i;
        }
    }
    [Button("Set position Default", ButtonSizes.Large)]
    void SetupDefault()
    {
        foreach (var item in this.lsT_ItemDragables) item.transform.position = item.posDefault;
    }

}
