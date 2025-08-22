using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using Sirenix.OdinInspector;

public class Level_76Ctrl : Singleton<Level_76Ctrl>
{
    [Header("Slots")]
    public List<Transform> lsPoints;

    [Header("Gameplay")]
    public List<L76_Tile> placedTiles = new();
    public List<L76_Tile> arrangedTiles = new();
    public int tripleRemovedCount;
    public int maxPlacedSlots = 7;
    public int totalPossibleTriples;

    [Header("Win/Lose Conditions")]
    public bool hasLost;
    public bool hasWon;
    private bool isRemovingTriple;
    private bool isCheckingOrRemoving; // Ngăn gọi CheckAndRemoveTriple nhiều lần

    public List<L76_Tile> allTiles = new List<L76_Tile>();

    private void Start()
    {
        totalPossibleTriples = allTiles.Count / 3;
        Debug.Log($"Total possible triples: {totalPossibleTriples}");
        DarkenOverlappedTiles();
    }

    public void OnTileClicked(L76_Tile clickedTile)
    {
        if (allTiles.Contains(clickedTile))
            allTiles.Remove(clickedTile);
        AddTile(clickedTile);
    }

    public void AddTile(L76_Tile tile)
    {
        if (tile == null || placedTiles.Contains(tile) || hasLost || hasWon)
            return;

        int insertIndex = placedTiles.Count;

        for (int i = placedTiles.Count - 1; i >= 0; i--)
        {
            if (i < placedTiles.Count && placedTiles[i] != null && 
                placedTiles[i].animalType == tile.animalType)
            {
                insertIndex = i + 1;
                break;
            }
        }

        tile.isMoving = true;
        placedTiles.Insert(insertIndex, tile);
        UpdateSlots();
    }

    private void UpdateSlots()
    {
        arrangedTiles.Clear();

        Sequence moveSequence = DOTween.Sequence();
        int totalTilesToMove = Mathf.Min(placedTiles.Count, lsPoints.Count);
        int moveCompletedCount = 0;
        int validTweens = 0;

        for (int i = 0; i < totalTilesToMove; i++)
        {
            if (i >= placedTiles.Count || i >= lsPoints.Count) break;
            
            L76_Tile currentTile = placedTiles[i];
            Transform targetSlot = lsPoints[i];

            if (currentTile == null || targetSlot == null || currentTile.gameObject == null) continue;

            Tween moveTween = currentTile.GetMoveTween(targetSlot, () =>
            {
                if (currentTile != null && !arrangedTiles.Contains(currentTile))
                {
                    arrangedTiles.Add(currentTile);
                }

                moveCompletedCount++;
                
                if (moveCompletedCount >= totalTilesToMove)
                {
                    if (!isCheckingOrRemoving)
                    {
                        CheckAndRemoveTriple();
                    }
                    DarkenOverlappedTiles();
                }
            });
            
            moveSequence.Join(moveTween);
            validTweens++;
        }

        if (validTweens > 0)
        {
            moveSequence.Play();
        }
        else
        {
            moveSequence.Kill();
        }
    }

    private void CheckAndRemoveTriple()
    {
        if (isCheckingOrRemoving) return;
        isCheckingOrRemoving = true;

        List<L76_Tile> fruitsToRemove = new List<L76_Tile>();

        for (int i = 0; i < arrangedTiles.Count - 2; i++)
        {
            if (i >= arrangedTiles.Count - 2) break;
            
            L76_Tile t1 = arrangedTiles[i];
            L76_Tile t2 = arrangedTiles[i + 1];
            L76_Tile t3 = arrangedTiles[i + 2];

            if (t1 != null && t2 != null && t3 != null &&
                t1.animalType == t2.animalType && t2.animalType == t3.animalType)
            {
                fruitsToRemove.Add(t1);
                fruitsToRemove.Add(t2);
                fruitsToRemove.Add(t3);
                i += 2;
            }
        }

        if (fruitsToRemove.Count > 0)
        {
            isRemovingTriple = true;
            HandleRemoveMatch(fruitsToRemove);
        }
        else
        {
            CheckLoseCondition();
            isCheckingOrRemoving = false;
        }
    }

