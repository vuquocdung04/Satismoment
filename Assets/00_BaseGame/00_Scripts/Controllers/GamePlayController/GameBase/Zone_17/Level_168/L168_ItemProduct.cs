using UnityEngine;

public class L168_ItemProduct : MonoBehaviour
{
    public SpriteRenderer objRenderer;       // Renderer gốc
    [HideInInspector] public int pointIndex; // Index trong lsPoints
    [HideInInspector] public bool IsCovered; // Được Setup cập nhật

    public void InitSprite(Sprite sprite) => objRenderer.sprite = sprite;

    public void SetSortingOrder(int order) => objRenderer.sortingOrder = order;
}
