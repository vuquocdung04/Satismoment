using UnityEngine;
using DG.Tweening;

public class L71_Fruit : MonoBehaviour
{
    public int idFruit; // ID của quả
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        if (Level_71Ctrl.Instance.hasLost)
        {
            return;
        }
        GameController.Instance.musicManager.PlayPick();
        Level_71Ctrl.Instance.AddFruit(this);
    }
    
    public Tween GetMoveTween(Transform targetSlot)
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        // Tạo một sequence nhỏ cho việc di chuyển và set parent
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(targetSlot.position, 0.5f).SetEase(Ease.OutQuad));
        seq.AppendCallback(() =>
        {
            transform.SetParent(targetSlot);
            transform.localPosition = Vector3.zero;
        });

        return seq;
    }
}