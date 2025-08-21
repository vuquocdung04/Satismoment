using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Level_71Ctrl : Singleton<Level_71Ctrl>
{
    public int winProgress;
    public List<Transform> lsPoints; // Danh sách các slot (transform trống)
    public List<L71_Fruit> placedFruits;
    public bool hasLost;
    
    // Queue system để xử lý tuần tự
    private Queue<L71_Fruit> fruitQueue = new Queue<L71_Fruit>();
    private bool isProcessing;
    
    public void AddFruit(L71_Fruit fruit)
    {
        if (placedFruits.Contains(fruit)) return;
        
        // Thêm vào queue thay vì xử lý ngay
        fruitQueue.Enqueue(fruit);
        
        // Nếu không đang xử lý thì bắt đầu xử lý
        if (!isProcessing)
        {
            ProcessNextFruit();
        }
    }
    
    private void ProcessNextFruit()
    {
        // Nếu hết quả trong queue thì dừng
        if (fruitQueue.Count == 0)
        {
            isProcessing = false;
            return;
        }
        
        isProcessing = true;
        L71_Fruit fruit = fruitQueue.Dequeue();
        
        // Logic thêm quả vào list (giống như cũ)
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
            
            // Check win/lose conditions
            if (placedFruits.Count >= 7 && !hasLost)
            {
                hasLost = true;
                StartCoroutine(HandleLoseCodition());
                return; // Không xử lý quả tiếp theo nếu thua
            }
            if (winProgress == 7)
            {
                StartCoroutine(HandleWinCodition());
                return; // Không xử lý quả tiếp theo nếu thắng
            }
            
            // Tiếp tục xử lý quả tiếp theo trong queue
            ProcessNextFruit();
        });
    }

    IEnumerator HandleWinCodition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show(); 
    }
    
    IEnumerator HandleLoseCodition()
    {
        yield return new WaitForSeconds(0.5f);
        Initiate.Fade(SceneName.GAME_PLAY, Color.black, 3f);
    }

    public List<L71_Fruit> fruitsToRemove = new List<L71_Fruit>();
    
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

        if (fruitsToRemove.Count > 0)
        {
            winProgress++;
            HandleRemoveMatch();
        }
    }

    void HandleRemoveMatch()
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
            // Destroy objects
            foreach (L71_Fruit fruit in fruitsToRemove)
            {
                Destroy(fruit.gameObject);
            }
            fruitsToRemove.Clear();
            
            // Update lại vị trí các quả còn lại
            UpdateSlots();
        });
    }
}
