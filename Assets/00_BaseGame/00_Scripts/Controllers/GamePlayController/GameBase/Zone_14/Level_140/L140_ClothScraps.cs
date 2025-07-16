using UnityEngine;

public class L140_ClothScraps : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public bool isOverlapping;
    public BoxCollider2D myCollider;

    private void Start()
    {
        CheckOverlap();
    }

    public void CheckOverlap()
    {
        isOverlapping = false;
        Bounds myBounds = myCollider.bounds;

        Collider2D[] nearbyColliders = Physics2D.OverlapAreaAll(myBounds.min, myBounds.max);

        foreach (Collider2D col in nearbyColliders)
        {
            // Bỏ qua các mảnh vải khác (cùng script L140_ClothScraps)
            if (col.GetComponent<L140_ClothScraps>() != null) continue;

            // Kiểm tra Bounds giao nhau
            if (myBounds.Intersects(col.bounds))
            {
                isOverlapping = true;
                Debug.Log("Giao với: " + col.name);
                break;
            }
        }
    }
}