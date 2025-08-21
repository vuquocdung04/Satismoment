using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Level_71Ctrl : Singleton<Level_71Ctrl>
{
    
    [Header("Slots")]
    public List<Transform> lsPoints; // Danh sách các slot (transform trống)

    [Header("Gameplay")]
    public List<L71_Fruit> placedFruits = new List<L71_Fruit>(); // Những quả đã được đặt
    public int winProgress; // Số lần match thành công
    public int maxPlacedFruits = 7; // Giới hạn số quả có thể đặt trước khi thua

    [Header("Win/Lose Conditions")]
    public bool hasLost;
    
    public void OnFruitClicked(L71_Fruit clickedFruit)
    {
        AddFruit(clickedFruit);
    }

    private void AddFruit(L71_Fruit fruit)
    {
        if (placedFruits.Contains(fruit) || hasLost)
            return;
        
        int insertIndex = placedFruits.Count;
        for (int i = placedFruits.Count - 1; i >= 0; i--)
        {
            if (placedFruits[i].idFruit == fruit.idFruit)
            {
                insertIndex = i + 1;
                break;
            }
        }
        
        placedFruits.Insert(insertIndex, fruit);
        UpdateSlots();
    }
    
    private void UpdateSlots()
    {
        Sequence masterSequence = DOTween.Sequence();

        for (int i = 0; i < placedFruits.Count && i < lsPoints.Count; i++)
        {
            L71_Fruit currentFruit = placedFruits[i];
            Transform targetSlot = lsPoints[i];

            masterSequence.Join(currentFruit.GetMoveTween(targetSlot));
        }

        masterSequence.OnComplete(() =>
        {
            CheckAndRemoveTriple();
            CheckLoseCondition();
            CheckWinCodition();
        });
    }

    private void CheckLoseCondition()
    {
        if (placedFruits.Count >= maxPlacedFruits && !hasLost)
        {
            hasLost = true;
            StartCoroutine(HandleLoseCodition());
        }
    }

    private void CheckWinCodition()
    {
        if (winProgress == 7)
        {
            StartCoroutine(HandleWinCodition());
        }
    }

    private IEnumerator HandleWinCodition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show(); 
    }

    private IEnumerator HandleLoseCodition()
    {
        yield return new WaitForSeconds(0.5f);
        Initiate.Fade(SceneName.GAME_PLAY, Color.black, 3f);
    }

    public List<L71_Fruit> fruitsToRemove = new();
    
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
                fruitsToRemove.Add(fruit1);
                fruitsToRemove.Add(fruit2);
                fruitsToRemove.Add(fruit3);

                i += 2;
            }
        }

        if (fruitsToRemove.Count > 0)
        {
            winProgress++;
            HandleRemoveMatch();
        }
    }

    private void HandleRemoveMatch()
    {
        // Gỡ bỏ các quả khỏi danh sách ngay lập tức
        foreach (L71_Fruit fruit in fruitsToRemove)
        {
            placedFruits.Remove(fruit);
        }
        
        Sequence destroySequence = DOTween.Sequence();

        foreach (L71_Fruit fruit in fruitsToRemove)
        {
            // Tạo tween co nhỏ. Bỏ việc destroy object trong callback của từng tween riêng lẻ
            Tween scaleTween = fruit.transform.DOScale(Vector3.zero, 0.35f).SetEase(Ease.InBack);
            destroySequence.Join(scaleTween);
        }
        
        destroySequence.OnComplete(() =>
        {
            // Hủy các đối tượng sau khi toàn bộ hiệu ứng đã hoàn tất
            foreach (L71_Fruit fruit in fruitsToRemove)
            {
                // Thêm kiểm tra null để chắc chắn đối tượng chưa bị hủy
                if (fruit != null) 
                {
                    Debug.LogError("Destroy");
                    fruit.transform.DOKill();
                    Destroy(fruit.gameObject);
                }
                else
                {
                    Debug.LogError("fruit is null");
                }
            }
            fruitsToRemove.Clear();
            
            // Cập nhật lại vị trí các quả còn lại
            UpdateSlots();
        });
    }
}