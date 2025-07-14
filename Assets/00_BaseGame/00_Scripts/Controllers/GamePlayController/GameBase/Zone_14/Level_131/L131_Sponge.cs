using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpongeStage
{
    public Transform stainTransform; // Vết bẩn cần được tắt khi hoàn thành stage này
    public SpriteRenderer targetSpriteRenderer; // Target Sprite để lấy width/height và pixelsPerUnit
    public SpriteMask spriteMask; // SpriteMask sẽ được vẽ lên

    // Các biến nội bộ cho mỗi Stage
    [NonSerialized] public Texture2D maskTexture;
    [NonSerialized] public Sprite maskSprite;
    [NonSerialized] public Color[] maskPixelsBuffer;
    [NonSerialized] public int drawnPixelCount;
    [NonSerialized] public int textureWidth;
    [NonSerialized] public int textureHeight;
    [NonSerialized] public bool isNinetyPercentReached; // Cờ riêng cho từng Stage
    [NonSerialized] public float pixelsPerUnit; // Lưu pixelsPerUnit cho stage này
    [NonSerialized] public Rect spriteRect; // Lưu rect của sprite cho stage này

    // Có thể thêm các thuộc tính khác nếu mỗi stage cần cấu hình riêng (ví dụ: drawRadius, drawColor)
    // public int stageDrawRadius = 10;
    // public Color stageDrawColor = Color.white;
}



public class L131_Sponge : MonoBehaviour
{
    public List<SpongeStage> stages; // Danh sách các giai đoạn lau chùi
    public int drawRadius = 10;
    public Color drawColor = Color.white;

    private int currentStageIndex = 0; // Chỉ số của giai đoạn hiện tại

    private void Start()
    {
        // Tắt tất cả vết bẩn và mask ban đầu
        foreach (var stage in stages)
        {
            if (stage.stainTransform != null)
                stage.stainTransform.gameObject.SetActive(false);
            if (stage.spriteMask != null)
            {
                stage.spriteMask.enabled = false;
                stage.spriteMask.sprite = null;
            }
        }

        // Khởi tạo giai đoạn đầu tiên
        InitCurrentStage();
    }

    void InitCurrentStage()
    {
        if (currentStageIndex >= stages.Count)
        {
            Debug.LogWarning("Đã hoàn thành tất cả các giai đoạn!");
            // Thêm logic kết thúc trò chơi ở đây
            return;
        }

        SpongeStage currentStage = stages[currentStageIndex];

        // Bật vết bẩn cho stage hiện tại
        if (currentStage.stainTransform != null)
        {
            currentStage.stainTransform.gameObject.SetActive(true);
            Debug.Log($"Đã bật vết bẩn cho Stage: {currentStageIndex}");
        }

        // Lấy thông tin từ TargetSprite
        Sprite targetSpriteRef = currentStage.targetSpriteRenderer.sprite;
        Texture2D originalTex = targetSpriteRef.texture;

        currentStage.textureWidth = originalTex.width;
        currentStage.textureHeight = originalTex.height;
        currentStage.pixelsPerUnit = targetSpriteRef.pixelsPerUnit;
        currentStage.spriteRect = targetSpriteRef.rect;

        // Tạo hoặc cập nhật maskTexture
        if (currentStage.maskTexture == null || currentStage.maskTexture.width != currentStage.textureWidth || currentStage.maskTexture.height != currentStage.textureHeight)
        {
            if (currentStage.maskTexture != null)
            {
                Destroy(currentStage.maskTexture);
            }
            currentStage.maskTexture = new Texture2D(currentStage.textureWidth, currentStage.textureHeight, TextureFormat.Alpha8, false);
        }
        ClearTexture(currentStage.maskTexture, new Color(0, 0, 0, 0), currentStage.textureWidth, currentStage.textureHeight); // Khởi tạo trong suốt

        // Cập nhật hoặc tạo maskSprite
        if (currentStage.maskSprite == null || currentStage.maskSprite.texture != currentStage.maskTexture)
        {
            if (currentStage.maskSprite != null)
            {
                Destroy(currentStage.maskSprite);
            }
            currentStage.maskSprite = Sprite.Create(currentStage.maskTexture,
                new Rect(0, 0, currentStage.textureWidth, currentStage.textureHeight),
                new Vector2(0.5f, 0.5f),
                currentStage.pixelsPerUnit);
        }
        else
        {
            currentStage.maskSprite.texture.SetPixels(currentStage.maskTexture.GetPixels());
        }

        // Gán maskSprite cho SpriteMask hiện tại và bật nó
        currentStage.spriteMask.sprite = currentStage.maskSprite;
        currentStage.spriteMask.enabled = true;
        Debug.Log($"Đang vẽ lên SpriteMask số: {currentStageIndex} (dựa trên {currentStage.targetSpriteRenderer.name})");

        // Cập nhật buffer và reset trạng thái vẽ
        currentStage.maskPixelsBuffer = currentStage.maskTexture.GetPixels();
        currentStage.drawnPixelCount = 0;
        currentStage.isNinetyPercentReached = false;

        ApplyMaskChanges(); // Áp dụng thay đổi ngay để mask mới hiển thị đúng
    }

