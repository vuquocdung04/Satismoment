using DG.Tweening;
using UnityEngine;

public class L76_Tile : MonoBehaviour
{
    public L76_AnimalType animalType; // ID loại Tile (thay cho idFruit)
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider2D;
    private readonly Color originalColor = new Color32(255, 255, 255, 255);
    public bool isMoving;

    public void Darken()
    {
        boxCollider2D.enabled = false;
        spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1f); // Màu xám tối
    }

    public void Restore()
    {
        boxCollider2D.enabled = true;
        spriteRenderer.color = originalColor; // Khôi phục lại màu gốc
    }

    public Tween GetMoveTween(Transform targetSlot)
    {
        // Bật trạng thái đang di chuyển
        isMoving = true;
        
        // Vô hiệu hóa collider trong khi di chuyển để tránh click trong lúc đang bay
        boxCollider2D.enabled = false;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(targetSlot.position, 0.5f).SetEase(Ease.OutQuad));

        seq.AppendCallback(() =>
        {
            // Tắt trạng thái di chuyển sau khi hoàn tất
            isMoving = false;
            // Kích hoạt lại collider
            boxCollider2D.enabled = true;
        });

        return seq;
    }
    private void OnMouseDown()
    {
        if (Level_76Ctrl.Instance.hasLost || isMoving) return;
        boxCollider2D.enabled = false;
        Level_76Ctrl.Instance.OnTileClicked(this);
    }
}