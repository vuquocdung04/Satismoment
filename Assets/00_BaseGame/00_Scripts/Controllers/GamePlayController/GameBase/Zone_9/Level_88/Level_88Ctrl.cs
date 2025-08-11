using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_88Ctrl : BaseDragController<L88_GrassTractor>
{
    public int winProgress = 0;
    public L88_Yard yard;
    [Header("Cài đặt làm mịn")]
    [Tooltip("Tốc độ đối tượng di chuyển bám theo chuột. Càng lớn càng nhanh.")]
    [SerializeField] private float moveSmoothSpeed = 8f;

    [Tooltip("Tốc độ đối tượng xoay theo chuột. Càng lớn càng nhanh.")]
    [SerializeField] private float rotationSmoothSpeed = 12f;
    protected override void OnDragEnded()
    {
        if(winProgress == 28)
        {
            StartCoroutine(HandleWinCondition());
        }
    }
    Vector3 pivotToMouse;
    float angleZ;
    Quaternion targetRotation;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        pivotToMouse = currentMousePosition - draggableComponent.transform.position;
        angleZ = Mathf.Atan2(pivotToMouse.y, pivotToMouse.x) * Mathf.Rad2Deg;
        targetRotation = Quaternion.Euler(0, 0, angleZ -90 );

        draggableComponent.transform.rotation = Quaternion.Slerp(
            draggableComponent.transform.rotation,
            targetRotation,
            Time.deltaTime * rotationSmoothSpeed
        );
        draggableComponent.transform.position = Vector3.Lerp(
                draggableComponent.transform.position,
                currentMousePosition, // <- ĐÂY LÀ THAY ĐỔI QUAN TRỌNG
                Time.deltaTime * moveSmoothSpeed
            );
    }

    protected override void OnDragStarted()
    {

    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        StartCoroutine(yard.AnimateWinCondition());
        yield return new WaitUntil(() => yard.isAnimateCompleted);
        yield return new WaitForSeconds(0.2f);
        WinBox.SetUp().Show();
    }
}
