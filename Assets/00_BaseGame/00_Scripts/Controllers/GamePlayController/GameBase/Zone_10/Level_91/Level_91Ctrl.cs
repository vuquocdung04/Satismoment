using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_91Ctrl : BaseDragController<L91_Item>
{
    public int winProgress;
    public L91_CatHand catHand;
    public List<L91_Item> lsItems;
    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckTochingWithZone())
        {
            winProgress++;
            if (winProgress == lsItems.Count)
                StartCoroutine(HandleWinCondition());
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

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return StartCoroutine(catHand.AnimateHand());
        yield return new WaitForSeconds(0.2f);
        WinBox.SetUp().Show();
    }

    [Button("SetupAfter",ButtonSizes.Large)]
    void SetupAfter()
    {
        foreach(var item in this.lsItems)
        {
            item.InitAfter();
        }
    }
    [Button("SetupBefore",ButtonSizes.Large)]

    void SetupBefore()
    {
        foreach (var item in this.lsItems)
        {
            item.InitBefore();
        }
    }

}
