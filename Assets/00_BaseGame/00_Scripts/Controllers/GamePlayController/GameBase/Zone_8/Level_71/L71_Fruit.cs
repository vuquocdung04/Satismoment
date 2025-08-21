using UnityEngine;
using DG.Tweening;

public class L71_Fruit : MonoBehaviour
{
    public int idFruit; // ID của quả
    public Rigidbody2D rb;
    public bool isMoving; // Thêm cờ để kiểm tra trạng thái di chuyển

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        // Kiểm tra hasLost và isMoving trước khi xử lý
        if (Level_71Ctrl.Instance.hasLost || isMoving) 
            return;
        
        GameController.Instance.musicManager.PlayPick();
        Level_71Ctrl.Instance.OnFruitClicked(this);
    }
    
    public Tween GetMoveTween(Transform targetSlot)
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        isMoving = true;
        
        // Tạo một sequence nhỏ cho việc di chuyển và set parent
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(targetSlot.position, 0.5f).SetEase(Ease.OutQuad));
        seq.AppendCallback(() =>
        {
            // Thiết lập vị trí và parent sau khi di chuyển xong
            transform.SetParent(targetSlot);
            transform.localPosition = Vector3.zero;
            isMoving = false;
        });

        return seq;
    }
}