using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_77Ctrl : BaseDragController<L77_Picture>
{
    public float pictureHeight;
    public float startPosY = -3.15f;
    public float spacingBetweenPicture = 0.2f;
    public List<L77_Picture> lsPictures;

    private void Start()
    {
        HandleArrange(null, true);
    }

    protected override void OnDragEnded()
    {
        HandleArrange(null, true);
        draggableComponent.spriteRenderer.sortingOrder = 2;
        StartCoroutine(HandleWinCodition());
    }
    Vector3 newPicturePos;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        newPicturePos = draggableComponent.transform.position += new Vector3(0, mouseDelta.y, 0);
        newPicturePos.x = 0;
        newPicturePos.y = Mathf.Clamp(newPicturePos.y, -4f, 4f);
        draggableComponent.transform.position = newPicturePos;

        HandleArrange(draggableComponent, false);

    }

    protected override void OnDragStarted()
    {
        draggableComponent.spriteRenderer.sortingOrder = 3;

    }
    void HandleArrange(L77_Picture draggedPicture, bool snapPosition = false)
    {
        // Sắp xếp lại danh sách theo Y tăng dần
        lsPictures.Sort((a, b) => a.transform.position.y.CompareTo(b.transform.position.y));
        L77_Picture picture  = null;
        Vector3 targetPosition;
        float currentY = startPosY;

        for(int i = 0; i < lsPictures.Count; i++)
        {
            picture = lsPictures[i];
            if (picture == null) continue;

            targetPosition = new Vector3(0, currentY, 0);

            // bo qua tranh hien tai neu dang keo va snap
            if (picture == draggedPicture && !snapPosition)
            {

            }
            else
            {
                if (snapPosition) picture.transform.position = targetPosition;
                else
                {
                    picture.transform.position = Vector3.Lerp(picture.transform.position, targetPosition, 10f * Time.deltaTime);
                }
            }
            currentY += pictureHeight + spacingBetweenPicture;
        }
    }

    bool CheckWinCondition()
    {
        for (int i = 0; i < lsPictures.Count; i++)
        {
            if (lsPictures[i].idPicture != (lsPictures.Count - 1 - i))
            {
                return false;
            }
        }
        return true;
    }
    IEnumerator HandleWinCodition()
    {
        if (CheckWinCondition())
        {
            isWin = true;
            yield return new WaitForSeconds(0.5f);
            WinBox.SetUp().Show();
        }
    }

    [Button("ShufflePicture", ButtonSizes.Large)]
    void ShufflePictures()
    {
        // Bước 1: Lưu trữ vị trí hiện tại của tất cả tranh
        List<Vector3> originalPositions = new List<Vector3>();
        foreach (var picture in lsPictures)
        {
            originalPositions.Add(picture.transform.position);
        }

        // Bước 2: Xáo trộn danh sách vị trí
        for (int i = 0; i < originalPositions.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, originalPositions.Count);
            Vector3 temp = originalPositions[i];
            originalPositions[i] = originalPositions[randomIndex];
            originalPositions[randomIndex] = temp;
        }

        // Bước 3: Gán lại vị trí mới cho từng tranh
        for (int i = 0; i < lsPictures.Count; i++)
        {
            lsPictures[i].transform.position = originalPositions[i];
        }
    }

    [Button("Setup Picture", ButtonSizes.Large)]
    void Setup()
    {
        for(int i = 0; i < lsPictures.Count; i++)
        {
            lsPictures[i].idPicture = i;
            lsPictures[i].spriteRenderer = lsPictures[i].transform.GetComponent<SpriteRenderer>();
        }
        pictureHeight = lsPictures[0].spriteRenderer.bounds.size.y;
        startPosY = lsPictures[0].transform.position.y - pictureHeight / 2f;
    }
}
