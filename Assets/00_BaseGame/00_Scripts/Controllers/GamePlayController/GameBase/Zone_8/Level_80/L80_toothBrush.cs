using UnityEngine;
using System.Collections;
public class L80_toothBrush : MonoBehaviour
{
    public Level_80Ctrl levelCtrl;
    public SpriteRenderer targetSprite; // Vùng răng cần vẽ
    public SpriteMask spriteMask;       // SpriteMask liên quan đến vùng răng

    private Texture2D maskTexture;
    private Sprite maskSprite;

    public int textureWidth = 256;
    public int textureHeight = 256;
    public int drawRadius = 10;
    public Color drawColor = Color.white;

    private bool ninetyPercentReached = false;

    void Start()
    {
        if (targetSprite == null || spriteMask == null)
        {
            enabled = false;
            return;
        }

        Sprite sprite = targetSprite.sprite;
        Texture2D originalTex = sprite.texture;

        textureWidth = originalTex.width;
        textureHeight = originalTex.height;

        // Sử dụng TextureFormat.Alpha8 để tối ưu cho SpriteMask
        maskTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.Alpha8, false);
        ClearTexture(new Color(0, 0, 0, 0)); // Khởi tạo toàn bộ là trong suốt
        maskTexture.Apply();

        maskSprite = Sprite.Create(maskTexture,
                                   new Rect(0, 0, textureWidth, textureHeight),
                                   new Vector2(0.5f, 0.5f),
                                   300f);

        spriteMask.sprite = maskSprite;
    }

    void ClearTexture(Color color)
    {
        Color[] clearColors = new Color[textureWidth * textureHeight];
        for (int i = 0; i < clearColors.Length; i++)
        {
            clearColors[i] = color;
        }
        maskTexture.SetPixels(clearColors);
        maskTexture.Apply();
    }

    public void DrawAtPosition(Vector3 worldPos)
    {
        Vector3 localPos = spriteMask.transform.InverseTransformPoint(worldPos);
        float pixelsPerUnit = maskSprite.pixelsPerUnit;

        float texX_normalized = (localPos.x / (maskTexture.width / pixelsPerUnit)) + 0.5f;
        float texY_normalized = (localPos.y / (maskTexture.height / pixelsPerUnit)) + 0.5f;

        int texX = (int)(texX_normalized * textureWidth);
        int texY = (int)(texY_normalized * textureHeight);

        if (texX >= 0 && texX < textureWidth && texY >= 0 && texY < textureHeight)
        {
            DrawCircle(new Vector2Int(texX, texY), drawRadius, drawColor);
            maskTexture.Apply();
            UpdateSprite();

            if (!ninetyPercentReached)
            {
                CheckDrawingCoverage();
            }
        }
    }

    void DrawCircle(Vector2Int center, int radius, Color color)
    {
        int startX = Mathf.Max(0, center.x - radius);
        int endX = Mathf.Min(textureWidth, center.x + radius);
        int startY = Mathf.Max(0, center.y - radius);
        int endY = Mathf.Min(textureHeight, center.y + radius);

        for (int x = startX; x < endX; x++)
        {
            for (int y = startY; y < endY; y++)
            {
                if (Vector2.Distance(new Vector2(x, y), center) <= radius)
                {
                    maskTexture.SetPixel(x, y, color);
                }
            }
        }
    }

    void UpdateSprite()
    {
        Sprite brushSprite = Sprite.Create(maskTexture,
                                           new Rect(0, 0, textureWidth, textureHeight),
                                           new Vector2(0.5f, 0.5f),
                                           300f);
        spriteMask.sprite = brushSprite;
    }

    void CheckDrawingCoverage()
    {
        Color32[] pixels = maskTexture.GetPixels32();
        int drawnCount = 0;

        foreach (var pixel in pixels)
        {
            if (pixel.a > 0) drawnCount++;
        }

        float coverage = (float)drawnCount / pixels.Length;

        if (coverage > 0.9f)
        {
            ninetyPercentReached = true;
            levelCtrl.isComplete = true;
        }
    }
    private void OnDestroy()
    {
        if (maskTexture != null) Destroy(maskTexture);
        if (maskSprite != null) Destroy(maskSprite);
    }
}