using DG.Tweening;
using UnityEngine;

public class L76_Tile : MonoBehaviour
{
    public L76_AnimalType animalType;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider2D;
    private readonly Color originalColor = new Color32(255, 255, 255, 255);
    public bool isMoving;
    private Tween currentTween; // Lưu reference của tween hiện tại

    private void OnDestroy()
    {
        // Kill tất cả tweens liên quan đến tile này
        KillAllTweens();
    }

    public void KillAllTweens()
    {
        // Kill tween hiện tại nếu có
        currentTween?.Kill();
        currentTween = null;
        
        // Kill tweens bằng target
        if (transform != null)
        {
            transform.DOKill(complete: false);
        }
        
        // Kill tweens bằng ID
        DOTween.Kill(this, complete: false);
    }

    public void Darken()
    {
        if (boxCollider2D != null)
            boxCollider2D.enabled = false;
        if (spriteRenderer != null)
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1f);
    }

    public void Restore()
    {
        if (boxCollider2D != null)
            boxCollider2D.enabled = true;
        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;
    }

    public Tween GetMoveTween(Transform targetSlot, System.Action onComplete = null)
    {
        // Kiểm tra null và gameObject còn tồn tại
        if (targetSlot == null || transform == null || gameObject == null)
        {
            onComplete?.Invoke();
            return null; // Trả về null thay vì empty sequence
        }

        isMoving = true;
        
        if (boxCollider2D != null)
            boxCollider2D.enabled = false;

        // Kill tween cũ trước khi tạo mới
        KillAllTweens();

        // Tạo tween mới và lưu reference
        currentTween = transform.DOMove(targetSlot.position, 0.5f)
            .SetEase(Ease.OutQuad)
            .SetTarget(transform) // Set target để DOTween track properly
            .SetId(this); // Set ID để có thể kill theo ID
        
        currentTween.OnComplete(() =>
        {
            // Kiểm tra object còn tồn tại trước khi thực hiện
            if (this != null && gameObject != null)
            {
                isMoving = false;
                if (boxCollider2D != null)
                    boxCollider2D.enabled = true;
                
                onComplete?.Invoke();
            }
            currentTween = null; // Clear reference sau khi complete
        });

        // Set OnKill callback để cleanup
        currentTween.OnKill(() =>
        {
            if (this != null && gameObject != null)
            {
                isMoving = false;
                if (boxCollider2D != null)
                    boxCollider2D.enabled = true;
            }
            currentTween = null;
        });

        return currentTween;
    }
    
    private void OnMouseDown()
    {
        // Kiểm tra đầy đủ điều kiện
        if (Level_76Ctrl.Instance == null || 
            Level_76Ctrl.Instance.hasLost || 
            Level_76Ctrl.Instance.hasWon || // Thêm check win
            isMoving) 
            return;
            
        if (boxCollider2D != null)
            boxCollider2D.enabled = false;
            
        Level_76Ctrl.Instance.OnTileClicked(this);
        
        // Play sound effect
        if (GameController.Instance != null && GameController.Instance.musicManager != null)
            GameController.Instance.musicManager.PlayPick();
    }
}