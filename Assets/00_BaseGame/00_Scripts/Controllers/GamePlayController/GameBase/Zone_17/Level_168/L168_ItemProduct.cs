using UnityEngine;

public class L168_ItemProduct : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public BoxCollider2D boxCollider2d;
    public int pointIndex;
    public bool IsCovered;
    public Vector3 originalPosition;
    public int spriteId; // ID để check combo

    public void InitSprite(Sprite sprite, int id)
    {
        objRenderer.sprite = sprite;
        spriteId = id;
        ResetColliderToSpriteBounds();
    }
    public void ResetColliderToSpriteBounds()
    {
        if (objRenderer.sprite != null && boxCollider2d != null)
        {
            // Lấy bounds của sprite
            Bounds spriteBounds = objRenderer.sprite.bounds;

            // Set size và offset của BoxCollider2D
            boxCollider2d.size = spriteBounds.size;
            boxCollider2d.offset = spriteBounds.center;
        }
    }
    public void SetSortingOrder(int order) => objRenderer.sortingOrder = order;

    public void SetCovered(bool covered)
    {
        IsCovered = covered;

        if (covered)
        {
            Color32 c = new Color32(135, 135, 135, 255);
            objRenderer.color = c;
            boxCollider2d.enabled = false;
        }
        else
        {
            Color c = Color.white;
            objRenderer.color = c;
            boxCollider2d.enabled = true;
        }
    }

    public void SetOriginalPosition(Vector3 pos)
    {
        originalPosition = pos;
    }

    public void SetPointIndex(int index)
    {
        pointIndex = index;
    }
}
