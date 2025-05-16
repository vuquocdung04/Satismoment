using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Giả sử L12_PaintRoller là class của cục tẩy bạn kéo thả
public class Level_12Ctrl : BaseDragController<L12_PaintRoller>
{
    // --- Biến lưu vị trí cũ của cục tẩy ---
    Vector2 oldPoint = Vector2.zero;
    public bool canPlayerInteract = false;
    // --- CÁC THAM SỐ CẦN KÉO THẢ HOẶC CHỈNH TRONG UNITY EDITOR ---
    [Header("KÉO THẢ TỪ HIERARCHY HOẶC PROJECT")]
    public SpriteMask spriteMask;           // GameObject chứa SpriteMask (vd: EraserMaskHolder)
    public SpriteRenderer paintRenderer;    // GameObject chứa SpriteRenderer của lớp sơn (vd: Paint)

    [Header("THÔNG SỐ CHO MẶT NẠ VÀ CỤC TẨY")]
    public int maskTextureWidth = 512;      // Chiều rộng texture của mặt nạ (pixel)
    public int maskTextureHeight = 512;     // Chiều cao texture của mặt nạ (pixel)
    public int eraserBrushRadiusOnMask = 25;// Bán kính đầu tẩy trên texture mặt nạ (pixel)
    public float maskPixelsPerUnit = 100f;  // Số pixel / 1 đơn vị Unity (nên giống với sprite sơn)

    [Header("ĐIỀU KIỆN THẮNG")]
    [Range(0.1f, 1.0f)] // Thanh trượt trong Inspector từ 0.1 (10%) đến 1.0 (100%)
    public float winPercentageThreshold = 0.9f; // Ngưỡng để thắng (vd: 0.9f = 90%)

    // --- Các biến nội bộ, không cần chỉnh từ Editor ---
    private Texture2D dynamicMaskTexture;   // Texture dùng để tạo sprite cho mặt nạ
    private Sprite dynamicMaskSprite;       // Sprite được tạo từ dynamicMaskTexture
    private Color32[] maskPixelColors;      // Mảng lưu màu của các pixel trên mặt nạ
    private bool needsMaskTextureUpdate = false; // Cờ báo hiệu cần cập nhật texture

    private int totalMaskPixels;            // Tổng số pixel của texture mặt nạ
    private int erasedPixelCount;           // Số pixel đã bị làm mờ đục (đã xóa)
    private bool hasWon = false;            // Cờ báo đã thắng hay chưa

    // --- HÀM KHỞI TẠO CHÍNH ---
    void Start()
    {
        InitializeMask(); // Chuẩn bị mọi thứ cho mặt nạ
    }

    void InitializeMask()
    {
        // 1. Tạo Texture2D cho mặt nạ (ban đầu trong suốt)
        dynamicMaskTexture = new Texture2D(maskTextureWidth, maskTextureHeight, TextureFormat.Alpha8, false);
        dynamicMaskTexture.filterMode = FilterMode.Bilinear;
        dynamicMaskTexture.wrapMode = TextureWrapMode.Clamp;

        maskPixelColors = new Color32[maskTextureWidth * maskTextureHeight];
        Color32 transparentColor = new Color32(0, 0, 0, 0); // Màu trong suốt (alpha = 0)
        for (int i = 0; i < maskPixelColors.Length; i++)
        {
            maskPixelColors[i] = transparentColor;
        }
        dynamicMaskTexture.SetPixels32(maskPixelColors);
        dynamicMaskTexture.Apply(); // Áp dụng thay đổi lên texture

        // 2. Tạo Sprite từ Texture2D trên
        Rect rect = new Rect(0, 0, maskTextureWidth, maskTextureHeight);
        dynamicMaskSprite = Sprite.Create(dynamicMaskTexture, rect, new Vector2(0.5f, 0.5f), maskPixelsPerUnit);

        // 3. Gán Sprite này cho component SpriteMask trong Scene
        spriteMask.sprite = dynamicMaskSprite;

        // 4. Khởi tạo biến cho điều kiện thắng
        totalMaskPixels = maskTextureWidth * maskTextureHeight;
        erasedPixelCount = 0;
        hasWon = false;
    }

