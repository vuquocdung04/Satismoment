using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class L168_Setup : MonoBehaviour
{
    public L168_ItemProduct productPrefab;
    public List<Sprite> lsSprites;
    public List<Transform> lsPoints;

    private List<L168_ItemProduct> round1Items = new List<L168_ItemProduct>();
    private List<L168_ItemProduct> round2Items = new List<L168_ItemProduct>();
    private Dictionary<int, List<int>> shelfItems = new Dictionary<int, List<int>>(); // shelf index -> sprite indices

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        // Initialize shelf tracking
        InitializeShelfTracking();

        // First round: 42 items (sorting order 2)
        CreateItemsRound1();

        // Second round: 24 items (sorting order 3) - will be placed on top of existing items
        CreateItemsRound2();

        // Update visibility - only round1 items can be darkened when covered by round2
        UpdateItemsVisibility();
    }

    private void InitializeShelfTracking()
    {
        for (int i = 0; i < 18; i++)
        {
            shelfItems[i] = new List<int>();
        }
    }

    private void CreateItemsRound1()
    {
        Debug.Log("Starting Round 1 - Creating exactly 42 items");

        // Create sprite distribution that totals exactly 42 items
        Dictionary<int, int> spriteCount = GenerateSpriteDistribution(42);
        List<int> spritePool = CreateSpritePool(spriteCount);

        // Get 42 random points from 54 available points
        List<int> selectedPoints = GetRandomPoints(42);

        // Try to place items while respecting shelf constraints
        int itemsCreated = 0;
        int attempts = 0;
        int maxAttempts = 200; // Prevent infinite loop

        while (itemsCreated < 42 && attempts < maxAttempts && spritePool.Count > 0)
        {
            attempts++;

            // Pick random sprite and point
            int spriteIndex = Random.Range(0, spritePool.Count);
            int spriteId = spritePool[spriteIndex];

            int pointIndex = Random.Range(0, selectedPoints.Count);
            int pointId = selectedPoints[pointIndex];
            int shelfIndex = pointId / 3;

            // Check shelf constraint
            if (shelfItems[shelfIndex].Count(x => x == spriteId) < 3)
            {
                // Create item
                L168_ItemProduct item = CreateItem(pointId, spriteId, 2);
                if (item != null)
                {
                    shelfItems[shelfIndex].Add(spriteId);
                    round1Items.Add(item);
                    itemsCreated++;

                    // Remove used sprite and point
                    spritePool.RemoveAt(spriteIndex);
                    selectedPoints.RemoveAt(pointIndex);
                }
            }
        }

        Debug.Log($"Round 1 completed - Created {itemsCreated} items");
    }

    private void CreateItemsRound2()
    {
        Debug.Log("Starting Round 2 - Creating exactly 24 items");

        // Create sprite distribution that totals exactly 24 items
        Dictionary<int, int> spriteCount = GenerateSpriteDistribution(24);
        List<int> spritePool = CreateSpritePool(spriteCount);

        // Get 24 random points from all 54 points (can overlap with round1)
        List<int> selectedPoints = GetRandomPoints(24);

        // Create a temporary shelf tracking for round 2 (independent from round 1)
        Dictionary<int, List<int>> round2ShelfItems = new Dictionary<int, List<int>>();
        for (int i = 0; i < 18; i++)
        {
            round2ShelfItems[i] = new List<int>();
        }

        int itemsCreated = 0;
        int attempts = 0;
        int maxAttempts = 200;

        while (itemsCreated < 24 && attempts < maxAttempts && spritePool.Count > 0)
        {
            attempts++;

            // Pick random sprite and point
            int spriteIndex = Random.Range(0, spritePool.Count);
            int spriteId = spritePool[spriteIndex];

            int pointIndex = Random.Range(0, selectedPoints.Count);
            int pointId = selectedPoints[pointIndex];
            int shelfIndex = pointId / 3;

            // Check shelf constraint for round 2 only
            if (round2ShelfItems[shelfIndex].Count(x => x == spriteId) < 3)
            {
                // Create item
                L168_ItemProduct item = CreateItem(pointId, spriteId, 3);
                if (item != null)
                {
                    round2ShelfItems[shelfIndex].Add(spriteId);
                    round2Items.Add(item);
                    itemsCreated++;

                    // Remove used sprite and point
                    spritePool.RemoveAt(spriteIndex);
                    selectedPoints.RemoveAt(pointIndex);
                }
            }
        }

        Debug.Log($"Round 2 completed - Created {itemsCreated} items");
    }

    private List<int> GetRandomPoints(int count)
    {
        List<int> allPoints = Enumerable.Range(0, 54).ToList();
        ShuffleList(allPoints);
        return allPoints.Take(count).ToList();
    }

    private Dictionary<int, int> GenerateSpriteDistribution(int totalItems)
    {
        Dictionary<int, int> spriteCount = new Dictionary<int, int>();
        int remainingItems = totalItems;

        // Available counts that are multiples of 3
        List<int> possibleCounts = new List<int> { 0, 3, 6, 9 };

        // Shuffle sprite indices for random distribution
        List<int> spriteIndices = Enumerable.Range(0, lsSprites.Count).ToList();
        ShuffleList(spriteIndices);

        foreach (int spriteIndex in spriteIndices)
        {
            if (remainingItems <= 0) break;

            // Filter possible counts that don't exceed remaining items
            List<int> validCounts = possibleCounts.Where(x => x <= remainingItems).ToList();

            if (validCounts.Count > 0)
            {
                int count = validCounts[Random.Range(0, validCounts.Count)];
                if (count > 0)
                {
                    spriteCount[spriteIndex] = count;
                    remainingItems -= count;
                }
            }
        }

        // If we still have remaining items, force distribute them
        while (remainingItems > 0)
        {
            foreach (int spriteIndex in spriteIndices)
            {
                if (remainingItems <= 0) break;

                if (!spriteCount.ContainsKey(spriteIndex))
                    spriteCount[spriteIndex] = 0;

                // Add 3 items if possible
                if (remainingItems >= 3 && spriteCount[spriteIndex] + 3 <= 9)
                {
                    spriteCount[spriteIndex] += 3;
                    remainingItems -= 3;
                }
            }

            // Safety break to prevent infinite loop
            if (remainingItems > 0)
            {
                Debug.LogWarning($"Could not distribute all {totalItems} items. {remainingItems} items remaining.");
                break;
            }
        }

        return spriteCount;
    }

    private List<int> CreateSpritePool(Dictionary<int, int> spriteCount)
    {
        List<int> pool = new List<int>();
        foreach (var kvp in spriteCount)
        {
            for (int i = 0; i < kvp.Value; i++)
            {
                pool.Add(kvp.Key);
            }
        }
        ShuffleList(pool);
        return pool;
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private L168_ItemProduct CreateItem(int pointIndex, int spriteIndex, int sortingOrder)
    {
        if (pointIndex >= lsPoints.Count || spriteIndex >= lsSprites.Count)
            return null;

        Transform point = lsPoints[pointIndex];
        L168_ItemProduct item = Instantiate(productPrefab, point.position, point.rotation);

        // Position at the point
        item.transform.position = point.position;

        item.InitSprite(lsSprites[spriteIndex]);

        // Set sorting order
        if (item.objRenderer != null)
        {
            item.objRenderer.sortingOrder = sortingOrder;
        }

        return item;
    }

    private void UpdateItemsVisibility()
    {
        Debug.Log("Updating items visibility");

        // Only round1 items can be darkened when covered by round2 items
        foreach (var round1Item in round1Items)
        {
            if (round1Item == null || round1Item.objRenderer == null) continue;

            bool isObscured = false;
            Bounds round1Bounds = round1Item.objRenderer.bounds;

            // Check if this round1 item is obscured by any round2 item
            foreach (var round2Item in round2Items)
            {
                if (round2Item == null || round2Item.objRenderer == null) continue;

                if (round1Bounds.Intersects(round2Item.objRenderer.bounds))
                {
                    isObscured = true;
                    break;
                }
            }

            // Adjust color based on visibility
            if (isObscured)
            {
                // Darken the round1 item
                round1Item.objRenderer.color = new Color(0.6f, 0.6f, 0.6f, 1f);
            }
            else
            {
                // Restore to full brightness
                round1Item.objRenderer.color = Color.white;
            }
        }

        // Round2 items always stay bright
        foreach (var round2Item in round2Items)
        {
            if (round2Item != null && round2Item.objRenderer != null)
            {
                round2Item.objRenderer.color = Color.white;
            }
        }

        Debug.Log($"Total items: Round1={round1Items.Count}, Round2={round2Items.Count}, Total={round1Items.Count + round2Items.Count}");
    }

    // Call this method if you want to refresh visibility after moving items
    public void RefreshVisibility()
    {
        UpdateItemsVisibility();
    }
}
