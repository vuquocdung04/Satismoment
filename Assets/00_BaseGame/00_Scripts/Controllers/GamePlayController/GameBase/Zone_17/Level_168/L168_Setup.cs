
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class L168_Setup : MonoBehaviour
{
    [Header("Prefabs & Data")]
    public L168_ItemProduct productPrefab;
    public List<Sprite> lsSprites;
    public List<Transform> lsPoints;           // 54 points (0-53)

    [HideInInspector] public readonly List<L168_ItemProduct> createdItems = new();

    /* Lưu giá trị sortingOrder cao nhất ứng với mỗi point */
    private readonly Dictionary<int, int> pointTopOrder = new();

    /******************************************************/
    /* ----------------    UNITY LIFE    --------------- */
    /******************************************************/
    private void Start()
    {
        CreateItemsRound(42, 2);   // round 1 – order bắt đầu = 2
        CreateItemsRound(18, 3);   // round 2 – order bắt đầu = 3
        UpdateCoveredItems();      // tính che phủ ban đầu
    }

    private void CreateItemsRound(int totalItem, int baseOrder)
    {
        int curItem = 0;
        const int batchSize = 3;   // sinh theo nhóm 3

        while (curItem < totalItem)
        {
            int spriteIdx = Random.Range(0, lsSprites.Count);
            Sprite sprite = lsSprites[spriteIdx];

            for (int i = 0; i < batchSize && curItem < totalItem; i++)
            {
                int pointIdx = Random.Range(0, lsPoints.Count);
                Transform pointTr = lsPoints[pointIdx];

                /* ---- Tính order = top + 1 (hoặc baseOrder nếu point trống) ---- */
                int nextOrder = GetNextOrderForPoint(pointIdx, baseOrder);

                var item = Instantiate(productPrefab, pointTr.position, Quaternion.identity);
                item.InitSprite(sprite, spriteIdx);
                item.SetSortingOrder(nextOrder);
                item.SetOriginalPosition(pointTr.position);
                item.SetPointIndex(pointIdx);

                createdItems.Add(item);
                curItem++;
            }
        }
    }

    private int GetNextOrderForPoint(int pointIdx, int baseOrder)
    {
        if (pointTopOrder.TryGetValue(pointIdx, out int top))
        {
            top += 1;
            pointTopOrder[pointIdx] = top;
            return top;
        }
        else
        {
            pointTopOrder[pointIdx] = baseOrder;
            return baseOrder;
        }
    }

    /******************************************************/
    /* -------------   UPDATE COVERED FLAG   ------------ */
    /******************************************************/
    public void UpdateCoveredItems()
    {
        /* Reset flag */
        foreach (var it in createdItems) it.SetCovered(false);

        /* So sánh bounds từng cặp */
        for (int i = 0; i < createdItems.Count; i++)
        {
            var a = createdItems[i];
            Bounds boundsA = a.objRenderer.bounds;

            for (int j = i + 1; j < createdItems.Count; j++)
            {
                var b = createdItems[j];
                Bounds boundsB = b.objRenderer.bounds;

                if (!boundsA.Intersects(boundsB)) continue;

                /* Item có order thấp hơn ⇒ bị che */
                if (b.objRenderer.sortingOrder < a.objRenderer.sortingOrder)
                    b.SetCovered(true);
                else if (a.objRenderer.sortingOrder < b.objRenderer.sortingOrder)
                    a.SetCovered(true);
            }
        }
    }

    /******************************************************/
    /* --------  CHECK & DESTROY 3-POINT COMBO   -------- */
    /******************************************************/
    public void CheckAndDestroyCombo()
    {
        UpdateCoveredItems();   // đảm bảo flag mới nhất

        for (int start = 0; start < lsPoints.Count; start += 3)
        {
            int p0 = start, p1 = start + 1, p2 = start + 2;
            if (p2 >= lsPoints.Count) break;

            /* Lấy đúng 1 item hiển thị ở mỗi point */
            var rep0 = GetVisibleItemAtPoint(p0);
            var rep1 = GetVisibleItemAtPoint(p1);
            var rep2 = GetVisibleItemAtPoint(p2);

            if (rep0 == null || rep1 == null || rep2 == null) continue;

            bool sameSprite = rep0.spriteId == rep1.spriteId && rep1.spriteId == rep2.spriteId;
            bool sameOrder = rep0.objRenderer.sortingOrder ==
                              rep1.objRenderer.sortingOrder &&
                              rep1.objRenderer.sortingOrder ==
                              rep2.objRenderer.sortingOrder;

            if (!sameSprite || !sameOrder) continue;

            /* ---- DESTROY combo ---- */
            DestroyItem(rep0);
            DestroyItem(rep1);
            DestroyItem(rep2);

            Debug.Log($"Combo destroyed at points {p0}-{p2} (spriteId {rep0.spriteId}, order {rep0.objRenderer.sortingOrder})");

            UpdateCoveredItems();   // cập nhật lại sau khi xoá
            return;                 // xử lý 1 combo / lần gọi
        }
    }

    private L168_ItemProduct GetVisibleItemAtPoint(int pointIdx)
    {
        /* Chỉ lấy item KHÔNG bị che và có order cao nhất */
        L168_ItemProduct top = null;
        int highest = int.MinValue;

        foreach (var it in createdItems)
        {
            if (it.pointIndex != pointIdx) continue;
            if (it.IsCovered) continue;

            int order = it.objRenderer.sortingOrder;
            if (order > highest)
            {
                highest = order;
                top = it;
            }
        }
        return top;
    }

    private void DestroyItem(L168_ItemProduct item)
    {
        createdItems.Remove(item);
        Destroy(item.gameObject);
    }
}
