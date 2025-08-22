using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L83_GlassCleaner : MonoBehaviour
{
    public Level_83Ctrl levelCtr;
    public L83_ClassCleanerAnim glassAnim;
    public List<L83_GlassSet> lsGlasss;
    private bool allCompleted = false;

    void Start()
    {
        foreach (var glass in lsGlasss)
        {
            if (glass.targetSprite == null || glass.mask == null)
                continue;

            Sprite sprite = glass.targetSprite.sprite;
            Texture2D originalTex = sprite.texture;

            glass.textureWidth = originalTex.width;
            glass.textureHeight = originalTex.height;

            // Khởi tạo mask texture với độ trong suốt
            glass.maskTexture = new Texture2D(glass.textureWidth, glass.textureHeight, TextureFormat.Alpha8, false);
            ClearTexture(glass);
            glass.maskTexture.Apply();

            // Tạo Sprite từ Texture để gán vào SpriteMask
            glass.maskSprite = Sprite.Create(
                glass.maskTexture,
                new Rect(0, 0, glass.textureWidth, glass.textureHeight),
                new Vector2(0.5f, 0.5f),
                168f
            );

            glass.mask.sprite = glass.maskSprite;
        }
    }

    void ClearTexture(L83_GlassSet glass, Color color = default)
    {
        Color[] clearColors = new Color[glass.textureWidth * glass.textureHeight];
        for (int i = 0; i < clearColors.Length; i++)
        {
            clearColors[i] = color;
        }
        glass.maskTexture.SetPixels(clearColors);
        glass.maskTexture.Apply();
    }

    public void DrawAtPosition(Vector3 worldPos)
    {
        foreach (var glass in lsGlasss)
        {
            if (glass.completed) continue;

            // Chuyển tọa độ thế giới sang local của SpriteMask
            Vector3 localPos = glass.mask.transform.InverseTransformPoint(worldPos);
            float pixelsPerUnit = glass.maskSprite.pixelsPerUnit;

            float texX_normalized = (localPos.x / (glass.textureWidth / pixelsPerUnit)) + 0.5f;
            float texY_normalized = (localPos.y / (glass.textureHeight / pixelsPerUnit)) + 0.5f;

            int texX = (int)(texX_normalized * glass.textureWidth);
            int texY = (int)(texY_normalized * glass.textureHeight);

            if (texX >= 0 && texX < glass.textureWidth && texY >= 0 && texY < glass.textureHeight)
            {
                DrawCircle(glass, new Vector2Int(texX, texY));
                glass.maskTexture.Apply();
                UpdateSprite(glass);

                if (!glass.completed)
                {
                    CheckDrawingCoverage(glass);
                }
            }
        }

        if (!allCompleted)
        {
            CheckAllComplete();
        }
    }

    void DrawCircle(L83_GlassSet glass, Vector2Int center)
    {
        int radius = glass.drawRadius;

        int startX = Mathf.Max(0, center.x - radius);
        int endX = Mathf.Min(glass.textureWidth, center.x + radius);
        int startY = Mathf.Max(0, center.y - radius);
        int endY = Mathf.Min(glass.textureHeight, center.y + radius);

        for (int x = startX; x < endX; x++)
        {
            for (int y = startY; y < endY; y++)
            {
                if (Vector2.Distance(new Vector2(x, y), center) <= radius)
                {
                    glass.maskTexture.SetPixel(x, y, glass.drawColor);
                }
            }
        }
    }

    void UpdateSprite(L83_GlassSet glass)
    {
        glass.maskSprite = Sprite.Create(
            glass.maskTexture,
            new Rect(0, 0, glass.textureWidth, glass.textureHeight),
            new Vector2(0.5f, 0.5f),
            168f
        );
        glass.mask.sprite = glass.maskSprite;
    }

    void CheckDrawingCoverage(L83_GlassSet glass)
    {
        Color32[] pixels = glass.maskTexture.GetPixels32();
        int drawnCount = 0;

        foreach (var pixel in pixels)
        {
            if (pixel.a > 0) drawnCount++;
        }

        float coverage = (float)drawnCount / pixels.Length;
        glassAnim.ChangeSpriteFirst();

        if (coverage > 0.95f) // Nếu hơn 90% đã được lau
        {
            glass.completed = true;
            glass.mask.gameObject.SetActive(false);
            glass.maskAfterWin.gameObject.SetActive(true);
        }
    }

    void CheckAllComplete()
    {
        foreach (var glass in lsGlasss)
        {
            if (!glass.completed)
                return;
        }
        Debug.LogError("Complete");

        levelCtr.isCompleteLevel = true;
        allCompleted = true;
    }

    private void OnDestroy()
    {
        foreach (var glass in lsGlasss)
        {
            if (glass.maskTexture != null)
                Destroy(glass.maskTexture);
            if (glass.maskSprite != null)
                Destroy(glass.maskSprite);
        }
    }
}