using System.Collections;
using UnityEngine;
using DG.Tweening;

public class L95_PieceStone : BaseDraggableObject
{
    [SerializeField] private float fallDuration = 1f; // Thời gian rơi
    [SerializeField] private float shakeStrength = 0.3f; // Mức độ rung
    [SerializeField] private int shakeVibrato = 10; // Tần số rung
    [SerializeField] private float randomness = 90f; // Độ ngẫu nhiên của hướng rung

    public override void OnStartDrag()
    {
        base.OnStartDrag();

        TriggerBreakEffect();
    }

    private void TriggerBreakEffect()
    {
        objectCollider.enabled = false;
        transform.DOShakeScale(0.5f, shakeStrength, shakeVibrato, randomness, true)
            .OnComplete(() =>
            {
                FallAndBreak();
            });
    }

    private void FallAndBreak()
    {
        Sequence sequence = DOTween.Sequence();

        // Rơi xuống dưới
        sequence.Append(transform.DOMoveY(-5f, fallDuration)
            .SetEase(Ease.InCubic));

        // Xoay khi rơi
        sequence.Join(transform.DORotate(new Vector3(0, 0, 360), fallDuration, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.Linear));
    }

    protected override void ReturnToOriginalPosition()
    {

    }
}