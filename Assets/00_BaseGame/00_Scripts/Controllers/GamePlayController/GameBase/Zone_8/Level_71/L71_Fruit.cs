using UnityEngine;
using DG.Tweening;

public class L71_Fruit : MonoBehaviour
{
    public int idFruit; // ID của quả
    private void OnMouseDown()
    {
        Level_71Ctrl.Instance.AddFruit(this);
    }
    public Tween GetMoveTween(Transform targetSlot)
    {
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