using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lớp để lưu trữ thông tin cho mỗi móng tay
[System.Serializable]
public class NailInfo
{
    public string nailName; // Để dễ nhận biết trong Inspector
    public SpriteMask spriteMask;
    public Sprite originalNailSprite; // Sprite gốc xác định hình dạng móng

    [HideInInspector] public Texture2D maskTexture;
    [HideInInspector] public Sprite runtimeMaskSprite;
    [HideInInspector] public bool isComplete = false;
    [HideInInspector] public int textureWidth;
    [HideInInspector] public int textureHeight;
    [HideInInspector] public float pixelsPerUnit = 220f; // Giá trị mặc định, có thể tùy chỉnh nếu cần
}

public class Level_29Ctrl : MonoBehaviour
{
    public Transform nail;
    [Header("Nail Brush Settings")]
    public Transform nailBrushTransform; // Giữ nguyên nếu đã có

    // THÊM MỚI HOẶC THAY ĐỔI CÁC BIẾN NÀY:
    private Vector3 initialNailBrushPosition; // Vị trí ban đầu của cọ
    private float nailBrushOperatingZ;      // Độ sâu Z khi cọ di chuyển
    private bool isBrushDragging = false;   // Cờ báo hiệu đang kéo cọ


    public List<NailInfo> nails = new List<NailInfo>(); // Danh sách các móng tay

    public int drawRadius = 20; // Giảm bán kính vẽ một chút so với code gốc là 10
    public Color drawColor = Color.white; // Màu dùng để vẽ (alpha = 1)

    private bool allNailsComplete = false;
    public bool isWin = false; // Có thể vẫn giữ biến này nếu logic game chung cần đến

    void Start()
    {
        // --- Phần khởi tạo các móng tay (NailInfo) của bạn giữ nguyên ---
        if (nails == null || nails.Count == 0)
        {
            Debug.LogError("Không có móng tay nào được cấu hình!");
            enabled = false;
            return;
        }
        for (int i = 0; i < nails.Count; i++)
        {
            NailInfo currentNail = nails[i];
            if (currentNail.spriteMask == null || currentNail.originalNailSprite == null)
            {
                Debug.LogError($"Móng tay '{currentNail.nailName}' (index {i}) chưa được cấu hình đầy đủ.");
                enabled = false;
                return;
            }
            if (currentNail.originalNailSprite != null)
            {
                currentNail.pixelsPerUnit = currentNail.originalNailSprite.pixelsPerUnit;
            }
            currentNail.textureWidth = currentNail.originalNailSprite.texture.width;
            currentNail.textureHeight = currentNail.originalNailSprite.texture.height;
            currentNail.maskTexture = new Texture2D(currentNail.textureWidth, currentNail.textureHeight, TextureFormat.Alpha8, false);
            ClearTexture(currentNail.maskTexture, currentNail.textureWidth, currentNail.textureHeight, new Color(0, 0, 0, 0)); // Bạn cần hàm ClearTexture
            currentNail.maskTexture.Apply();
            currentNail.runtimeMaskSprite = Sprite.Create(currentNail.maskTexture,
                                                      new Rect(0.0f, 0.0f, currentNail.textureWidth, currentNail.textureHeight),
                                                      new Vector2(0.5f, 0.5f),
                                                      currentNail.pixelsPerUnit);
            currentNail.spriteMask.sprite = currentNail.runtimeMaskSprite;
            currentNail.isComplete = false;
        }
        if (nailBrushTransform != null)
        {
            initialNailBrushPosition = nailBrushTransform.position; // Lưu vị trí ban đầu
            nailBrushOperatingZ = initialNailBrushPosition.z;     // Lưu độ sâu Z
            nailBrushTransform.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("NailBrush Transform chưa được gán trong Inspector! Script sẽ bị vô hiệu hóa.");
            enabled = false; // Quan trọng: Vô hiệu hóa script nếu thiếu cọ
            return;
        }
        Cursor.visible = true; // Đảm bảo con trỏ chuột luôn hiển thị
    }

