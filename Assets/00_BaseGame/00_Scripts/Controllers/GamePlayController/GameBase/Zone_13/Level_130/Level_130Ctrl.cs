using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_130Ctrl : BaseDragController<L130_MixerTap>
{
    public L130_Boy boy;
    public L130_ShowerHead showerHead;

    // Biến trạng thái hiện tại
    private bool isCurrentHot = false;
    private bool isCurrentCold = false;

    protected override void OnDragEnded()
    {
        // Tùy chọn: có thể reset trạng thái, hoặc giữ nguyên
    }

    float rotationAmount;
    float currentZ;
    float clampedZ;
    float newZ;
    bool shouldBeHot;
    bool shouldBeCold;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        // Tính lượng xoay dựa trên mouseDelta.x
        rotationAmount = deltaMousePosition.x * 15f;

        // Lấy góc xoay hiện tại theo trục Z
        currentZ = draggableComponent.transform.eulerAngles.z;

        // Chuyển về khoảng [-180, 180] để dễ xử lý
        clampedZ = currentZ > 180 ? currentZ - 360 : currentZ;

        // Kiểm tra nếu xoay thêm không vượt quá giới hạn
        newZ = Mathf.Clamp(clampedZ + rotationAmount, -60f, 60f);

        // Áp dụng xoay với giá trị đã được giới hạn
        draggableComponent.transform.rotation = Quaternion.Euler(0, 0, newZ);

        // --- Logic kiểm tra và chuyển trạng thái ---

        shouldBeCold = (newZ <= 0f && newZ >= -60f);
        shouldBeHot = (newZ >= 20f && newZ <= 60f);

        // Chỉ gọi hàm nếu trạng thái thực sự thay đổi
        if (shouldBeCold && !isCurrentCold)
        {
            showerHead.ActiveColdEffect();
            isCurrentHot = false;
            isCurrentCold = true;
        }
        else if (shouldBeHot && !isCurrentHot)
        {
            showerHead.ActiveHotEffect();
            isCurrentCold = false;
            isCurrentHot = true;
        }
    }

    protected override void OnDragStarted()
    {
        // Có thể bỏ trống hoặc xử lý gì đó khi bắt đầu kéo
    }
}