using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L167_Eraser : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public Sprite spriteOnDrag;
    public Sprite spriteOffDrag;
    public List<L143_ItemStage> stages;
    public int drawRadius = 50;
    public Color drawColor = Color.white;

    private int currentStageIndex = 0;
    L143_ItemStage currentStage;
    Vector3 localPos;
    float texX_normalized;
    float texY_normalized;
    int texX;
    int texY;

    private void Start()
    {
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

        InitCurrentStage();
    }

    void InitCurrentStage()
    {
        if (currentStageIndex >= stages.Count)
        {
            Debug.LogWarning("Đã hoàn thành tất cả các giai đoạn!");
            return;
        }

        L143_ItemStage currentStage = stages[currentStageIndex];

        if (currentStage.stainTransform != null)
        {
            currentStage.stainTransform.gameObject.SetActive(true);
            Debug.Log($"Đã bật vết bẩn cho Stage: {currentStageIndex}");
        }

        Sprite targetSpriteRef = currentStage.targetSpriteRenderer.sprite;
        Texture2D originalTex = targetSpriteRef.texture;

        currentStage.textureWidth = originalTex.width;
        currentStage.textureHeight = originalTex.height;
        currentStage.pixelsPerUnit = targetSpriteRef.pixelsPerUnit;
        currentStage.spriteRect = targetSpriteRef.rect;

        Debug.LogError(currentStage.pixelsPerUnit);

        if (currentStage.maskTexture == null || currentStage.maskTexture.width != currentStage.textureWidth || currentStage.maskTexture.height != currentStage.textureHeight)
        {
            if (currentStage.maskTexture != null)
            {
                Destroy(currentStage.maskTexture);
            }
            currentStage.maskTexture = new Texture2D(currentStage.textureWidth, currentStage.textureHeight, TextureFormat.Alpha8, false);
        }
        ClearTexture(currentStage.maskTexture, new Color(0, 0, 0, 0), currentStage.textureWidth, currentStage.textureHeight);

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

        currentStage.spriteMask.sprite = currentStage.maskSprite;
        currentStage.spriteMask.enabled = true;
        Debug.Log($"Đang vẽ lên SpriteMask số: {currentStageIndex} (dựa trên {currentStage.targetSpriteRenderer.name})");

        currentStage.maskPixelsBuffer = currentStage.maskTexture.GetPixels();
        currentStage.drawnPixelCount = 0;
        currentStage.isNinetyPercentReached = false;

        ApplyMaskChanges();
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

    int startX, endX, startY, endY, radiusSq, dx, dy, index;

    // Trả về true nếu đã vẽ lên vùng mask chưa vẽ trước đó
    bool DrawCircle(L143_ItemStage stage, Vector2Int center, int radius, Color color)
    {
        bool changed = false;
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
                        changed = true;
                    }
                    stage.maskPixelsBuffer[index] = color;
                }
            }
        }
        return changed;
    }

    // Trả về true nếu thao tác này vẽ lên vùng chưa vẽ trên mask
    public bool DrawAtPosition(Vector3 worldPos)
    {
        if (currentStageIndex >= stages.Count) return false;

        currentStage = stages[currentStageIndex];
        if (currentStage.isNinetyPercentReached) return false;

        localPos = currentStage.spriteMask.transform.InverseTransformPoint(worldPos);

        texX_normalized = (localPos.x / (currentStage.spriteRect.width / currentStage.pixelsPerUnit)) + 0.5f;
        texY_normalized = (localPos.y / (currentStage.spriteRect.height / currentStage.pixelsPerUnit)) + 0.5f;

        texX = (int)(texX_normalized * currentStage.textureWidth);
        texY = (int)(texY_normalized * currentStage.textureHeight);

        if (texX >= 0 && texX < currentStage.textureWidth && texY >= 0 && texY < currentStage.textureHeight)
        {
            return DrawCircle(currentStage, new Vector2Int(texX, texY), drawRadius, drawColor);
        }
        return false;
    }

    public void ApplyMaskChanges()
    {
        if (currentStageIndex >= stages.Count) return;

        L143_ItemStage currentStage = stages[currentStageIndex];
        if (currentStage.maskTexture != null)
        {
            currentStage.maskTexture.SetPixels(currentStage.maskPixelsBuffer);
            currentStage.maskTexture.Apply();
        }
    }

    public bool CheckDrawingCoverage()
    {
        if (currentStageIndex >= stages.Count) return false;

        L143_ItemStage currentStage = stages[currentStageIndex];
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

        L143_ItemStage completedStage = stages[currentStageIndex];

        if (completedStage.stainTransform != null)
        {
            completedStage.stainTransform.gameObject.SetActive(false);
            Debug.Log($"Đã tắt vết bẩn cho Stage: {currentStageIndex}");
        }

        if (completedStage.spriteMask != null)
        {
            completedStage.spriteMask.enabled = false;
            completedStage.spriteMask.sprite = null;
        }

        currentStageIndex++;
        InitCurrentStage();
    }
}
