using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L46_Scissors : MonoBehaviour
{
    public List<Transform> lsPieces;
    public float rotateAngle = 10f;      // góc xoay mỗi bên
    public float duration = 0.3f;        // thời gian xoay
    public void DoAnimation()
    {
        if (lsPieces.Count < 2) return;

        Sequence seq = DOTween.Sequence();

        // Cắt (xoay vào trong)
        seq.Append(lsPieces[0].DOLocalRotate(new Vector3(0, 0, -rotateAngle), duration));
        seq.Join(lsPieces[1].DOLocalRotate(new Vector3(0, 0, rotateAngle), duration));

        // Mở lại (xoay về 0)
        seq.Append(lsPieces[0].DOLocalRotate(Vector3.zero, duration));
        seq.Join(lsPieces[1].DOLocalRotate(Vector3.zero, duration));

        seq.SetEase(Ease.InOutSine);
    }

}
