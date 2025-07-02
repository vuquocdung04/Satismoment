using UnityEngine;

public enum L107_DonutType
{
    Donut,
    None,
}

public class L107_PieceDonut : MonoBehaviour
{
    public L107_PieceDonut neighbor;
    public L107_DonutType donutType;
    public float sizeSprite;
    public SpriteRenderer donutRenderer;
    [SerializeField] private float rayLength = 5f; // Chiều dài tia ray (điều chỉnh tùy grid size)

    Vector2[] directions = new Vector2[]
        {
        Vector2.up,    // top
        Vector2.down,  // bottom
        Vector2.left,  // left
        Vector2.right, // right
        };


    private void Start()
    {
        CheckNeighbors();
    }

    public void CheckNeighbors()
    {
        // Chỉ chạy nếu đây là ô loại None
        if (donutType != L107_DonutType.None) return;
        foreach (Vector2 direction in directions)
        {
            // Tính gốc tia từ tâm ô + đẩy ra biên theo hướng
            Vector2 rayOrigin = (Vector2)transform.position + (direction * sizeSprite);

            // Bắn tia ray
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, rayLength);

            // Debug ray màu xanh lá cho trái/phải, đỏ cho trên/dưới
            Color rayColor = (direction == Vector2.left || direction == Vector2.right) ? Color.green : Color.red;
            Debug.DrawRay(rayOrigin, direction * rayLength, rayColor, 10f);

            if (hit.collider != null)
            {
                L107_PieceDonut piece = hit.collider.GetComponent<L107_PieceDonut>();

                // Kiểm tra hợp lệ: không phải chính nó
                if (piece != null && piece != this)
                {
                    piece.neighbor = this;
                }
            }
        }
    }

    public void Init()
    {
        donutRenderer = transform.GetComponent<SpriteRenderer>();
        sizeSprite = donutRenderer.bounds.size.y;
    }
}