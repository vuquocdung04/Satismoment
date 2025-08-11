using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using DG.Tweening;

public class Level_168Ctrl : BaseDragController<L168_ItemProduct>
{
    [SerializeField] private L168_Setup gameSetup;
    [SerializeField] private float snapDistance = 2f;
    public int winProgress = 0;
    public int totalWin = 0;

    // Lưu vị trí ban đầu của item khi bắt đầu drag
    private Vector3 originalDragPosition;
    private void Start()
    {
        totalWin = (42 + 18)/3;   
    }

    protected override bool CanStartDragCondition(L168_ItemProduct component)
    {
        return !component.IsCovered;
    }

    protected override void OnDragStarted()
    {
        originalDragPosition = draggableComponent.transform.position;
        draggableComponent.SetOriginalPosition(originalDragPosition);
        draggableComponent.SetSortingOrder(100);
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        if (!draggableComponent.IsCovered)
            draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragEnded()
    {
        L168_Point targetPoint = FindNearestPoint();

        if (targetPoint != null)
        {
            ProcessItemPlacement(targetPoint);
        }
        else
        {
            // Không tìm thấy point gần, trở về vị trí ban đầu
            ReturnToOriginalPosition();
        }

        // Reset sorting order
        ResetItemSortingOrder(draggableComponent);

        // Refresh covered status sau khi di chuyển
        gameSetup.RefreshCoveredStatus();
        Debug.LogError("refesh");

        // Check combo
        CheckAndDestroyCombo();
    }

    private L168_Point FindNearestPoint()
    {
        L168_Point nearestPoint = null;
        float nearestDistance = float.MaxValue;

        foreach (var point in gameSetup.lsPoints)
        {
            float distance = Vector3.Distance(draggableComponent.transform.position, point.transform.position);
            if (distance < snapDistance && distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestPoint = point;
            }
        }

        return nearestPoint;
    }

    private void ProcessItemPlacement(L168_Point targetPoint)
    {
        // Tìm item trên cùng tại target point
        L168_ItemProduct topItemAtTarget = GetTopItemAtPoint(targetPoint);

        if (topItemAtTarget == null)
        {
            // Vị trí trống - đặt item vào luôn
            PlaceItemAtPoint(draggableComponent, targetPoint);
        }
        else
        {
            if (topItemAtTarget.IsCovered)
            {
                // Item tối màu - đặt lên trên
                PlaceItemOnTop(draggableComponent, targetPoint);
            }
            else
            {
                // Item sáng màu - swap
                SwapItems(draggableComponent, topItemAtTarget);
            }
        }
    }

    private L168_ItemProduct GetTopItemAtPoint(L168_Point point)
    {
        L168_ItemProduct topItem = null;
        int highestSortingOrder = -1;

        foreach (var item in point.lsItems)
        {
            if (item != null && item.objRenderer.sortingOrder > highestSortingOrder)
            {
                highestSortingOrder = item.objRenderer.sortingOrder;
                topItem = item;
            }
        }

        return topItem;
    }

    private void PlaceItemAtPoint(L168_ItemProduct item, L168_Point targetPoint)
    {
        // Remove từ point cũ
        RemoveItemFromCurrentPoint(item);

        // Set vị trí mới
        item.transform.position = targetPoint.transform.position;
        item.SetOriginalPosition(targetPoint.transform.position);

        // Add vào point mới
        targetPoint.AddItem(item);

        // Set sorting order
        int newSortingOrder = targetPoint.indexOrder + targetPoint.lsItems.Count - 1;
        item.SetSortingOrder(newSortingOrder);
        item.SetPointIndex(newSortingOrder);
    }

    private void PlaceItemOnTop(L168_ItemProduct item, L168_Point targetPoint)
    {
        // Remove từ point cũ
        RemoveItemFromCurrentPoint(item);

        // Set vị trí mới
        item.transform.position = targetPoint.transform.position;
        item.SetOriginalPosition(targetPoint.transform.position);

        // Add vào point mới
        targetPoint.AddItem(item);

        // Set sorting order cao nhất tại point này
        int maxSortingOrder = targetPoint.indexOrder;
        foreach (var existingItem in targetPoint.lsItems)
        {
            if (existingItem != item && existingItem.objRenderer.sortingOrder > maxSortingOrder)
            {
                maxSortingOrder = existingItem.objRenderer.sortingOrder;
            }
        }

        item.SetSortingOrder(maxSortingOrder + 1);
        item.SetPointIndex(maxSortingOrder + 1);
    }

    private void SwapItems(L168_ItemProduct draggedItem, L168_ItemProduct targetItem)
    {
        // Lưu thông tin của cả 2 items
        Vector3 draggedOriginalPos = originalDragPosition;
        Vector3 targetOriginalPos = targetItem.originalPosition;
        L168_Point draggedOriginalPoint = FindPointContainingItem(draggedItem);
        L168_Point targetOriginalPoint = FindPointContainingItem(targetItem);

        // Remove cả 2 items từ points hiện tại
        if (draggedOriginalPoint != null) draggedOriginalPoint.RemoveItem(draggedItem);
        if (targetOriginalPoint != null) targetOriginalPoint.RemoveItem(targetItem);

        // Swap positions
        draggedItem.transform.position = targetOriginalPos;
        draggedItem.SetOriginalPosition(targetOriginalPos);
        targetItem.transform.position = draggedOriginalPos;
        targetItem.SetOriginalPosition(draggedOriginalPos);

        // Add vào points mới
        if (targetOriginalPoint != null) targetOriginalPoint.AddItem(draggedItem);
        if (draggedOriginalPoint != null) draggedOriginalPoint.AddItem(targetItem);

        // Reset sorting orders
        ResetItemSortingOrder(draggedItem);
        ResetItemSortingOrder(targetItem);
    }

    private void RemoveItemFromCurrentPoint(L168_ItemProduct item)
    {
        L168_Point currentPoint = FindPointContainingItem(item);
        currentPoint?.RemoveItem(item);
    }

    private L168_Point FindPointContainingItem(L168_ItemProduct item)
    {
        return gameSetup.lsPoints.FirstOrDefault(point => point.lsItems.Contains(item));
    }

    private void ReturnToOriginalPosition()
    {
        draggableComponent.transform.position = originalDragPosition;
    }

    private void ResetItemSortingOrder(L168_ItemProduct item)
    {
        L168_Point point = FindPointContainingItem(item);
        if (point != null)
        {
            int index = point.lsItems.IndexOf(item);
            int sortingOrder = point.indexOrder + index;
            item.SetSortingOrder(sortingOrder);
            item.SetPointIndex(sortingOrder);
        }
    }

    private void CheckAndDestroyCombo()
    {
        // Check từng nhóm 3 points liên tiếp (0-1-2, 3-4-5, 6-7-8, ...)
        for (int groupStart = 0; groupStart < gameSetup.lsPoints.Count; groupStart += 3)
        {
            if (groupStart + 2 >= gameSetup.lsPoints.Count) break;

            List<L168_Point> pointGroup = new List<L168_Point>()
            {
                gameSetup.lsPoints[groupStart],
                gameSetup.lsPoints[groupStart + 1],
                gameSetup.lsPoints[groupStart + 2]
            };

            CheckComboInGroup(pointGroup);
        }
    }

    private void CheckComboInGroup(List<L168_Point> pointGroup)
    {
        // Lấy top item của mỗi point
        List<L168_ItemProduct> topItems = new List<L168_ItemProduct>();

        foreach (var point in pointGroup)
        {
            L168_ItemProduct topItem = GetTopItemAtPoint(point);
            topItems.Add(topItem);
        }

        // Check nếu có đủ 3 items
        if (topItems.All(item => item != null))
        {
            // Check nếu cùng spriteId và đều sáng màu (không bị covered)
            int firstSpriteId = topItems[0].spriteId;
            bool allSameSprite = topItems.All(item => item.spriteId == firstSpriteId);
            bool allBright = topItems.All(item => !item.IsCovered);

            if (allSameSprite && allBright)
            {
                // Destroy combo
                StartCoroutine(DestroyComboCoroutine(topItems));
            }
        }
    }

    private IEnumerator DestroyComboCoroutine(List<L168_ItemProduct> comboItems)
    {
        comboItems[0].transform.DOMove(comboItems[1].transform.position,0.1f).SetEase(Ease.OutBack);
        comboItems[2].transform.DOMove(comboItems[1].transform.position,0.1f).SetEase(Ease.OutBack);
        // Visual effect hoặc animation có thể thêm ở đây
        yield return new WaitForSeconds(0.21f);

        foreach (var item in comboItems)
        {
            // Remove từ point
            RemoveItemFromCurrentPoint(item);

            // Remove từ created items list
            gameSetup.createdItems.Remove(item);

            // Destroy object
            Destroy(item.gameObject);
        }
        CheckWin();
        yield return new WaitForSeconds(0.1f);
        gameSetup.RefreshCoveredStatus();
    }

    void CheckWin()
    {
        winProgress++;
        if (winProgress == totalWin)
        {
            isWin = true;
            StartCoroutine(HandleWinCondition());
        }
    }
    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.2f);
        WinBox.SetUp().Show();
    }
}
