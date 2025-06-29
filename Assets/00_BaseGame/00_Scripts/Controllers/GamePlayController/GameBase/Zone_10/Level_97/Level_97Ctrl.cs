using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_97Ctrl : BaseDragController<L97_Devices>
{
    public int winProgress;
    public List<L97_Devices> lsDevices;

    private float lastApplyTime;
    public float applyInterval = 0.05f; // Thời gian tối thiểu giữa các lần áp dụng texture (ví dụ: 50ms)

    protected override void OnDragEnded()
    {
        draggableComponent.OnEndDrag();
        // Đảm bảo các thay đổi cuối cùng được áp dụng khi kết thúc kéo
        draggableComponent.ApplyMaskChangesAndCheckCoverage();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
        switch (draggableComponent.deviceType)
        {
            case L97_DeviceType.SteamIron:
                draggableComponent.DrawAtPosition(currentMousePosition);

                // Áp dụng các thay đổi và kiểm tra độ phủ có giới hạn thời gian (throttling)
                if (Time.time - lastApplyTime > applyInterval)
                {
                    draggableComponent.ApplyMaskChangesAndCheckCoverage();
                    lastApplyTime = Time.time;
                }
                break;
            default:
                break;
        }
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
        lastApplyTime = Time.time; // Đặt lại thời gian khi bắt đầu kéo
    }


    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        foreach (var device in this.lsDevices)
        {
            // Các phương thức InitAfter, InitBefore không có trong code bạn cung cấp,
            // nếu chúng có liên quan đến việc thay đổi mask, hãy đảm bảo chúng xử lý maskPixelsBuffer đúng cách.
            // device.InitAfter();
            // device.InitBefore();
        }
    }
}