using UnityEngine;

public class PaintEraser : MonoBehaviour
{
    public Texture2D canvasTexture; // Reference to your paint canvas texture
    public int brushSize = 10;       // Size of the eraser
    public Transform roller;         // Reference to your Paint_Roller object

    private Color32[] eraseColors;
    private bool isErasing = false;
    private Vector2 lastPosition;

    void Start()
    {
        // If canvas texture isn't assigned, try to find it
        if (canvasTexture == null)
        {
            // Try to find a renderer with the main texture
            Renderer paintRenderer = GameObject.Find("Paint").GetComponent<Renderer>();
            if (paintRenderer != null)
            {
                // Get the current texture and make a readable copy
                Texture2D sourceTexture = paintRenderer.material.mainTexture as Texture2D;
                canvasTexture = MakeReadableTextureCopy(sourceTexture);
                paintRenderer.material.mainTexture = canvasTexture;
            }
        }

        // Initialize the erase brush (transparent pixels)
        eraseColors = new Color32[brushSize * brushSize];
        for (int i = 0; i < eraseColors.Length; i++)
        {
            eraseColors[i] = new Color32(0, 0, 0, 0); // Completely transparent
        }
    }

    Texture2D MakeReadableTextureCopy(Texture2D source)
    {
        // Create a temporary RenderTexture
        RenderTexture renderTex = RenderTexture.GetTemporary(
            source.width,
            source.height,
            0,
            RenderTextureFormat.ARGB32
        );

        // Copy the texture content
        Graphics.Blit(source, renderTex);

        // Create a new readable texture
        Texture2D readableTexture = new Texture2D(source.width, source.height, TextureFormat.RGBA32, false);

        // Remember the active render texture
        RenderTexture previous = RenderTexture.active;

        // Set the render texture as active
        RenderTexture.active = renderTex;

        // Read pixels from the RenderTexture to the readable texture
        readableTexture.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableTexture.Apply();

        // Restore the previously active render texture
        RenderTexture.active = previous;

        // Release the temporary render texture
        RenderTexture.ReleaseTemporary(renderTex);

        return readableTexture;
    }

    void Update()
    {
        if (canvasTexture == null) return;

        // Check for mouse input
        if (Input.GetMouseButtonDown(0))
        {
            isErasing = true;
            Erase();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isErasing = false;
        }
        else if (isErasing && Input.GetMouseButton(0))
        {
            Erase();
        }
    }

    void Erase()
    {
        // Convert mouse position to world position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if we hit the paint object
            if (hit.collider.gameObject.name == "Paint")
            {
                // Get UV coordinates
                Vector2 pixelUV = hit.textureCoord;
                Vector2 pixelPos = new Vector2(
                    Mathf.FloorToInt(pixelUV.x * canvasTexture.width),
                    Mathf.FloorToInt(pixelUV.y * canvasTexture.height)
                );

                // If this is a continued erase, draw a line between last position and current
                if (isErasing && Vector2.Distance(pixelPos, lastPosition) > brushSize / 2)
                {
                    EraseLineBetween(lastPosition, pixelPos);
                }
                else
                {
                    // Erase at current position
                    EraseAtPosition(pixelPos);
                }

                // Move the roller to the hit point
                if (roller != null)
                {
                    roller.position = hit.point + new Vector3(0, roller.localScale.y / 2, 0);
                }

                // Update the last position
                lastPosition = pixelPos;
            }
        }
    }

    void EraseAtPosition(Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x);
        int y = Mathf.FloorToInt(pos.y);

        // Calculate the starting position to ensure the brush is centered
        int startX = x - brushSize / 2;
        int startY = y - brushSize / 2;

        // Apply the erase colors (transparent) to the texture
        canvasTexture.SetPixels32(startX, startY, brushSize, brushSize, eraseColors);

        // Apply changes to the texture
        canvasTexture.Apply();
    }

    void EraseLineBetween(Vector2 start, Vector2 end)
    {
        // Calculate the number of steps based on distance
        float distance = Vector2.Distance(start, end);
        int steps = Mathf.CeilToInt(distance / (brushSize / 4f));

        // Interpolate points along the line and erase at each point
        for (int i = 0; i <= steps; i++)
        {
            float t = i / (float)steps;
            Vector2 point = Vector2.Lerp(start, end, t);
            EraseAtPosition(point);
        }
    }
}