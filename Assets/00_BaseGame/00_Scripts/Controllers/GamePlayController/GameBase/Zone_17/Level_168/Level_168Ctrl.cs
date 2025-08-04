using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Level_168Ctrl : BaseDragController<L168_ItemProduct>
{
    [SerializeField] private L168_Setup gameSetup;
    [SerializeField] private float snapDistance = 2f;

    protected override bool CanStartDragCondition(L168_ItemProduct component)
    {
        return !component.IsCovered;
    }

    protected override void OnDragStarted()
    {
            draggableComponent.SetOriginalPosition(draggableComponent.transform.position);
            draggableComponent.SetSortingOrder(100);
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        if(!draggableComponent.IsCovered)
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragEnded()
    {
        /* 1. Tìm point gần nhất */
        Transform nearestPoint = FindNearestPoint(draggableComponent.transform.position);

        /* 2. Đưa item về đúng point hoặc trả về chỗ cũ */
        if (nearestPoint != null &&
            Vector3.Distance(draggableComponent.transform.position, nearestPoint.position) <= snapDistance)
        {
            HandleItemPlacement(nearestPoint);
        }
        else
        {
            ReturnToOriginalPosition();
        }

        /* 3. HẠ order về đúng tầng NGAY BÂY GIỜ */
        draggableComponent.SetSortingOrder(
            draggableComponent.objRenderer.sortingOrder < 50 ? 2 : 3);   // hoặc bất kỳ quy tắc nào bạn muốn

        /* 4. Cập-nhật che phủ & phá combo */
        gameSetup.UpdateCoveredItems();
        gameSetup.CheckAndDestroyCombo();
    }


    private Transform FindNearestPoint(Vector3 position)
    {
        Transform nearest = null;
        float minDistance = float.MaxValue;

        for (int i = 0; i < gameSetup.lsPoints.Count; i++)
        {
            Transform point = gameSetup.lsPoints[i];
            float distance = Vector3.Distance(position, point.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = point;
            }
        }

        return nearest;
    }

    private void HandleItemPlacement(Transform targetPoint)
    {
        // Tìm index của target point
        int targetPointIndex = gameSetup.lsPoints.IndexOf(targetPoint);

        List<L168_ItemProduct> itemsAtTarget = GetItemsAtPoint(targetPoint);
        List<L168_ItemProduct> uncoveredItemsAtTarget = itemsAtTarget.Where(item => !item.IsCovered).ToList();

        if (uncoveredItemsAtTarget.Count > 0)
        {
            SwapItems(draggableComponent, uncoveredItemsAtTarget[0]);
        }
        else
        {
            MoveItemToPoint(draggableComponent, targetPoint, targetPointIndex);
        }

        // Kiểm tra combo theo nhóm 3 points liên tiếp
        gameSetup.CheckAndDestroyCombo();

        // Cập nhật trạng thái che phủ
        gameSetup.UpdateCoveredItems();
    }

    private void SwapItems(L168_ItemProduct item1, L168_ItemProduct item2)
    {
        Vector3 tempPos = item1.originalPosition;
        int tempPointIndex = item1.pointIndex;

        item1.transform.position = item2.originalPosition;
        item1.SetOriginalPosition(item2.originalPosition);
        item1.SetPointIndex(item2.pointIndex);

        item2.transform.position = tempPos;
        item2.SetOriginalPosition(tempPos);
        item2.SetPointIndex(tempPointIndex);
    }

    private void MoveItemToPoint(L168_ItemProduct item, Transform point, int pointIndex)
    {
        item.transform.position = point.position;
        item.SetOriginalPosition(point.position);
        item.SetPointIndex(pointIndex);
    }

    private void ReturnToOriginalPosition()
    {
        draggableComponent.transform.position = draggableComponent.originalPosition;
    }

    private List<L168_ItemProduct> GetItemsAtPoint(Transform point)
    {
        List<L168_ItemProduct> itemsAtPoint = new List<L168_ItemProduct>();

        foreach (L168_ItemProduct item in gameSetup.createdItems)
        {
            if (Vector3.Distance(item.transform.position, point.position) < 0.1f)
            {
                itemsAtPoint.Add(item);
            }
        }

        return itemsAtPoint;
    }
}