    private void HandleRemoveMatch(List<L76_Tile> toRemove)
    {
        Debug.Log($"Removing triple, current count: {tripleRemovedCount}");

        // Dọn dẹp danh sách và kill tween
        foreach (var tile in toRemove)
        {
            if (tile != null)
            {
                tile.KillAllTweens();

                placedTiles.Remove(tile);
                arrangedTiles.Remove(tile);
                allTiles.Remove(tile);
            }
        }

        tripleRemovedCount++;
        CheckWinCondition();

        Sequence destroySequence = DOTween.Sequence();

        foreach (var tile in toRemove)
        {
            if (tile == null || tile.gameObject == null) continue;

            Tween scaleTween = tile.transform.DOScale(Vector3.zero, 0.35f)
                .SetEase(Ease.InBack);

            destroySequence.Join(scaleTween);
        }

        destroySequence.OnComplete(() =>
        {
            // Destroy sau khi tween xong
            foreach (var tile in toRemove)
            {
                if (tile != null && tile.gameObject != null)
                {
                    tile.gameObject.SetActive(false);
                }
            }

            isRemovingTriple = false;
            isCheckingOrRemoving = false;
            UpdateSlots(); // Cập nhật lại giao diện
        });

        destroySequence.Play();
    }

    private void CheckLoseCondition()
    {
        if (placedTiles.Count >= maxPlacedSlots && !hasLost)
        {
            bool hasTriple = false;
            for (int i = 0; i < placedTiles.Count - 2; i++)
            {
                if (i < lsPoints.Count - 2 && i < placedTiles.Count - 2)
                {
                    L76_Tile t1 = placedTiles[i];
                    L76_Tile t2 = placedTiles[i + 1];
                    L76_Tile t3 = placedTiles[i + 2];

                    if (t1 != null && t2 != null && t3 != null &&
                        t1.animalType == t2.animalType && t2.animalType == t3.animalType)
                    {
                        hasTriple = true;
                        break;
                    }
                }
            }

            if (!hasTriple)
            {
                hasLost = true;
                StartCoroutine(HandleLose());
            }
        }

        isCheckingOrRemoving = false; // Đảm bảo mở khóa nếu chưa kịp
    }

    private void CheckWinCondition()
    {
        Debug.Log($"CheckWin: tripleRemovedCount={tripleRemovedCount}, totalPossibleTriples={totalPossibleTriples}");
        if (tripleRemovedCount >= totalPossibleTriples && !hasWon)
        {
            hasWon = true;
            Debug.Log("Win condition met!");
            StartCoroutine(HandleWin());
        }
    }

    IEnumerator HandleLose()
    {
        DOTween.KillAll();
        yield return new WaitForSeconds(0.5f);
        Debug.Log("You Lose!");
        Initiate.Fade(SceneName.GAME_PLAY, Color.black, 3f);
    }

    IEnumerator HandleWin()
    {
        DOTween.KillAll();
        yield return new WaitForSeconds(0.5f);
        Debug.Log("You Win!");
        WinBox.SetUp().Show();
    }

    private void DarkenOverlappedTiles()
    {
        foreach (var tile in allTiles)
        {
            if (tile != null && !tile.isMoving)
            {
                tile.Restore();
            }
        }

        for (int i = 0; i < allTiles.Count; i++)
        {
            if (i >= allTiles.Count) break;
            
            var frontTile = allTiles[i];
            if (frontTile == null || frontTile.isMoving) continue;

            for (int j = 0; j < allTiles.Count; j++)
            {
                if (i == j) continue;
                if (j >= allTiles.Count) break;

                var backTile = allTiles[j];
                if (backTile == null || backTile.isMoving) continue;

                if (IsOverlapping(frontTile.boxCollider2D, backTile.boxCollider2D))
                {
                    if (backTile.spriteRenderer != null && frontTile.spriteRenderer != null &&
                        backTile.spriteRenderer.sortingOrder < frontTile.spriteRenderer.sortingOrder)
                    {
                        backTile.Darken();
                    }
                }
            }
        }
    }

    private bool IsOverlapping(BoxCollider2D colA, BoxCollider2D colB)
    {
        if (colA == null || colB == null) return false;
        return colA.bounds.Intersects(colB.bounds);
    }

    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        foreach (var tile in this.allTiles)
        {
            if (tile != null && tile.transform != null)
            {
                tile.boxCollider2D = tile.transform.GetComponent<BoxCollider2D>();
                if (tile.boxCollider2D != null)
                {
                    tile.boxCollider2D.size = new Vector2(0.54f, 0.54f);
                    tile.boxCollider2D.offset = new Vector2(-0.04218856f, 0.04073071f);
                }
            }
        }
    }
}