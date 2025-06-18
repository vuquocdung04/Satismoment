using DG.Tweening;
using UnityEngine;

public class L76_Tile : MonoBehaviour
{
    public L76_AnimalType animalType; // ID loại Tile (thay cho idFruit)
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D _collider2d;
    private Color originalColor = new Color32(255, 255, 255, 255);
    public bool isMoving = false;

    public void Darken()
    {
        _collider2d.enabled = false;
        spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1f); // Màu xám tối
    }

    public void Restore()
    {
        _collider2d.enabled = true;
        spriteRenderer.color = originalColor; // Khôi phục lại màu gốc
    }

    public Tween GetMoveTween(Transform targetSlot)
    {
        // Bật trạng thái đang di chuyển
        isMoving = true;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(targetSlot.position, 0.5f).SetEase(Ease.OutQuad));

        seq.AppendCallback(() =>
        {
            transform.SetParent(targetSlot);
            transform.localPosition = Vector3.zero;

            // Tắt trạng thái di chuyển sau khi hoàn tất
            isMoving = false;

            // Sau khi xong hiệu ứng, cập lại trạng thái màu
            Restore(); // Đảm bảo màu về bình thường
        });

        return seq;
    }
    private void OnMouseDown()
    {
        if (Level_76Ctrl.Instance.hasLost) return;

        Level_76Ctrl.Instance.OnTileClicked(this);
    }
}