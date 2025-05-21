using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq; // Cần cho LINQ (ví dụ: Select, Distinct, OrderBy) nếu dùng cách nhóm kệ phức tạp hơn
using UnityEngine;

public class Level_28Ctrl : BaseDragController<L28_Bottle>
{
    public int winProgress = 0;
    public float snapDistance = 0.4f;
    public List<L28_Bottle> lsBottles;
    protected override void OnDragStarted()
    {
        base.OnDragStarted();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        if(!draggableComponent.isMove)
         draggableComponent.transform.position += deltaMousePosition;
    }

    protected override void OnDragEnded()
    {
        base.OnDragEnded();

        L28_Bottle draggedBottle = draggableComponent;
        L28_Bottle closesBottleToSwapWith = null;
        float shortestDistanceFound = snapDistance;

        foreach (var otherBottle in lsBottles)
        {
            if (otherBottle == draggedBottle) continue;

            float distance = Vector2.Distance(
                new Vector2(draggedBottle.transform.position.x, draggedBottle.transform.position.y),
                new Vector2(otherBottle.transform.position.x, otherBottle.transform.position.y)
            );

            if (distance < shortestDistanceFound)
            {
                shortestDistanceFound = distance;
                closesBottleToSwapWith = otherBottle;
            }
        }

        if (closesBottleToSwapWith != null && !closesBottleToSwapWith.isMove)
        {
            StartCoroutine(HandleSwapAndCheckWin(draggedBottle, closesBottleToSwapWith));
        }
        else
        {
            if (draggedBottle != null) 
            {
                draggedBottle.transform.DOMove(draggedBottle.posDefault, 0.3f);
            }
        }
    }

    IEnumerator HandleSwapAndCheckWin(L28_Bottle bottleA, L28_Bottle bottleB)
    {
        Vector2 slotPosForA_target = bottleB.posDefault;
        Vector2 slotPosForB_target = bottleA.posDefault;

        bottleA.posDefault = slotPosForA_target;
        bottleB.posDefault = slotPosForB_target;

        bottleA.transform.position = slotPosForA_target;

        // Chai B di chuyển từ từ đến vị trí slot cũ của A
        Tween tweenB = bottleB.transform.DOMove(slotPosForB_target, 0.3f);

        // Đợi tween của B hoàn thành (nếu có)
        if (tweenB != null && tweenB.IsActive())
        {
            yield return tweenB.WaitForCompletion();
        }
        else
        {
            // Nếu không có tween hoặc tween không active, đảm bảo vị trí cuối cùng được đặt
            bottleB.transform.position = slotPosForB_target;
            yield return null; // Đợi một frame cho chắc
        }

        // Sau khi tất cả di chuyển đã hoàn tất, kiểm tra điều kiện thắng
        HandleWinCondition();
    }

    void HandleWinCondition()
    {
        Dictionary<int, List<L28_Bottle>> shelves = new Dictionary<int, List<L28_Bottle>>();
        float yPositionMultiplier = 10f;

        foreach (L28_Bottle bottle in lsBottles)
        {
            int shelfKey = Mathf.RoundToInt(bottle.transform.position.y * yPositionMultiplier);
            if (!shelves.ContainsKey(shelfKey))
            {
                shelves[shelfKey] = new List<L28_Bottle>();
            }
            shelves[shelfKey].Add(bottle);
        }

        foreach (var shelfKey in shelves.Keys)
        {
            List<L28_Bottle> bottlesOnThisShelf = shelves[shelfKey];

            bottlesOnThisShelf.Sort((a, b) => a.transform.position.x.CompareTo(b.transform.position.x));

            // 3. Kiểm tra 3 chai liên tiếp có cùng idBottle.
            if (bottlesOnThisShelf.Count >= 3)
            {
                for (int i = 0; i <= bottlesOnThisShelf.Count - 3; i++)
                {
                    L28_Bottle b1 = bottlesOnThisShelf[i];
                    L28_Bottle b2 = bottlesOnThisShelf[i + 1];
                    L28_Bottle b3 = bottlesOnThisShelf[i + 2];

                    if (b1.idBottle == b2.idBottle && b2.idBottle == b3.idBottle)
                    {
                        b1.isMove = true;
                        b2.isMove = true;
                        b3.isMove = true;
                        winProgress++;
                    }
                }
            }
        }
    }
}