    // --- XỬ LÝ KHI KÉO THẢ CỤC TẨY ---
    protected override void OnDragStarted()
    {
        base.OnDragStarted();
        oldPoint = draggableComponent.transform.position; // Lưu vị trí bắt đầu kéo
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        if (canPlayerInteract) return;

        draggableComponent.transform.Translate(mouseDelta); // Di chuyển cục tẩy

        // Nếu cục tẩy di chuyển đủ xa, thì thực hiện vẽ lên mặt nạ
        if (Vector2.Distance(oldPoint, draggableComponent.transform.position) > 0.1f)
        {
            Vector3 currentEraserPosition = draggableComponent.transform.position;
            DrawOnMask(currentEraserPosition); // Gọi hàm vẽ
            oldPoint = currentEraserPosition; // Cập nhật vị trí cũ
        }
    }
    protected override void OnDragEnded()
    {
        base.OnDragEnded();
        CheckWinCondition(); // Kiểm tra xem đã thắng chưa

    }
    // --- HÀM "VẼ" LÊN TEXTURE MẶT NẠ ---
    void DrawOnMask(Vector3 worldPosition)
    {
        // Chuyển vị trí thế giới của cục tẩy sang tọa độ pixel trên texture mặt nạ
        PixelPoint maskPixelCoords = WorldToPixelOnMaskTexture(worldPosition);

        int centerX = maskPixelCoords.x;
        int centerY = maskPixelCoords.y;

        // Duyệt qua các pixel xung quanh vị trí cục tẩy (tạo thành hình tròn)
        for (int x = -eraserBrushRadiusOnMask; x <= eraserBrushRadiusOnMask; x++)
        {
            for (int y = -eraserBrushRadiusOnMask; y <= eraserBrushRadiusOnMask; y++)
            {
                if (x * x + y * y <= eraserBrushRadiusOnMask * eraserBrushRadiusOnMask) // Điều kiện hình tròn
                {
                    int Px = centerX + x;
                    int Py = centerY + y;

                    // Kiểm tra xem pixel có nằm trong giới hạn texture không
                    if (Px >= 0 && Px < dynamicMaskTexture.width && Py >= 0 && Py < dynamicMaskTexture.height)
                    {
                        int index = Py * dynamicMaskTexture.width + Px; // Chuyển tọa độ 2D sang 1D

                        // Nếu pixel này trước đó trong suốt (alpha=0), thì tăng biến đếm
                        if (maskPixelColors[index].a == 0)
                        {
                            erasedPixelCount++;
                        }
                        // Đặt pixel này thành mờ đục (alpha=255)
                        maskPixelColors[index] = new Color32(255, 255, 255, 255);
                    }
                }
            }
        }
        needsMaskTextureUpdate = true; // Đánh dấu là texture cần được cập nhật
    }

    // --- CẬP NHẬT TEXTURE VÀ KIỂM TRA THẮNG (CHẠY MỖI FRAME NẾU CẦN) ---
    void LateUpdate()
    {
        if (needsMaskTextureUpdate)
        {
            // Áp dụng các thay đổi pixel từ mảng maskPixelColors vào dynamicMaskTexture
            dynamicMaskTexture.SetPixels32(maskPixelColors);
            dynamicMaskTexture.Apply(); // Rất quan trọng: tải texture lên GPU!

            needsMaskTextureUpdate = false; // Reset cờ

        }
    }

    // --- HÀM KIỂM TRA ĐIỀU KIỆN THẮNG ---
    void CheckWinCondition()
    {
        // Tính tỷ lệ phần trăm đã xóa
        float currentPercentageErased = (float)erasedPixelCount / totalMaskPixels;
        // Debug.Log($"Tỷ lệ đã xóa: {currentPercentageErased * 100:F2}%"); // Bỏ comment để xem % xóa

        // Nếu tỷ lệ xóa đạt ngưỡng VÀ chưa thắng trước đó
        if (currentPercentageErased >= winPercentageThreshold && !hasWon)
        {
            hasWon = true; // Đánh dấu đã thắng để không báo nữa
            canPlayerInteract = true;
            DOVirtual.DelayedCall(1f, () => WinBox.SetUp().Show());
        }
    }

    // --- Cấu trúc phụ trợ để lưu tọa độ pixel ---
    public struct PixelPoint { public int x; public int y; }

    // --- HÀM CHUYỂN ĐỔI TỌA ĐỘ THẾ GIỚI SANG PIXEL TRÊN TEXTURE MẶT NẠ ---
    private PixelPoint WorldToPixelOnMaskTexture(Vector3 worldPos)
    {
        Sprite currentMaskSprite = spriteMask.sprite; // Sprite mặt nạ hiện tại
        Texture2D tex = currentMaskSprite.texture;   // Texture của sprite mặt nạ

        // Chuyển vị trí thế giới (worldPos) sang vị trí cục bộ (localPos) của SpriteMask GameObject
        Vector3 localPos = spriteMask.transform.InverseTransformPoint(worldPos);

        Rect spriteRect = currentMaskSprite.rect; // Vùng texture mà sprite sử dụng
        float PPU = currentMaskSprite.pixelsPerUnit; // Số pixel / 1 đơn vị Unity

        // Tính pivot của sprite (chuẩn hóa 0-1)
        float pivotX_norm = currentMaskSprite.pivot.x / spriteRect.width;
        float pivotY_norm = currentMaskSprite.pivot.y / spriteRect.height;

        // Tính tọa độ pixel tương đối với góc dưới trái của vùng spriteRect
        float Px_on_sprite_rect = (localPos.x * PPU) + (spriteRect.width * pivotX_norm);
        float Py_on_sprite_rect = (localPos.y * PPU) + (spriteRect.height * pivotY_norm);

        // Tính tọa độ pixel cuối cùng trên texture gốc
        int texX = Mathf.FloorToInt(Px_on_sprite_rect + spriteRect.x);
        int texY = Mathf.FloorToInt(Py_on_sprite_rect + spriteRect.y);

        return new PixelPoint { x = texX, y = texY };
    }
}