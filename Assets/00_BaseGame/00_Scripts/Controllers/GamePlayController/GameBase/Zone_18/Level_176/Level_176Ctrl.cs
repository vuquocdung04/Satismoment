using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;
using UnityEngine.UIElements;

public class Level_176Ctrl : BaseDragController<L176_Apple>
{
    [Header("Setup matrix")]
    public Vector2 startPositionSpawn;
    public float spacing;
    public L176_Apple applePrefab;
    public List<Sprite> lsSprites;
    public List<L176_Apple> lsApples;
    private void Start()
    {
        InitMatrix();
    }

    protected override void OnDragEnded()
    {
        if (draggableComponent == null) return;

        // Tắt collider của quả táo đang kéo tạm thời
        draggableComponent.objCollider.enabled = false;

        // Raycast để tìm quả táo target
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        // Bật lại collider
        draggableComponent.objCollider.enabled = true;

        if (hit.collider != null)
        {
            L176_Apple targetApple = hit.collider.GetComponent<L176_Apple>();

            // Nếu tìm thấy quả táo khác
            if (targetApple != null)
            {
                // Swap 2 quả táo
                SwapApple(draggableComponent, targetApple);
            }
            else
            {
                ReturnAppleToOriginalPosition();
            }
        }
        else
        {
            ReturnAppleToOriginalPosition();
        }
        draggableComponent.objCollider.enabled = true;
        draggableComponent.objRenderer.sortingOrder = 2;
    }


    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.objRenderer.sortingOrder = 4;   
    }


    private void ReturnAppleToOriginalPosition()
    {
        // Trả về vị trí gốc được lưu trong thuộc tính position
        draggableComponent.transform.position = new Vector3(
            draggableComponent.position.x,
            draggableComponent.position.y,
            0f
        );
    }


    private void InitMatrix()
    {
        List<Sprite> newListSprite = new List<Sprite>(lsSprites);
        List<int> availableIds = new List<int>(); // List chứa các id có thể dùng

        // Tạo list id từ 0 đến số lượng sprite
        for (int i = 0; i < lsSprites.Count; i++)
        {
            availableIds.Add(i);
        }

        Debug.LogError(newListSprite.Count);

        for (int row = 0; row < 3; row++)
        {
            // Vòng for ngang (columns = 3)
            for (int col = 0; col < 3; col++)
            {
                // Tính vị trí cho từng object
                Vector3 position = new Vector3(
                    startPositionSpawn.x + col * spacing,  // X position
                    startPositionSpawn.y - row * spacing,  // Y position (trừ vì đi từ trên xuống)
                    0f                                // Z position
                );
                var appleClone = Instantiate(applePrefab, position, Quaternion.identity);
                appleClone.transform.SetParent(this.transform);
                lsApples.Add(appleClone);
                appleClone.position = position;

                // Random id từ list availableIds
                int randIdIndex = Random.Range(0, availableIds.Count);
                int spriteId = availableIds[randIdIndex];
                availableIds.RemoveAt(randIdIndex);

                // Lấy sprite tương ứng với id
                Sprite sprite = lsSprites[spriteId];
                appleClone.InitState(sprite);

                // Set id = index của sprite trong lsSprites
                appleClone.id = spriteId;
                appleClone.gameObject.name = "Apple_" + spriteId.ToString();
            }
        }
    }



    void SwapAppleAndCheck(int indexA, int indexB)
    {
        if (indexA < 0 || indexA >= lsApples.Count || indexB < 0 || indexB >= lsApples.Count)
        {
            Debug.LogWarning("Index swap không hợp lệ!");
            return;
        }

        // Hoán đổi thuộc tính position
        Vector2 tempPosition = lsApples[indexA].position;
        lsApples[indexA].position = lsApples[indexB].position;
        lsApples[indexB].position = tempPosition;

        // Cập nhật vị trí transform theo position mới
        lsApples[indexB].objRenderer.sortingOrder = 3;
        lsApples[indexA].transform.position = new Vector3(lsApples[indexA].position.x, lsApples[indexA].position.y, 0f);
        lsApples[indexB].transform.DOMove(tempPosition, 0.2f).SetEase(Ease.Linear).OnComplete(delegate
        {
            CheckCorrect();
            lsApples[indexB].objRenderer.sortingOrder = 2;
        });

        // Hoán đổi trong list
        L176_Apple tempApple = lsApples[indexA];
        lsApples[indexA] = lsApples[indexB];
        lsApples[indexB] = tempApple;
    }


    void SwapApple(L176_Apple appleA, L176_Apple appleB)
    {
        int indexA = lsApples.IndexOf(appleA);
        int indexB = lsApples.IndexOf(appleB);

        if (indexA != -1 && indexB != -1)
        {
           SwapAppleAndCheck(indexA, indexB);
        }
    }


    void CheckCorrect()
    {
        // Sắp xếp list trước khi check
        SortAppleList();

        bool isCorrect = true;
        for (int i = 0; i < lsApples.Count; i++)
        {
            int expectedId = lsApples.Count - 1 - i;

            if (lsApples[i].id != expectedId)
            {
                isCorrect = false;
                break;
            }
        }
        if (isCorrect)
            StartCoroutine(HandleWinCondition());
        Debug.Log("Check result: " + (isCorrect ? "Correct!" : "Incorrect!"));
    }

    void SortAppleList()
    {
        lsApples.Sort((a, b) =>
        {
            // So sánh Y trước (giảm dần)
            if (a.transform.position.y != b.transform.position.y)
                return b.transform.position.y.CompareTo(a.transform.position.y);

            // Nếu Y bằng nhau thì so sánh X (tăng dần)
            return a.transform.position.x.CompareTo(b.transform.position.x);
        });
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }
}
