using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_61Ctrl : BaseDragController<L61_Petal>
{
    public int winProgress;
    public List<Transform> lsTrans;
    [Header("Tùy chỉnh thông số")]
    [Tooltip("Độ nhạy khi scale cánh hoa khi kéo")]
    [SerializeField] private float scaleSensitivity = 1.5f;
    [Tooltip("Ngưỡng scale để xác định cánh hoa bị bứt")]
    [SerializeField] private float pluckThreshold = 2.0f;
    [Tooltip("Thời gian animation khi reset cánh hoa")]
    [SerializeField] private float resetAnimationTime = 0.25f;
    [Tooltip("Thời gian animation khi cánh hoa bị bứt")]
    [SerializeField] private float pluckAnimationTime = 1.5f;

    private Vector3 dragStartMousePos;
    private Vector3 initialScale;
    private Quaternion initialRotation;
    private bool isPetalPlucked = false;

    protected override void OnDragStarted()
    {
        dragStartMousePos = mouseWorldPos;
        isPetalPlucked = false;

        // Lưu lại trạng thái ban đầu để có thể reset
        initialScale = draggableComponent.transform.localScale;
        initialRotation = draggableComponent.transform.rotation;
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        if (isPetalPlucked) return;

        // 1. Xử lý xoay cánh hoa
        float deltaY = currentMousePosition.y - dragStartMousePos.y;
        float rotationAngle = deltaY * 30f;
        draggableComponent.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);

        // 2. Xử lý scale cánh hoa
        float absDeltaX = Mathf.Abs(mouseDelta.x);
        float absDeltaY = Mathf.Abs(mouseDelta.y);
        Vector3 growth = new Vector3(absDeltaX, absDeltaY, 0);
        draggableComponent.transform.localScale += growth * scaleSensitivity;

        // 3. Kiểm tra điều kiện bứt cánh hoa
        if (draggableComponent.transform.localScale.x > pluckThreshold || draggableComponent.transform.localScale.y > pluckThreshold)
        {
            HandlePetalPluck();
        }
    }

    protected override void OnDragEnded()
    {
        if (!isPetalPlucked)
        {
            draggableComponent.transform.DOScale(initialScale, resetAnimationTime);
            draggableComponent.transform.DORotateQuaternion(initialRotation, resetAnimationTime);
        }

        StartCoroutine(HandleWinCodition());
    }

    private void HandlePetalPluck()
    {
        isPetalPlucked = true;
        var pluckedPetal = draggableComponent;

        // Tắt collider để không tương tác được nữa
        pluckedPetal._collider.enabled = false;
        pluckedPetal.transform.localScale = Vector3.one;
        pluckedPetal.transform.DOMoveY(pluckedPetal.defaultPos.y - 5f, pluckAnimationTime)
            .SetEase(Ease.InQuad).OnComplete(()=> Destroy(pluckedPetal.gameObject));
        winProgress++;
        
    }

    IEnumerator HandleWinCodition()
    {
        if (winProgress == 20)
        {
            isWin = true;
            lsTrans[0].DOMoveX(1, 0.75f).SetEase(Ease.InOutQuad);
            lsTrans[1].DOMoveX(-1, 0.75f).SetEase(Ease.InOutQuad);
            lsTrans[2].gameObject.SetActive(true);
            yield return new WaitForSeconds(1.6f);
            WinBox.SetUp().Show();
        }
    }
}