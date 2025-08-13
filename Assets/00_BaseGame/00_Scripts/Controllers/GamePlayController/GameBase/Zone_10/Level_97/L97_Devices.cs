using DG.Tweening;
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
    public bool ninetyPercentReached = false;


    private Color[] maskPixelsBuffer; // Buffer để thao tác pixel
    private int drawnPixelCount = 0; // Đếm số pixel đã vẽ một cách tăng dần
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

        maskPixelsBuffer = maskTexture.GetPixels();
        drawnPixelCount = 0; // Đặt lại số pixel đã vẽ
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

        int radiusSq = radius * radius; // Tối ưu: So sánh bình phương khoảng cách để tránh sqrt

        for (int x = startX; x < endX; x++)
        {
            for (int y = startY; y < endY; y++)
            {
                int dx = x - center.x;
                int dy = y - center.y;
                if (dx * dx + dy * dy <= radiusSq) // So sánh bình phương khoảng cách
                {
                    int index = y * textureWidth + x;
                    // Chỉ tăng drawnPixelCount nếu pixel đó trước đây trong suốt và bây giờ được vẽ
                    if (maskPixelsBuffer[index].a <= 0.01f && color.a > 0.01f) // Dùng epsilon cho so sánh float
                    {
                        drawnPixelCount++;
                    }
                    maskPixelsBuffer[index] = color; // Thay đổi trong buffer
                }
            }
        }
    }

    // Phương thức mới để áp dụng thay đổi pixel và kiểm tra độ phủ
    public void ApplyMaskChangesAndCheckCoverage()
    {
        maskTexture.SetPixels(maskPixelsBuffer); // Áp dụng toàn bộ buffer vào texture
        maskTexture.Apply(); // Chỉ Apply() một lần sau khi vẽ

        if (!ninetyPercentReached) // Chỉ kiểm tra nếu chưa đạt 90%
        {
            CheckDrawingCoverage();
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

    float coverage;
    void CheckDrawingCoverage()
    {
        // Bây giờ tính toán dựa trên drawnPixelCount đã được cập nhật tăng dần
        coverage = (float)drawnPixelCount / (textureWidth * textureHeight);

        if (coverage > 0.90f && !ninetyPercentReached) // Thêm !ninetyPercentReached để chỉ kích hoạt một lần
        {
            ninetyPercentReached = true;
            Debug.Log("Đã đạt 95% độ phủ!"); // Hoặc gọi sự kiện thắng cuộc
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

    protected override void ReturnToOriginalPosition()
    {
        transform.DOMove(posDefault,0.2f).SetEase(Ease.Linear);
    }
}
