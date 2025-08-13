using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_137Ctrl : BaseDragControllerVer2<L137_Animal>
{
    public List<Transform> lsGlass;

    private void Start()
    {
        foreach (var glass in this.lsGlass) glass.gameObject.SetActive(false);
    }
    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckTheCorrectPosition())
        {
            winProgress++;
            draggableComponent.HandleTheCorrectCondition(lsGlass[draggableComponent.id]);

            if (winProgress == lsT_ItemDragables.Count)
            {
                isWin = true;
                StartCoroutine(HandleWinCondition());
            }
        }
        else
        {
            draggableComponent.OnEndDrag();
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
    }


    //ODin
    protected override void SetupComponent_PositionCorrect()
    {
        for(int i = 0; i < lsT_ItemDragables.Count; i++)
        {
            lsT_ItemDragables[i].id = i;
            lsT_ItemDragables[i].InitCorrect();
        }
    }

    protected override void SetupPositionDefault()
    {
        foreach(var item in this.lsT_ItemDragables) item.InitDefault();
    }
}
