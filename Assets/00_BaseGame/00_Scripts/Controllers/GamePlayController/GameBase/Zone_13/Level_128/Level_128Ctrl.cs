using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_128Ctrl : BaseDragControllerVer2<L128_Candy>
{
    public List<L128_Compartment> lsCompartments;
    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckleCorrectToPosition(GetCompartmentById(draggableComponent.id))){
            winProgress++;
            if(winProgress == lsT_ItemDragables.Count)
            {
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

    public L128_Compartment GetCompartmentById(int id )
    {
        foreach(var compartment in this.lsCompartments)
            if(compartment.id == id) return compartment;
        return null;
    }

    protected override IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return base.HandleWinCondition();
    }


    //ODin

    protected override void SetupComponent_PositionCorrect()
    {
        foreach(var item in this.lsT_ItemDragables)
        {
            item.InitCorrect();
            item.InitDefault();
        }
    }

    protected override void SetupPositionDefault()
    {
        
    }
}
