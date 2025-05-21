using UnityEngine;
using Sirenix.OdinInspector;
public class Level_27Ctrl : MonoBehaviour
{
    public Transform bread;
    public SpriteMask spriteMask;
    public int textureWidth = 256;
    public int textureHeight = 256;
    public int drawRadius = 10;
    public Color drawColor = Color.white; // Màu dùng để vẽ (alpha = 1)

    private Texture2D maskTexture;
    private Sprite maskSprite;
    private Vector3 mousePos;
    private bool ninetyPercentReached = false; // Biến cờ
    public bool isWin = false;
    void Start()
    {
        if (spriteMask == null)
        {
            enabled = false;
            return;
        }

        // Sử dụng TextureFormat.Alpha8, pixel được vẽ bằng drawColor (Color.white) sẽ có alpha = 1 (hoặc 255 dạng byte).
        // Pixel được ClearTexture bằng new Color(0,0,0,0) sẽ có alpha = 0.
        maskTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.Alpha8, false);
        ClearTexture(new Color(0, 0, 0, 0)); // Khởi tạo texture trong suốt (alpha = 0)
        maskTexture.Apply();

        maskSprite = Sprite.Create(maskTexture,
                                    new Rect(0.0f, 0.0f, maskTexture.width, maskTexture.height),
                                    new Vector2(0.5f, 0.5f),
                                    100.0f);
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
                if (Vector2.Distance(new Vector2(x, y), center) < radius)
                {
                    maskTexture.SetPixel(x, y, color);
                }
            }
        }
    }

    private void Update()
    {
        if (isWin) return;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = spriteMask.transform.position.z;

        if (Input.GetMouseButtonDown(0)) // Hoặc GetMouseButton(0) nếu muốn vẽ liên tục
        {
            if (maskSprite == null || maskTexture == null) return;

            Vector3 localMousePos = spriteMask.transform.InverseTransformPoint(mousePos);
            float pixelsPerUnit = maskSprite.pixelsPerUnit;
            float texX_normalized = (localMousePos.x / (maskTexture.width / pixelsPerUnit)) + 0.5f;
            float texY_normalized = (localMousePos.y / (maskTexture.height / pixelsPerUnit)) + 0.5f;

            int texX = (int)(texX_normalized * textureWidth);
            int texY = (int)(texY_normalized * textureHeight);

            if (texX >= 0 && texX < textureWidth && texY >= 0 && texY < textureHeight)
            {
                DrawCircle(new Vector2Int(texX, texY), drawRadius, drawColor);
                maskTexture.Apply();

                // Kiểm tra tỷ lệ sau khi vẽ và áp dụng
                if (!ninetyPercentReached) // Chỉ kiểm tra nếu chưa đạt ngưỡng
                {
                    CheckDrawingCoverage();
                }
            }
        }
    }

    void CheckDrawingCoverage()
    {
        // GetPixels32 hiệu quả hơn cho việc đọc từng byte màu
        Color32[] pixels = maskTexture.GetPixels32();
        int drawnPixelCount = 0;
        int totalPixels = textureWidth * textureHeight;

        if (totalPixels == 0) return; // Tránh chia cho 0

        for (int i = 0; i < pixels.Length; i++)
        {
            // Với TextureFormat.Alpha8 và drawColor là Color.white,
            // pixel đã vẽ sẽ có giá trị alpha > 0 (cụ thể là 255).
            // Pixel chưa vẽ (đã clear bằng Color(0,0,0,0)) sẽ có alpha = 0.
            if (pixels[i].a > 0)
            {
                drawnPixelCount++;
            }
        }

        float coveragePercentage = (float)drawnPixelCount / totalPixels;

        if (coveragePercentage > 0.9f)
        {
            ninetyPercentReached = true;
            isWin = true;
            bread.gameObject.SetActive(false);
            WinBox.SetUp().Show();
        }
    }

    void OnDestroy()
    {
        if (maskTexture != null)
        {
            Destroy(maskTexture);
        }
        if (maskSprite != null)
        {
            Destroy(maskSprite);
        }
    }


    [Button("Setup width height", ButtonSizes.Large)]
    void SetUp()
    {
        textureWidth = bread.GetComponent<SpriteRenderer>().sprite.texture.width - 130;
        textureHeight = bread.GetComponent<SpriteRenderer>().sprite.texture.height - 130;
    }

}