    void ClearTexture(Texture2D texture, Color color, int width, int height)
    {
        Color[] clearColors = new Color[width * height];
        for (int i = 0; i < clearColors.Length; i++)
        {
            clearColors[i] = color;
        }
        texture.SetPixels(clearColors);
    }


    int startX;
    int endX;
    int startY;
    int endY;
    int radiusSq;
    int dx;
    int dy;
    int index;
    void DrawCircle(SpongeStage stage, Vector2Int center, int radius, Color color)
    {
        startX = Mathf.Max(0, center.x - radius);
        endX = Mathf.Min(stage.textureWidth, center.x + radius);
        startY = Mathf.Max(0, center.y - radius);
        endY = Mathf.Min(stage.textureHeight, center.y + radius);

        radiusSq = radius * radius;

        for (int x = startX; x < endX; x++)
        {
            for (int y = startY; y < endY; y++)
            {
                dx = x - center.x;
                dy = y - center.y;
                if (dx * dx + dy * dy <= radiusSq)
                {
                    index = y * stage.textureWidth + x;
                    if (stage.maskPixelsBuffer[index].a <= 0.01f && color.a > 0.01f)
                    {
                        stage.drawnPixelCount++;
                    }
                    stage.maskPixelsBuffer[index] = color;
                }
            }
        }
    }

    public void ApplyMaskChanges()
    {
        if (currentStageIndex >= stages.Count) return;

        SpongeStage currentStage = stages[currentStageIndex];
        if (currentStage.maskTexture != null)
        {
            currentStage.maskTexture.SetPixels(currentStage.maskPixelsBuffer);
            currentStage.maskTexture.Apply();
        }
    }

    public bool CheckDrawingCoverage()
    {
        if (currentStageIndex >= stages.Count) return false;

        SpongeStage currentStage = stages[currentStageIndex];
        if (currentStage.isNinetyPercentReached) return false;

        float totalPixels = currentStage.textureWidth * currentStage.textureHeight;
        float coverage = (float)currentStage.drawnPixelCount / totalPixels;

        if (coverage > 0.90f && !currentStage.isNinetyPercentReached)
        {
            currentStage.isNinetyPercentReached = true;
            Debug.Log($"Đã đạt 90% độ phủ cho Stage: {currentStageIndex}!");
            AdvanceMask();
            return true;
        }
        return false;
    }

    void AdvanceMask()
    {
        if (currentStageIndex >= stages.Count) return;

        SpongeStage completedStage = stages[currentStageIndex];

        // Tắt vết bẩn của stage vừa hoàn thành
        if (completedStage.stainTransform != null)
        {
            completedStage.stainTransform.gameObject.SetActive(false);
            Debug.Log($"Đã tắt vết bẩn cho Stage: {currentStageIndex}");
        }

        // Tắt mask của stage vừa hoàn thành
        if (completedStage.spriteMask != null)
        {
            completedStage.spriteMask.enabled = false;
            completedStage.spriteMask.sprite = null; // Xóa sprite khỏi mask để giải phóng bộ nhớ (optional)
        }

        currentStageIndex++;
        InitCurrentStage(); // Khởi tạo stage tiếp theo
    }

    SpongeStage currentStage;
    Vector3 localPos;
    float texX_normalized;
    float texY_normalized;
    int texX;
    int texY;
    public void DrawAtPosition(Vector3 worldPos)
    {
        if (currentStageIndex >= stages.Count) return;

        currentStage = stages[currentStageIndex];
        if (currentStage.isNinetyPercentReached) return;

        localPos = currentStage.spriteMask.transform.InverseTransformPoint(worldPos);

        // Tính toán tọa độ texture dựa trên kích thước của TargetSprite hiện tại và pixelsPerUnit
        // Sử dụng spriteRect để tính toán chính xác hơn nếu sprite không nằm trọn trong texture
        texX_normalized = (localPos.x / (currentStage.spriteRect.width / currentStage.pixelsPerUnit)) + 0.5f;
        texY_normalized = (localPos.y / (currentStage.spriteRect.height / currentStage.pixelsPerUnit)) + 0.5f;

        texX = (int)(texX_normalized * currentStage.textureWidth);
        texY = (int)(texY_normalized * currentStage.textureHeight);

        if (texX >= 0 && texX < currentStage.textureWidth && texY >= 0 && texY < currentStage.textureHeight)
        {
            DrawCircle(currentStage, new Vector2Int(texX, texY), drawRadius, drawColor);
        }
    }
}