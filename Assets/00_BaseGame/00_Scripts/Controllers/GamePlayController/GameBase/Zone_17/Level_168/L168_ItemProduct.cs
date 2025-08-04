using UnityEngine;

public class L168_ItemProduct : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    [HideInInspector] public int pointIndex;
    [HideInInspector] public bool IsCovered;
    [HideInInspector] public Vector3 originalPosition;
    [HideInInspector] public int spriteId; // ID để check combo

    public void InitSprite(Sprite sprite, int id)
    {
        objRenderer.sprite = sprite;
        spriteId = id;
    }

    public void SetSortingOrder(int order) => objRenderer.sortingOrder = order;

    public void SetCovered(bool covered)
    {
        IsCovered = covered;

        if (covered)
        {
            Color c = objRenderer.color;
            c.a = 0.5f;
            objRenderer.color = c;
        }
        else
        {
            Color c = objRenderer.color;
            c.a = 1f;
            objRenderer.color = c;
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
