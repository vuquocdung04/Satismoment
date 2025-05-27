using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_42Ctrl : BaseDragController<L42_item>
{
    public int winProgress = 0;
    public List<L42_item> lsItems;
    protected override void OnDragStarted()
    {
        
    }
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragEnded()
    {
        CheckSnapDistance();
        HandleWinCodition();
    }

    void CheckSnapDistance()
    {
        float distance = Vector2.Distance(draggableComponent.transform.position, draggableComponent.positionCorrect);

        if(distance < 0.2f)
        {
            winProgress++;
            draggableComponent.transform.position = draggableComponent.positionCorrect;
            draggableComponent._collider.enabled = false;
        }
        else
        {
            draggableComponent.SnapBackPostion(Ease.InBack);
        }
    }

    void HandleWinCodition()
    {
        if (winProgress == lsItems.Count)
        {
            isWin = true;
            WinBox.SetUp().Show();
        }
    }

    [Button("Setup")]
    void Setup()
    {
        foreach(var item in this.lsItems)
        {
            item.positionCorrect = item.transform.position;
            item.transform.position = item.positionDefault;
        }
    }
}
