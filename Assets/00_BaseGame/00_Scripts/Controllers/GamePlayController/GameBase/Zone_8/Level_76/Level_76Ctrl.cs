using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using Sirenix.OdinInspector;

public class Level_76Ctrl : Singleton<Level_76Ctrl>
{
    
    [Header("Slots")]
    public List<Transform> lsPoints; // Danh sách vị trí cố định để đặt Tile

    [Header("Gameplay")]
    public List<L76_Tile> placedTiles = new List<L76_Tile>(); // Những Tile đã được đặt
    public int winProgress; // Số lần match thành công
    public int maxPlacedTiles = 7; // Giới hạn số Tile có thể đặt trước khi thua

    [Header("Win/Lose Conditions")]
    public bool hasLost;

    public List<L76_Tile> allTiles = new List<L76_Tile>(); // Tất cả Tile trong scene

    private void Start()
    {
        DarkenOverlappedTiles(); // Gọi ban đầu để làm tối những Tile bị che
    }

    public void OnTileClicked(L76_Tile clickedTile)
    {
        AddTile(clickedTile); // Thêm Tile vào hàng đợi
    }

    public void AddTile(L76_Tile tile)
    {
        if (placedTiles.Contains(tile) || hasLost)
            return;

        int insertIndex = placedTiles.Count;

        // Tìm vị trí cuối cùng của bất kỳ quả nào có cùng idFruit
        for (int i = placedTiles.Count - 1; i >= 0; i--)
        {
            if (placedTiles[i].animalType == tile.animalType)
            {
                insertIndex = i + 1;
                break;
            }
        }

        // Bật cờ isMoving cho tile được click
        tile.isMoving = true;
        placedTiles.Insert(insertIndex, tile);
        UpdateSlots(); // Bắt đầu hiệu ứng di chuyển
    }

    private void UpdateSlots()
    {
        Sequence masterSequence = DOTween.Sequence();

        for (int i = 0; i < placedTiles.Count && i < lsPoints.Count; i++)
        {
            L76_Tile currentTile = placedTiles[i];
            Transform targetSlot = lsPoints[i];

            masterSequence.Join(currentTile.GetMoveTween(targetSlot));
        }

        masterSequence.OnComplete(() =>
        {
            // Sau khi tất cả tile đã di chuyển xong, mới bắt đầu kiểm tra
            CheckAndRemoveTriple();
            CheckLoseCondition();
            CheckWinCodition();
            DarkenOverlappedTiles(); // Cập nhật màu sau khi các tile đã ở vị trí mới
        });
    }

    private void CheckAndRemoveTriple()
    {
        List<L76_Tile> fruitsToRemove = new List<L76_Tile>();

        for (int i = 0; i < placedTiles.Count - 2; i++)
        {
            L76_Tile t1 = placedTiles[i];
            L76_Tile t2 = placedTiles[i + 1];
            L76_Tile t3 = placedTiles[i + 2];

            if (t1.animalType == t2.animalType && t2.animalType == t3.animalType)
            {
                fruitsToRemove.Add(t1);
                fruitsToRemove.Add(t2);
                fruitsToRemove.Add(t3);
                i += 2;
            }
        }

        if (fruitsToRemove.Count > 0)
        {
            winProgress++;
            HandleRemoveMatch(fruitsToRemove);
        }
    }

    private void HandleRemoveMatch(List<L76_Tile> toRemove)
    {
        // Gỡ bỏ các tile khỏi danh sách ngay lập tức
        foreach (var tile in toRemove)
        {
            placedTiles.Remove(tile);
            allTiles.Remove(tile);
        }

        Sequence destroySequence = DOTween.Sequence();

        foreach (var tile in toRemove)
        {
            Tween scaleTween = tile.transform.DOScale(Vector3.zero, 0.35f)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    Destroy(tile.gameObject);
                });

            destroySequence.Join(scaleTween);
        }

        destroySequence.OnComplete(() =>
        {
            // Sau khi hiệu ứng xóa hoàn tất, gọi lại UpdateSlots() để dồn các tile còn lại
            UpdateSlots();
        });
    }

    private void CheckLoseCondition()
    {
        if (placedTiles.Count >= maxPlacedTiles && !hasLost)
        {
            hasLost = true;
            StartCoroutine(HandleLose());
        }
    }

    private void CheckWinCodition()
    {
        if(winProgress == 16)
        {
            StartCoroutine(HandleWin());
        }
    }

    IEnumerator HandleLose()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("You Lose!");
         Initiate.Fade(SceneName.GAME_PLAY, Color.black, 3f);
    }

    IEnumerator HandleWin()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("You Win!");
         WinBox.SetUp().Show();
    }

    private void DarkenOverlappedTiles()
    {
        // Bước 1: Khôi phục lại màu cho tất cả Tile trước khi xét lại
        foreach (var tile in allTiles)
        {
            if (!tile.isMoving) // Chỉ khôi phục nếu không đang di chuyển
            {
                tile.Restore();
            }
        }

        // Bước 2: Duyệt từng cặp Tile để kiểm tra chồng lấp và thứ tự hiển thị
        for (int i = 0; i < allTiles.Count; i++)
        {
            var frontTile = allTiles[i];

            for (int j = 0; j < allTiles.Count; j++)
            {
                if (i == j) continue;

                var backTile = allTiles[j];

                // Bỏ qua Tile đang di chuyển
                if (backTile.isMoving) continue;

                // Kiểm tra xem hai Tile có chồng lên nhau không bằng Collider
                if (IsOverlapping(frontTile.boxCollider2D, backTile.boxCollider2D))
                {
                    // Nếu backTile nằm dưới frontTile theo sortingOrder thì làm tối nó
                    if (backTile.spriteRenderer.sortingOrder < frontTile.spriteRenderer.sortingOrder)
                    {
                        backTile.Darken();
                    }
                }
            }
        }
    }

    // Hàm hỗ trợ kiểm tra 2 collider có chồng nhau không
    private bool IsOverlapping(BoxCollider2D colA, BoxCollider2D colB)
    {
        return colA.bounds.Intersects(colB.bounds);
    }

    [Button("Setup",ButtonSizes.Large)]
    void Setup()
    {
        foreach (var tile in this.allTiles)
        {
            tile.boxCollider2D = tile.transform.GetComponent<BoxCollider2D>();
            tile.boxCollider2D.size = new Vector2(0.54f, 0.54f);
            tile.boxCollider2D.offset = new Vector2(-0.04218856f, 0.04073071f);
        }
    }
}