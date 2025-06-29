using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum L97_DeviceType
{
    SteamIron,
    SprayBottle
}

public class L97_Devices : BaseDraggableObject
{
    public L97_DeviceAnim deviceAnim;
    public L97_DeviceType deviceType;
    [Header("TargetSprite de lay width va heigh")]
    public SpriteRenderer targetSprite; // lay with heigh
    public int textureWidth;
    public int textureHeight;
    [Space(5)]
    public SpriteMask spriteMask; // mask de apply
    public int drawRadius = 10;
    public Color drawColor = Color.white;

    // Khoi tao bien tam trung gian 
    private Texture2D maskTexture;
    private Sprite maskSprite;
    private bool ninetyPercentReached = false;

    #region mask
    private void Start()
    {
        if(deviceType == L97_DeviceType.SteamIron)
        InitMask();
    }

    void InitMask()
    {
        Sprite sprite = targetSprite.sprite;
        Texture2D originalTex = sprite.texture;

        textureWidth = originalTex.width;
        textureHeight = originalTex.height;

        maskTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.Alpha8,false);
        ClearTexture(new Color(0, 0, 0, 0)); // Khởi tạo toàn bộ là trong suốt

        maskTexture.Apply();

        maskSprite = Sprite.Create(maskTexture,
            new Rect(0, 0, textureWidth, textureHeight),
            new Vector2(0.5f, 0.5f),
            200f);

        // gan nguoc lai
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
    int startX;
    int endX;
    int startY;
    int endY;
    void DrawCircle(Vector2Int center, int radius, Color color)
    {
         startX = Mathf.Max(0, center.x - radius);
         endX = Mathf.Min(textureWidth, center.x + radius);
         startY = Mathf.Max(0, center.y - radius);
         endY = Mathf.Min(textureHeight, center.y + radius);

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
    Sprite brushSprite;
    void UpdateSprite()
    {
        brushSprite = Sprite.Create(maskTexture,
                                           new Rect(0, 0, textureWidth, textureHeight),
                                           new Vector2(0.5f, 0.5f),
                                           200f);
        spriteMask.sprite = brushSprite;
    }
    int drawnCount;
    Color32[] pixels;
    float coverage;
    void CheckDrawingCoverage()
    {
        pixels = maskTexture.GetPixels32();
        drawnCount = 0;

        foreach (var pixel in pixels)
        {
            if (pixel.a > 0) drawnCount++;
        }

        coverage = (float)drawnCount / pixels.Length;

        if (coverage > 0.9f)
        {
            ninetyPercentReached = true;
        }
    }
    #endregion

    Vector3 localPos;
    float texY_normalized;
    float texX_normalized;
    int texX;
    int texY;
    public void DrawAtPosition(Vector3 worldPos)
    {
        localPos = spriteMask.transform.InverseTransformPoint(worldPos);
         texX_normalized = (localPos.x / (maskTexture.width / maskSprite.pixelsPerUnit)) + 0.5f;
         texY_normalized = (localPos.y / (maskTexture.height / maskSprite.pixelsPerUnit)) + 0.5f;

         texX = (int)(texX_normalized * textureWidth);
         texY = (int)(texY_normalized * textureHeight);

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


    public override void OnStartDrag()
    {
        base.OnStartDrag();
        deviceAnim.StartAnimation();
    }

    public override void OnEndDrag()
    {
        base.OnEndDrag();
        deviceAnim.StopAnimation();
    }

    public override void ReturnToOriginalPosition()
    {
        
    }
}
