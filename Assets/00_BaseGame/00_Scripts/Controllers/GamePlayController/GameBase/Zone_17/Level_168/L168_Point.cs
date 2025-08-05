using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L168_Point : MonoBehaviour
{
    public List<L168_ItemProduct> lsItems = new List<L168_ItemProduct>();
    public int indexOrder = 2;

    // Method để remove item khỏi point
    public void RemoveItem(L168_ItemProduct item)
    {
        if (lsItems.Contains(item))
        {
            lsItems.Remove(item);
        }
    }

    // Method để add item vào point
    public void AddItem(L168_ItemProduct item)
    {
        if (!lsItems.Contains(item))
        {
            lsItems.Add(item);
        }
    }

    // Method để lấy item trên cùng
    public L168_ItemProduct GetTopItem()
    {
        L168_ItemProduct topItem = null;
        int highestSortingOrder = -1;

        foreach (var item in lsItems)
        {
            if (item != null && item.objRenderer.sortingOrder > highestSortingOrder)
            {
                highestSortingOrder = item.objRenderer.sortingOrder;
                topItem = item;
            }
        }

        return topItem;
    }
}