    void ClearTexture(Texture2D texture, int width, int height, Color color)
    {
        Color[] clearColors = new Color[width * height];
        for (int i = 0; i < clearColors.Length; i++)
        {
            clearColors[i] = color;
        }
        texture.SetPixels(clearColors);
        // texture.Apply() sẽ được gọi sau đó
    }

    void DrawCircle(NailInfo nail, Vector2Int center, int radius, Color color)
    {
        int startX = Mathf.Max(0, center.x - radius);
        int endX = Mathf.Min(nail.textureWidth, center.x + radius);
        int startY = Mathf.Max(0, center.y - radius);
        int endY = Mathf.Min(nail.textureHeight, center.y + radius);

        for (int x = startX; x < endX; x++)
        {
            for (int y = startY; y < endY; y++)
            {
                if (Vector2.Distance(new Vector2(x, y), center) < radius)
                {
                    nail.maskTexture.SetPixel(x, y, color);
                }
            }
        }
    }

    void Update()
    {
        if (nailBrushTransform == null) return; // Không làm gì nếu không có cọ

        // Xử lý trạng thái thắng game hoặc hoàn thành
        if (allNailsComplete || isWin)
        {
            if (nailBrushTransform.gameObject.activeSelf)
                nailBrushTransform.gameObject.SetActive(false); // Ẩn cọ khi thắng
            if (!Cursor.visible) Cursor.visible = true; // Đảm bảo con trỏ hiện nếu bị ẩn
            return;
        }

        // 1. Xử lý nhặt cọ khi nhấn chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            // Chuyển tọa độ chuột màn hình sang tọa độ thế giới (world space)
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Kiểm tra xem có nhấp trúng collider 2D của nailBrush không
            // Đảm bảo nailBrush có một Collider2D (ví dụ CircleCollider2D)
            Collider2D hitCollider = Physics2D.OverlapPoint(new Vector2(mouseWorldPos.x, mouseWorldPos.y));

            if (hitCollider != null && hitCollider.transform == nailBrushTransform)
            {
                isBrushDragging = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isBrushDragging)
            {
                isBrushDragging = false;
                nailBrushTransform.position = initialNailBrushPosition;
            }
        }
        if (isBrushDragging)
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane brushPlane = new Plane(Vector3.forward, nailBrushOperatingZ); // Mặt phẳng XY tại độ sâu Z của cọ

                if (brushPlane.Raycast(ray, out float distanceToPlane))
                {
                    nailBrushTransform.position = ray.GetPoint(distanceToPlane);
                }

