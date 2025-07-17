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

    protected override void SetupAfter()
    {
        foreach (var item in this.lsItems) item.InitAfter();
    }

    protected override void SetupBefore()
    {
        foreach (var item in this.lsItems) item.InitBefore();
    }
    [Button("Set position Correct",ButtonSizes.Large)]
    void SetupCorrect()
    {
        for(int i = 0; i < lsItems.Count; i++)
        {
            lsItems[i].transform.position = lsItems[i].posCorrect;
            jar.lsPoints[i].transform.position = lsItems[i].transform.position;
            lsItems[i].id = i;
            jar.lsPoints[i].id = i;
        }
    }
    [Button("Set position Default", ButtonSizes.Large)]
    void SetupDefault()
    {
        foreach (var item in this.lsItems) item.transform.position = item.posDefault;
    }

}
