using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_48Ctrl : BaseDragController<L48_Eraser>
{
    public Transform board;
    public int winProgress;
    public int textureWidth = 512;
    public int textureHeight = 512;
    public int pixelsPerUnit = 300;
    [Header("Cai dat but ve")]
    public int brushRadius = 10;
    [Tooltip("Màu dùng để vẽ. Alpha = 0 (trong suốt) sẽ 'xóa' mask, Alpha = 1 (đục) sẽ 'vẽ' mask.")]
    public Color brushColor = new Color(1f, 1f, 1f, 0f); // Mặc định là trong suốt (xóa mask)
    public List<Transform> lsFaceBoards;
    [Space(5)]
    public SpriteMask spriteMask;
    private Texture2D maskTexture;
    private Sprite maskSprite;
    private Color32[] clearColors;

    private void Start()
    {
        InitMask();
    }
    protected override void OnDragStarted()
    {
        
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
        TryPaint();
    }

    protected override void OnDragEnded()
    {
        CheckDrawingCoverage();
        HandleWinCodition();
    }
    //
    public void InitMask()
    {
        maskTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        maskTexture.wrapMode = TextureWrapMode.Clamp;

        clearColors = new Color32[textureWidth * textureHeight];
        Color32 initialColor = new Color32(255, 255, 255, 255);
        for (int i = 0; i < clearColors.Length; i++) clearColors[i] = initialColor;

        maskTexture.SetPixels32(clearColors);
        maskTexture.Apply();

        //Khoi tao sprite
        maskSprite = Sprite.Create(
            maskTexture,
            new Rect(0f, 0f, textureWidth, textureHeight),
            new Vector2(0.5f,0.5f),
            pixelsPerUnit
            );

        spriteMask.sprite = maskSprite;
    }

    void TryPaint()
    {
        if (draggableComponent == null) return;

        Vector2 localPos = transform.InverseTransformPoint(draggableComponent.transform.position);

        int texX = (int)((localPos.x * pixelsPerUnit) + (textureWidth / 2f));
        int texY = (int)((localPos.y * pixelsPerUnit) + (textureHeight / 2f));

        if (texX >= 0 && texX < textureWidth && texY >= 0 && texY < textureHeight)
        {
            DrawCircle(new Vector2Int(texX, texY), brushRadius, brushColor);
            maskTexture.Apply();
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
                if (Vector2.Distance(new Vector2(x, y), center) < radius)
                {
                    maskTexture.SetPixel(x, y, color);
                }
            }
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
    int erasedPixelCount;
    void CheckDrawingCoverage()
    {
        erasedPixelCount = 0;
        Color32[] pixels = maskTexture.GetPixels32(); 
        int totalPixels = textureWidth * textureHeight;

        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i].a == 0)
            {
                erasedPixelCount++;
            }
        }
        float coveragePercentage = (float)erasedPixelCount / totalPixels;

        if (coveragePercentage >= 0.85f) 
        {
            lsFaceBoards[winProgress].gameObject.SetActive(false);
            winProgress++;
            erasedPixelCount = 0;
            lsFaceBoards[1].GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            InitMask();
        }
        else
        {
            Debug.Log("Chưa đạt 90% độ che phủ. Hãy tiếp tục vẽ!");
        }
    }

    void HandleWinCodition()
    {
        if(winProgress == 2)
        {
            isWin = true;
            WinBox.SetUp().Show();
        }
    }


    [Button("Setup Size texture", ButtonSizes.Large)]
    void Setup()
    {
        textureWidth = board.GetComponent<SpriteRenderer>().sprite.texture.width;
        textureHeight = board.GetComponent<SpriteRenderer>().sprite.texture.height;
    }


}
