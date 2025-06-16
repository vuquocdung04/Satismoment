using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level_71Ctrl : Singleton<Level_71Ctrl>
{
    public List<Transform> lsPoints; // Danh sách các slot (transform trống)
    private List<L71_Fruit> placedFruits = new List<L71_Fruit>(); // Danh sách các quả đã chọn
    public void AddFruit(L71_Fruit fruit)
    {
        if (placedFruits.Contains(fruit)) return;

        int insertIndex = placedFruits.Count;

        // Tìm vị trí cuối cùng của bất kỳ quả nào có cùng idFruit
        for (int i = placedFruits.Count - 1; i >= 0; i--)
        {
            if (placedFruits[i].idFruit == fruit.idFruit)
            {
                insertIndex = i + 1;
                break;
            }
        }

        placedFruits.Insert(insertIndex, fruit);

        UpdateSlots(); // Cập nhật vị trí cho từng quả
    }

    private void UpdateSlots()
    {
        // Tạo một sequence lớn để quản lý tất cả chuyển động
        Sequence masterSequence = DOTween.Sequence();

        for (int i = 0; i < placedFruits.Count && i < lsPoints.Count; i++)
        {
            L71_Fruit fruit = placedFruits[i];
            Transform targetSlot = lsPoints[i];

            // Lấy tween di chuyển từ mỗi quả và thêm vào sequence lớn
            // Dùng Join() để tất cả các quả di chuyển cùng lúc
            masterSequence.Join(fruit.GetMoveTween(targetSlot));
        }

        masterSequence.OnComplete(() =>
        {
            CheckAndRemoveTriple();
        });
    }
    List<L71_Fruit> fruitsToRemove = new List<L71_Fruit>();
    private void CheckAndRemoveTriple()
    {
        fruitsToRemove.Clear();
        for (int i = 0; i < placedFruits.Count - 2; i++)
        {
            L71_Fruit fruit1 = placedFruits[i];
            L71_Fruit fruit2 = placedFruits[i + 1];
            L71_Fruit fruit3 = placedFruits[i + 2];

            if (fruit1.idFruit == fruit2.idFruit && fruit1.idFruit == fruit3.idFruit)
            {
                if (!fruitsToRemove.Contains(fruit1)) fruitsToRemove.Add(fruit1);
                if (!fruitsToRemove.Contains(fruit2)) fruitsToRemove.Add(fruit2);
                if (!fruitsToRemove.Contains(fruit3)) fruitsToRemove.Add(fruit3);

                i += 2;
            }
        }

        // handle list
        if (fruitsToRemove.Count > 0)
        {
            foreach (L71_Fruit fruit in fruitsToRemove)
            {
                placedFruits.Remove(fruit);
            }
            Sequence destroySequence = DOTween.Sequence();

            foreach (L71_Fruit fruit in fruitsToRemove)
            {
                // Tạo tween co nhỏ và tự hủy.
                Tween scaleTween = fruit.transform.DOScale(Vector3.zero, 0.35f)
                    .SetEase(Ease.InBack);
                destroySequence.Join(scaleTween);
            }
            destroySequence.OnComplete(() =>
            {
                UpdateSlots();
            });
        }
    }
}