                PerformPaintingAt(nailBrushTransform.position + new Vector3(0,-1f)); 
            }
            else
            {
                isBrushDragging = false;
                nailBrushTransform.position = initialNailBrushPosition;
            }
        }
    }
    void PerformPaintingAt(Vector3 paintSourceWorldPosition)
    {
        for (int i = 0; i < nails.Count; i++)
        {
            NailInfo currentNail = nails[i];
            if (currentNail.isComplete) continue; // Bỏ qua nếu móng này đã hoàn thành

            // Kiểm tra các đối tượng cần thiết
            if (currentNail.runtimeMaskSprite == null || currentNail.maskTexture == null || currentNail.spriteMask == null) continue;

            // Chuyển vị trí nguồn vẽ (cọ) sang không gian local của SpriteMask của móng tay hiện tại
            Vector3 localDrawPos = currentNail.spriteMask.transform.InverseTransformPoint(paintSourceWorldPosition);

            // Lấy pixelsPerUnit từ runtimeMaskSprite (được tạo từ originalNailSprite)
            float pixelsPerUnit = currentNail.runtimeMaskSprite.pixelsPerUnit;

            // Tính toán kích thước của sprite mask trong world units
            float maskWorldWidth = currentNail.maskTexture.width / pixelsPerUnit;
            float maskWorldHeight = currentNail.maskTexture.height / pixelsPerUnit;

            // Chuyển đổi localDrawPos (với pivot ở giữa sprite) thành tọa độ texture (gốc 0,0 ở dưới trái)
            // Phép cộng 0.5f là để chuyển từ không gian có gốc ở giữa (-0.5 đến 0.5) sang không gian có gốc ở góc (0 đến 1)
            float texX_normalized = (localDrawPos.x / maskWorldWidth) + 0.5f;
            float texY_normalized = (localDrawPos.y / maskWorldHeight) + 0.5f;

            // Chuyển tọa độ đã chuẩn hóa (0-1) thành tọa độ pixel thực tế trên texture
            int texX = (int)(texX_normalized * currentNail.textureWidth);
            int texY = (int)(texY_normalized * currentNail.textureHeight);

            // Kiểm tra xem tọa độ pixel có nằm trong phạm vi của texture không
            if (texX >= 0 && texX < currentNail.textureWidth && texY >= 0 && texY < currentNail.textureHeight)
            {
                DrawCircle(currentNail, new Vector2Int(texX, texY), drawRadius, drawColor); // Bạn cần hàm DrawCircle
                currentNail.maskTexture.Apply(); // Áp dụng thay đổi lên texture
                CheckDrawingCoverage(i); // Bạn cần hàm CheckDrawingCoverage
            }
        }
    }
    void CheckDrawingCoverage(int nailIndex)
    {
        NailInfo currentNail = nails[nailIndex];
        if (currentNail.isComplete) return;

        Color32[] pixels = currentNail.maskTexture.GetPixels32();
        int drawnPixelCount = 0;
        int totalPixels = currentNail.textureWidth * currentNail.textureHeight;

        if (totalPixels == 0) return;

        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i].a > 0) // Alpha > 0 nghĩa là đã được vẽ
            {
                drawnPixelCount++;
            }
        }

        float coveragePercentage = (float)drawnPixelCount / totalPixels;

        if (coveragePercentage > 0.9f)
        {
            currentNail.isComplete = true;
            Debug.Log($"Móng tay '{currentNail.nailName}' đã hoàn thành!");
            CheckForAllNailsComplete();
        }
    }

    void CheckForAllNailsComplete()
    {
        foreach (NailInfo nail in nails)
        {
            if (!nail.isComplete)
            {
                return; // Vẫn còn móng chưa hoàn thành
            }
        }
        allNailsComplete = true;
        isWin = true; // Đặt cờ chiến thắng chung
        nail.gameObject.SetActive(false);
        Debug.Log("Tất cả các móng tay đã hoàn thành! CHIẾN THẮNG!");

        WinBox.SetUp().Show();

    }

    void OnDestroy()
    {
        foreach (NailInfo nail in nails)
        {
            if (nail.maskTexture != null)
            {
                Destroy(nail.maskTexture);
            }
            if (nail.runtimeMaskSprite != null)
            {
                Destroy(nail.runtimeMaskSprite); // Sprite được tạo bởi Sprite.Create cần được Destroy
            }
        }
    }

    [Button("Setup Nail Dimensions (Optional)", ButtonSizes.Large)]
    void EditorSetupNailDimensions()
    {
        if (nails == null || nails.Count == 0)
        {
            Debug.LogWarning("Không có móng tay nào trong danh sách để thiết lập.");
            return;
        }

        int count = 0;
        foreach (NailInfo nail in nails)
        {
            if (nail.originalNailSprite != null && nail.originalNailSprite.texture != null)
            {
                nail.textureWidth = nail.originalNailSprite.texture.width;
                nail.textureHeight = nail.originalNailSprite.texture.height;
                nail.pixelsPerUnit = nail.originalNailSprite.pixelsPerUnit;
                Debug.Log($"Thiết lập kích thước cho '{nail.nailName}': {nail.textureWidth}x{nail.textureHeight}, PPU: {nail.pixelsPerUnit}");
                count++;
            }
            else
            {
                Debug.LogWarning($"Không thể thiết lập kích thước cho '{nail.nailName}' do thiếu originalNailSprite hoặc texture của nó.");
            }
        }
        if (count > 0) Debug.Log($"Đã cập nhật kích thước cho {count} móng tay. Thông tin này sẽ được sử dụng khi chạy game (Start).");
        else Debug.Log("Không có móng tay nào được cập nhật kích thước.");
    }
}