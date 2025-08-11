using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_109Ctrl : BaseDragController<L109_Item>
{
    public int winProgress;
    public float maxDistanceX = 1.2f;
    public float maxDistanceY = 2;
    public List<L109_Compartment> lsCompartments;
    public List<L109_Item> lsItems;
    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckTochingWithZone())
        {
            winProgress++;
            CheckWin();
        }
        else
        {
            draggableComponent.OnDragEnded();
        }
    }

    Vector3 newPos;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.OnDragUpdate(newPos, mouseDelta, maxDistanceX, maxDistanceY);
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnDragStarted();
    }

    private bool CheckWin()
    {
        if (winProgress == lsItems.Count)
        {
            isWin = true;
            StartCoroutine(HandleWinCondition());
            return true;
        }
        return false;
    }


    private IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();

    }



    public L109_Compartment GetCompartmentByID(int id)
    {
        foreach (var compartment in this.lsCompartments)
        {
            if (compartment.id == id) return compartment;
        }
        return null;
    }

    [Button("Setup Item", ButtonSizes.Large)]
    void SetupItem()
    {
        foreach (var item in this.lsItems)
        {
            item.InitSetupOdin(this);
        }
    }

}
