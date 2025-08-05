using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class L168_Setup : MonoBehaviour
{
    [Header("Prefabs & Data")]
    public L168_ItemProduct productPrefab;
    
    public List<Sprite> lsSprites;
    public List<L168_Point> lsPoints;           // 54 points (0-53)

    [HideInInspector] public readonly List<L168_ItemProduct> createdItems = new();
    private void Start()
    {
        CreateItemsRound(42);
        CreateItemsRound(18);

        // Sau khi tạo xong tất cả items, check covered
        CheckCoveredItems();
    }

    private void CreateItemsRound(int totalItem)
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
                L168_Point pointTr = lsPoints[pointIdx];

                // Tạo item
                var item = Instantiate(productPrefab, pointTr.transform.position, Quaternion.identity);
                item.InitSprite(sprite, spriteIdx);
                item.SetOriginalPosition(pointTr.transform.position);

                // Set sorting order dựa trên số lượng items đã có tại point
                item.SetSortingOrder(pointTr.indexOrder + pointTr.lsItems.Count);
                item.SetPointIndex(pointTr.indexOrder + pointTr.lsItems.Count);

                // Thêm vào point và created items
                pointTr.lsItems.Add(item);
                createdItems.Add(item);
                curItem++;
            }
        }
    }

    private void CheckCoveredItems()
    {
        // Reset tất cả items về trạng thái không bị che
        foreach (var item in createdItems)
        {
            item.SetCovered(false);
        }

        // Check từng item xem có bị che bởi item nào khác không
        for (int i = 0; i < createdItems.Count; i++)
        {
            var currentItem = createdItems[i];

            // Lấy bounds của item hiện tại
            Bounds currentBounds = GetItemBounds(currentItem);

            // Check với tất cả các items khác
            for (int j = 0; j < createdItems.Count; j++)
            {
                if (i == j) continue; // Bỏ qua chính nó

                var otherItem = createdItems[j];

                // Chỉ check nếu otherItem có sorting order cao hơn (ở trên)
                if (otherItem.objRenderer.sortingOrder > currentItem.objRenderer.sortingOrder)
                {
                    Bounds otherBounds = GetItemBounds(otherItem);

                    // Nếu bounds intersect thì item hiện tại bị che
                    if (currentBounds.Intersects(otherBounds))
                    {
                        currentItem.SetCovered(true);
                        break; // Đã bị che rồi thì không cần check nữa
                    }
                }
            }
        }
    }

    private Bounds GetItemBounds(L168_ItemProduct item)
    {
        // Lấy bounds từ SpriteRenderer
        return item.objRenderer.bounds;
    }

    // Method để gọi lại check covered khi cần (ví dụ sau khi move item)
    public void RefreshCoveredStatus()
    {
        CheckCoveredItems();
    }
}
