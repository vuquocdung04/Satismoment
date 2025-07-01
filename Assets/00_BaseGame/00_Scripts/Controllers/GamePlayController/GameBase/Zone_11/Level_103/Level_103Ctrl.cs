using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_103Ctrl : BaseDragControllerVer2<L103_Ring>
{

    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckAngleCorrect())
        {
            winProgress++;
            if (winProgress == lsItems.Count)
                StartCoroutine(HandleWinCondition());
        }
    }
    float rotationAmount;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        if (!draggableComponent.isDone)
        {
            rotationAmount = mouseDelta.y * 30f;
            draggableComponent.transform.Rotate(0, 0, rotationAmount);
        }
        
    }

    float distance;
    float minDistance;
    L103_Ring closestItem;
    protected override void OnDragStarted()
    {
        closestItem = null;
        minDistance = float.MaxValue;
        distance = 0;
        // Tìm item gần chuột nhất
        foreach (var item in this.lsItems)
        {
            if (item.isDone) continue;
            distance = Vector2.Distance(item.transform.position, mouseWorldPos);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestItem = item;
            }
        }

        // Gán draggableComponent dựa trên khoảng cách nếu tìm thấy item gần nhất
        if (closestItem != null)
        {
            if (minDistance < 0.7f)
                draggableComponent = lsItems[0]; 
            else if (minDistance < 1f)
                draggableComponent = lsItems[1];
            else if (minDistance < 1.3f)
                draggableComponent = lsItems[2];
            else
                draggableComponent = null;
        }
    }

    public override IEnumerator HandleWinCondition()
    {
        isWin = true;
        return base.HandleWinCondition();
    }


    protected override void SetupAfter()
    {
        foreach(var item in this.lsItems)
        {
            item.InitAfter();
        }
    }

    protected override void SetupBefore()
    {
        foreach (var item in this.lsItems)
        {
            item.InitBefore();
        }
    }

}
