using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L109_Compartment : MonoBehaviour
{
    public int id;
    public BoxCollider2D boxCollider2d;
    public List<Vector2> lsPoints = new List<Vector2>();

    // Lưu trữ các item đang gắn vào từng điểm
    private List<L109_Item> assignedItems;

    private void Start()
    {
        assignedItems = new List<L109_Item>(new L109_Item[lsPoints.Count]);
    }

    // Trả về index trống đầu tiên từ trái sang phải
    public int GetFirstEmptySlotIndex()
    {
        for (int i = 0; i < assignedItems.Count; i++)
        {
            if (assignedItems[i] == null)
                return i;
        }
        return -1; // Không còn chỗ trống
    }

    // Gán item vào vị trí gần nhất và trả về vị trí đó
    public Vector3 AssignItemToNearestAvailable(L109_Item item, Vector3 position)
    {
        float minDistance = float.MaxValue;
        int nearestIndex = -1;

        for (int i = 0; i < lsPoints.Count; i++)
        {
            if (assignedItems[i] != null) continue;

            float distance = Vector3.Distance(position, lsPoints[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestIndex = i;
            }
        }

        // Nếu tìm thấy vị trí gần và đủ điều kiện
        if (nearestIndex >= 0 && minDistance < 0.3f)
        {
            assignedItems[nearestIndex] = item;
            return lsPoints[nearestIndex]; // Trả về vị trí của điểm được gán
        }

        // Nếu không đủ gần, tìm ô trống từ trái sang phải
        int firstEmpty = GetFirstEmptySlotIndex();
        if (firstEmpty >= 0)
        {
            assignedItems[firstEmpty] = item;
            return lsPoints[firstEmpty]; // Trả về vị trí của điểm được gán
        }

        return Vector3.zero; // Không thể gán, trả về Vector3.zero (hoặc một giá trị không hợp lệ)
    }



    public List<Transform> lsSetups;
    [Button("Setup Point", ButtonSizes.Large)]
    void SetupPoint()
    {
        lsPoints.Clear();
        for (int i = 0; i < lsSetups.Count; i++)
        {
            lsPoints.Add(lsSetups[i].position);
        }

    }
}
