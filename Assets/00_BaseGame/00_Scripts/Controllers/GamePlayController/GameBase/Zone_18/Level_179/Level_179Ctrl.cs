using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_179Ctrl : MonoBehaviour
{
    public Transform picture;
    public L179_Len lenOutSide;
    public L179_Len lenInSide;
    Vector2 limit;
    float bondInside;
    float bondOutside;  
    Vector3 mousePos;
    Vector3 prevMousePos;
    L179_Len curLen;

    [Header("Win Condition")]
    public bool isWin = false;
    public float winAngleTolerance = 5f; // Khoảng từ -5° đến +5° để win

    private void Start()
    {
        lenInSide.Init();
        lenOutSide.Init();
        limit = lenInSide.transform.position;
        Debug.LogError(limit);
        bondInside = lenInSide.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        bondOutside = lenOutSide.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        Debug.LogError(bondInside);

        InitializeBlur();
    }

    private void Update()
    {
        if (isWin) return;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider == null) return;

            float distance = Vector2.Distance(mousePos, limit);
            Debug.Log(distance);
            if (distance > bondOutside) return;

            if (distance > bondInside - 0.2f)
            {
                curLen = lenOutSide;
            }
            else
            {
                curLen = lenInSide;
            }

            prevMousePos = mousePos;
            Debug.LogError(curLen.gameObject.name);
        }

        if (Input.GetMouseButton(0) && curLen != null)
        {
            float angle;
            Vector3 objectCenter;
            Vector2 vectorToPrevMouse;
            Vector2 vectorToCurrentMouse;

            objectCenter = curLen.transform.position;

            vectorToPrevMouse = (Vector2)prevMousePos - (Vector2)objectCenter;
            vectorToCurrentMouse = (Vector2)mousePos - (Vector2)objectCenter;

            angle = Vector2.SignedAngle(vectorToPrevMouse, vectorToCurrentMouse);

            curLen.transform.Rotate(0, 0, angle);

            UpdateLensBlur(curLen);

            prevMousePos = mousePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            curLen = null;
            CheckWinCondition();
        }
    }

    private void CheckWinCondition()
    {
        bool lenOutSideCorrect = IsLensInWinZone(lenOutSide);
        bool lenInSideCorrect = IsLensInWinZone(lenInSide);

        if (lenOutSideCorrect && lenInSideCorrect)
        {
            isWin = true;
        }
        else
        {
            Debug.Log($"OutSide: {lenOutSideCorrect} (angle: {GetNormalizedAngle(lenOutSide)}°), InSide: {lenInSideCorrect} (angle: {GetNormalizedAngle(lenInSide)}°)");
        }
    }

    private bool IsLensInWinZone(L179_Len lens)
    {
        if (lens == null) return false;

        float normalizedAngle = GetNormalizedAngle(lens);
        return Mathf.Abs(normalizedAngle) <= winAngleTolerance;
    }

    private float GetNormalizedAngle(L179_Len lens)
    {
        if (lens == null) return 0f;

        float currentAngle = lens.transform.eulerAngles.z;

        // Chuẩn hóa góc về khoảng -180 đến +180
        if (currentAngle > 180f)
            currentAngle -= 360f;

        return currentAngle;
    }

    public void OnWin()
    {
        Debug.Log("Game completed successfully!");
        StartCoroutine(HandleAnimationWin());
    }

    IEnumerator HandleAnimationWin()
    {
        Tween pictureMove = picture.DOMoveY(-3.22f, 0.5f).SetEase(Ease.OutBack);
        yield return pictureMove.WaitForCompletion();
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }


    private void InitializeBlur()
    {
        if (lenOutSide != null && lenOutSide.blur != null)
        {
            UpdateLensBlur(lenOutSide);
        }

        if (lenInSide != null && lenInSide.blur != null)
        {
            UpdateLensBlur(lenInSide);
        }
    }

    private void UpdateLensBlur(L179_Len lens)
    {
        if (lens != null && lens.blur != null)
        {
            float currentZRotation = lens.transform.eulerAngles.z;
            lens.blur.UpdateBlur(currentZRotation);
        }
    }
}
