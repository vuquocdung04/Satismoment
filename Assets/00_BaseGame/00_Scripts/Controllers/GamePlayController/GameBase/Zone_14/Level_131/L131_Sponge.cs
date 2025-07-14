using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L131_Sponge : MonoBehaviour
{
    public List<Transform> lsSpriteStains; // Danh sách các Transform của vết bẩn
    [Header("TargetSprite de lay width va heigh")]
    public List<SpriteRenderer> lsTargetSprites;
    // textureWidth và textureHeight sẽ được xác định nội bộ theo targetSprite hiện tại
    // public int textureWidth; // Không cần thiết public nữa
    // public int textureHeight; // Không cần thiết public nữa

    [Space(5)]
    public List<SpriteMask> lsSpriteMasks;
    public int drawRadius = 10;
    public Color drawColor = Color.white;

    // Khởi tạo biến tạm trung gian 
    private Texture2D maskTexture;
    private Sprite maskSprite;
    public bool ninetyPercentReached = false;


    private Color[] maskPixelsBuffer; // Buffer để thao tác pixel
    private int drawnPixelCount = 0; // Đếm số pixel đã vẽ một cách tăng dần

    // Biến mới để theo dõi mask hiện tại trong danh sách
    private int currentMaskIndex = 0;

    // Kích thước texture hiện tại, được cập nhật theo targetSprite
    private int currentTextureWidth;
    private int currentTextureHeight;


    private void Start()
    {
        // Đảm bảo tất cả các vết bẩn đều được tắt ban đầu
        // và chỉ bật vết bẩn đầu tiên (nếu có)
        for (int i = 0; i < lsSpriteStains.Count; i++)
        {
            if (lsSpriteStains[i] != null)
            {
                lsSpriteStains[i].gameObject.SetActive(false);
            }
        }

        // Bật vết bẩn đầu tiên nếu danh sách không rỗng
        if (lsSpriteStains.Count > 0 && lsSpriteStains[0] != null)
        {
            lsSpriteStains[0].gameObject.SetActive(true);
        }

        InitMask();
    }

    void InitMask()
    {
        // Đảm bảo chỉ số nằm trong phạm vi của cả lsTargetSprites và lsSpriteMasks
        if (currentMaskIndex >= lsTargetSprites.Count || currentMaskIndex >= lsSpriteMasks.Count)
        {
            Debug.LogWarning("Đã hoàn thành tất cả các SpriteMasks hoặc hết TargetSprites để tham chiếu!");
            // Đặt logic kết thúc trò chơi hoặc dừng ở đây
            return;
        }

        Sprite targetSpriteRef = lsTargetSprites[currentMaskIndex].sprite;
        Texture2D originalTex = targetSpriteRef.texture;

        currentTextureWidth = originalTex.width;
        currentTextureHeight = originalTex.height;

        // Tạo lại maskTexture nếu kích thước thay đổi hoặc nếu nó chưa được tạo
        if (maskTexture == null || maskTexture.width != currentTextureWidth || maskTexture.height != currentTextureHeight)
        {
            // Nếu đã có maskTexture cũ, phá hủy nó trước khi tạo mới để tránh rò rỉ bộ nhớ
            if (maskTexture != null)
            {
                Destroy(maskTexture);
            }
            maskTexture = new Texture2D(currentTextureWidth, currentTextureHeight, TextureFormat.Alpha8, false);
        }

        ClearTexture(new Color(0, 0, 0, 0)); // Khởi tạo toàn bộ là trong suốt

        // Cập nhật hoặc tạo maskSprite
        if (maskSprite == null || maskSprite.texture != maskTexture) // Cần tạo lại sprite nếu texture thay đổi
        {
            // Nếu đã có maskSprite cũ, phá hủy nó trước khi tạo mới
            if (maskSprite != null)
            {
                Destroy(maskSprite);
            }
            maskSprite = Sprite.Create(maskTexture,
                new Rect(0, 0, currentTextureWidth, currentTextureHeight),
                new Vector2(0.5f, 0.5f),
                targetSpriteRef.pixelsPerUnit); // Lấy pixelsPerUnit từ targetSprite
        }
        else
        {
            // Nếu maskSprite đã tồn tại và dùng cùng texture, chỉ cập nhật texture của nó
            maskSprite.texture.SetPixels(maskTexture.GetPixels());
        }


        // Gán maskSprite cho SpriteMask hiện tại được chỉ định bởi currentMaskIndex
        for (int i = 0; i < lsSpriteMasks.Count; i++)
        {
            if (lsSpriteMasks[i] != null)
            {
                lsSpriteMasks[i].sprite = null; // Xóa sprite cũ
                lsSpriteMasks[i].enabled = false; // Tắt mask cũ
            }
        }

        // Gán maskSprite cho mask hiện tại và bật nó
        lsSpriteMasks[currentMaskIndex].sprite = maskSprite;
        lsSpriteMasks[currentMaskIndex].enabled = true;
        Debug.Log($"Đang vẽ lên SpriteMask số: {currentMaskIndex} (dựa trên {lsTargetSprites[currentMaskIndex].name})");

        // Cập nhật maskPixelsBuffer với pixel của maskTexture mới
        maskPixelsBuffer = maskTexture.GetPixels();
        drawnPixelCount = 0; // Đặt lại số pixel đã vẽ
        ninetyPercentReached = false; // Đặt lại cờ 90% khi khởi tạo mask mới

        ApplyMaskChanges(); // Áp dụng thay đổi ngay để mask mới hiển thị đúng

        void ClearTexture(Color color)
        {
            Color[] clearColors = new Color[currentTextureWidth * currentTextureHeight];
            for (int i = 0; i < clearColors.Length; i++)
            {
                clearColors[i] = color;
            }
            maskTexture.SetPixels(clearColors);
        }
    }
    int startX;
    int endX;
    int startY;
    int endY;
    void DrawCircle(Vector2Int center, int radius, Color color)
    {
        // Sử dụng currentTextureWidth và currentTextureHeight
        startX = Mathf.Max(0, center.x - radius);
        endX = Mathf.Min(currentTextureWidth, center.x + radius);
        startY = Mathf.Max(0, center.y - radius);
        endY = Mathf.Min(currentTextureHeight, center.y + radius);

        int radiusSq = radius * radius;

        for (int x = startX; x < endX; x++)
        {
            for (int y = startY; y < endY; y++)
            {
                int dx = x - center.x;
                int dy = y - center.y;
                if (dx * dx + dy * dy <= radiusSq)
                {
                    int index = y * currentTextureWidth + x; // Sử dụng currentTextureWidth
                    if (maskPixelsBuffer[index].a <= 0.01f && color.a > 0.01f)
                    {
                        drawnPixelCount++;
                    }
                    maskPixelsBuffer[index] = color;
                }
            }
        }
    }

    public void ApplyMaskChanges()
    {
        maskTexture.SetPixels(maskPixelsBuffer);
        maskTexture.Apply();
    }

    public bool CheckDrawingCoverage()
    {
        if (ninetyPercentReached) return false;

        float totalPixels = currentTextureWidth * currentTextureHeight; // Sử dụng kích thước hiện tại
        float coverage = (float)drawnPixelCount / totalPixels;

        if (coverage > 0.90f && !ninetyPercentReached)
        {
            ninetyPercentReached = true;
            Debug.Log($"Đã đạt 90% độ phủ cho SpriteMask số: {currentMaskIndex}!");
            AdvanceMask();
            return true;
        }
        return false;
    }

    void AdvanceMask()
    {
        // Nếu có vết bẩn tương ứng với mask vừa hoàn thành, tắt nó đi
        if (currentMaskIndex < lsSpriteStains.Count && lsSpriteStains[currentMaskIndex] != null)
        {
            lsSpriteStains[currentMaskIndex].gameObject.SetActive(false);
            Debug.Log($"Đã tắt vết bẩn số: {currentMaskIndex}");
        }

        // Tắt mask hiện tại
        if (lsSpriteMasks.Count > 0 && currentMaskIndex < lsSpriteMasks.Count && lsSpriteMasks[currentMaskIndex] != null)
        {
            lsSpriteMasks[currentMaskIndex].enabled = false;
            lsSpriteMasks[currentMaskIndex].sprite = null;
        }

        currentMaskIndex++;

        // Kiểm tra xem còn mask và target sprite nào nữa không
        if (currentMaskIndex < lsSpriteMasks.Count && currentMaskIndex < lsTargetSprites.Count)
        {
            Debug.Log($"Chuyển sang SpriteMask số: {currentMaskIndex}");

            // Bật vết bẩn tiếp theo (nếu có)
            if (currentMaskIndex < lsSpriteStains.Count && lsSpriteStains[currentMaskIndex] != null)
            {
                lsSpriteStains[currentMaskIndex].gameObject.SetActive(true);
                Debug.Log($"Đã bật vết bẩn số: {currentMaskIndex}");
            }

            InitMask(); // Khởi tạo lại mask cho SpriteMask tiếp theo
        }
        else
        {
            Debug.Log("Đã hoàn thành tất cả các SpriteMasks và vết bẩn!");
            // Bạn có thể thêm logic kết thúc trò chơi ở đây
        }
    }

    Vector3 localPos;
    float texY_normalized;
    float texX_normalized;
    int texX;
    int texY;
    public void DrawAtPosition(Vector3 worldPos)
    {
        // Thêm điều kiện kiểm tra currentMaskIndex để tránh lỗi khi hết mask
        if (ninetyPercentReached || currentMaskIndex >= lsSpriteMasks.Count || currentMaskIndex >= lsTargetSprites.Count) return;

        // Quan trọng: Sử dụng transform của SpriteMask hiện tại và pixelsPerUnit của TargetSprite hiện tại
        Sprite currentTargetSprite = lsTargetSprites[currentMaskIndex].sprite;

        localPos = lsSpriteMasks[currentMaskIndex].transform.InverseTransformPoint(worldPos);

        // Tính toán tọa độ texture dựa trên kích thước của TargetSprite hiện tại và pixelsPerUnit
        texX_normalized = (localPos.x / (currentTargetSprite.rect.width / currentTargetSprite.pixelsPerUnit)) + 0.5f;
        texY_normalized = (localPos.y / (currentTargetSprite.rect.height / currentTargetSprite.pixelsPerUnit)) + 0.5f;

        texX = (int)(texX_normalized * currentTextureWidth); // Sử dụng currentTextureWidth
        texY = (int)(texY_normalized * currentTextureHeight); // Sử dụng currentTextureHeight

        if (texX >= 0 && texX < currentTextureWidth && texY >= 0 && texY < currentTextureHeight)
        {
            DrawCircle(new Vector2Int(texX, texY), drawRadius, drawColor);
        }
    }
}