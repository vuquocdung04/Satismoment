using UnityEngine;

public class RollerEraser : MonoBehaviour
{
    public Texture2D canvasTexture;     // Reference to the paint canvas texture
    public int eraserSize = 30;         // Size of the eraser effect
    public float rollerSpeed = 5f;      // Speed of the roller movement
    public GameObject rollerObject;     // Reference to the Paint_Roller object

    private Color32[] eraseColors;      // Array of transparent colors for erasing
    private Vector2 lastErasePosition;  // Last position where erasing occurred
    private bool isErasing = false;

    void Start()
    {
        // Try to find the Paint object if canvas texture isn't assigned
        if (canvasTexture == null)
        {
            Renderer paintRenderer = GameObject.Find("Paint").GetComponent<Renderer>();
            if (paintRenderer != null)
            {
                Texture2D sourceTexture = paintRenderer.material.mainTexture as Texture2D;
                // Make sure texture is readable and writable
                canvasTexture = MakeReadableTextureCopy(sourceTexture);
                paintRenderer.material.mainTexture = canvasTexture;
            }
        }

        // Find the roller object if not assigned
        if (rollerObject == null)
        {
            rollerObject = GameObject.Find("Paint_Roller");
        }

        // Initialize the eraser brush with transparent pixels
        eraseColors = new Color32[eraserSize * eraserSize];
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

        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;

        // Read pixels from the RenderTexture to the readable texture
        readableTexture.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableTexture.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);

        return readableTexture;
    }

    void Update()
    {
        if (canvasTexture == null) return;

        // Handle roller movement with arrow keys or WASD
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move the roller based on input
        if (rollerObject != null && (horizontalInput != 0 || verticalInput != 0))
        {
            // Calculate movement direction
            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * rollerSpeed * Time.deltaTime;
            rollerObject.transform.Translate(movement);

            // Start erasing when moving
            isErasing = true;

            // Cast a ray from the roller to the paint surface
            Ray ray = new Ray(rollerObject.transform.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "Paint")
                {
                    // Get texture coordinates from the hit point
                    Vector2 pixelUV = hit.textureCoord;
                    Vector2 pixelPos = new Vector2(
                        Mathf.FloorToInt(pixelUV.x * canvasTexture.width),
                        Mathf.FloorToInt(pixelUV.y * canvasTexture.height)
                    );

                    // If we've moved since last frame, draw a line to ensure continuous erasing
                    if (isErasing && Vector2.Distance(pixelPos, lastErasePosition) > eraserSize / 4)
                    {
                        EraseLineBetween(lastErasePosition, pixelPos);
                    }
                    else
                    {
                        // Just erase at current position
                        EraseAtPosition(pixelPos);
                    }

                    // Update last position
                    lastErasePosition = pixelPos;
                }
            }
        }
        else
        {
            isErasing = false;
        }
    }

    void EraseAtPosition(Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x);
        int y = Mathf.FloorToInt(pos.y);

        // Calculate the starting position to center the eraser
        int startX = x - eraserSize / 2;
        int startY = y - eraserSize / 2;

        // Make sure we're within texture bounds
        if (startX < 0) startX = 0;
        if (startY < 0) startY = 0;
        if (startX + eraserSize > canvasTexture.width) startX = canvasTexture.width - eraserSize;
        if (startY + eraserSize > canvasTexture.height) startY = canvasTexture.height - eraserSize;

        // Apply the transparent colors to erase the texture
        canvasTexture.SetPixels32(startX, startY, eraserSize, eraserSize, eraseColors);

        // Apply changes to the texture
        canvasTexture.Apply();
    }

    void EraseLineBetween(Vector2 start, Vector2 end)
    {
        // Calculate the number of steps based on distance
        float distance = Vector2.Distance(start, end);
        int steps = Mathf.CeilToInt(distance / (eraserSize / 5f));

        // Interpolate points along the line and erase at each point
        for (int i = 0; i <= steps; i++)
        {
            float t = i / (float)steps;
            Vector2 point = Vector2.Lerp(start, end, t);
            EraseAtPosition(point);
        }
    }
}