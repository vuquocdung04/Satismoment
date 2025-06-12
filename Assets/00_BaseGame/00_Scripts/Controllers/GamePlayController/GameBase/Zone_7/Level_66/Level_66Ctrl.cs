using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_66Ctrl : BaseDragController<L66_CleanerObj>
{

    public int winProgress;

    [Header("Cài đặt làm mịn")]
    [Tooltip("Tốc độ đối tượng di chuyển bám theo chuột. Càng lớn càng nhanh.")]
    [SerializeField] private float moveSmoothSpeed = 8f;

    [Tooltip("Tốc độ đối tượng xoay theo chuột. Càng lớn càng nhanh.")]
    [SerializeField] private float rotationSmoothSpeed = 12f;

    protected override void OnDragStarted()
    {

    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        Vector3 pivotToMouse = currentMousePosition - draggableComponent.transform.position;
        float angleZ = Mathf.Atan2(pivotToMouse.y, pivotToMouse.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angleZ + 180f);

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

    protected override void OnDragEnded()
    {

    }

}