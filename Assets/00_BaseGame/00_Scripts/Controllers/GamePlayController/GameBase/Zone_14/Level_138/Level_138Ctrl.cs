using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_138Ctrl : BaseDragController<L138_Valve>
{
    [SerializeField] private float rotationAmount = 0f;
    public Transform water;
    public Transform puddle;
    public L138_Effect effect;
    private int lastFrameIndex = 0;
    protected override void OnDragEnded()
    {
        
    }
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        rotationAmount += deltaMousePosition.x; // Tăng/giảm dựa trên kéo chuột
        rotationAmount = Mathf.Max(0, rotationAmount);

        // Gọi hàm cập nhật sprite nếu rotationAmount vượt qua mốc mới
        int currentFrameIndex = Mathf.FloorToInt(rotationAmount);
        if (currentFrameIndex != lastFrameIndex && draggableComponent != null)
        {
            draggableComponent.UpdateSpriteByRotation(rotationAmount);
            lastFrameIndex = currentFrameIndex;

            // Kiểm tra nếu đã đến frame cuối cùng
            if (currentFrameIndex >= draggableComponent.lsFrames.Count - 1)
            {
                Debug.Log("✅ Van đã mở hết!");
                StartCoroutine(HandleWinCondition());
            }
        }

        // Cập nhật chiều cao nước (scale y) theo rotationAmount
        float scaleFactor = Mathf.Max(0, 1f - (rotationAmount * 0.1f));
        Vector3 newScale = water.localScale;
        newScale.x = scaleFactor;
        water.localScale = newScale;
    }

    protected override void OnDragStarted()
    {
        
    }

    IEnumerator HandleWinCondition()
    {
        effect.StopAnimation();
        water.localScale = Vector3.zero;
        isWin = true;
        puddle.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}
