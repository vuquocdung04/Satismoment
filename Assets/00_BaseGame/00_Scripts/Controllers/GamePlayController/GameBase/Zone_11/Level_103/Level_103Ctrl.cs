using System.Collections;
using UnityEngine;

public class Level_103Ctrl : BaseDragControllerVer2<L103_Ring>
{

    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckAngleCorrect())
        {
            winProgress++;
            GameController.Instance.musicManager.PlayPlace();
            if (winProgress == lsT_ItemDragables.Count)
                StartCoroutine(HandleWinCondition());
        }
    }
    float angle;
    Vector3 objectCenter;
    Vector2 vectorToPrevMouse;
    Vector2 vectorToCurrentMouse;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        if (!draggableComponent.isDone)
        {
            objectCenter = draggableComponent.transform.position;

            vectorToPrevMouse = (Vector2)prevMouseWorldPos - (Vector2)objectCenter;

            vectorToCurrentMouse = (Vector2)currentMousePosition - (Vector2)objectCenter;

            angle = Vector2.SignedAngle(vectorToPrevMouse, vectorToCurrentMouse);


            draggableComponent.transform.Rotate(0, 0, angle);
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
        GameController.Instance.musicManager.PlayPick();
        // Tìm item gần chuột nhất
        foreach (var item in this.lsT_ItemDragables)
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
                draggableComponent = lsT_ItemDragables[0]; 
            else if (minDistance < 1f)
                draggableComponent = lsT_ItemDragables[1];
            else if (minDistance < 1.3f)
                draggableComponent = lsT_ItemDragables[2];
            else
                draggableComponent = null;
        }
    }

    protected override IEnumerator HandleWinCondition()
    {
        isWin = true;
        return base.HandleWinCondition();
    }


    protected override void SetupComponent_PositionCorrect()
    {
        foreach(var item in this.lsT_ItemDragables)
        {
            item.InitCorrect();
        }
    }

    protected override void SetupPositionDefault()
    {
        foreach (var item in this.lsT_ItemDragables)
        {
            item.InitDefault();
        }
    }

}
