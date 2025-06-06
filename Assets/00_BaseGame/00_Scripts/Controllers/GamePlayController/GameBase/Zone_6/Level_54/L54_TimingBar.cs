using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L54_TimingBar : MonoBehaviour
{
    public float minYPosition = -1.25f;
    public float maxYPosition = 1.25f;
    public float moveDuration = 1f;
    public Transform thumb;
    private Tween thumbTween;
    public void DoThumbMoving()
    {
        float fullDistance = maxYPosition - minYPosition;
        float initialDistance = maxYPosition - 0;
        float initialDuration = moveDuration * (initialDistance / fullDistance);

        thumbTween = thumb.DOLocalMoveY(maxYPosition, initialDuration)
            .SetEase(Ease.OutSine)
            .OnComplete(() =>
            {
                thumbTween = thumb.DOLocalMoveY(minYPosition, moveDuration)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo);
            });
    }
    public void DoThumbPausing()
    {
        if (thumbTween != null && thumbTween.IsActive()) thumbTween.Pause(); 
    }
    public void DoThumbResuming()
    {
        if (thumbTween != null) thumbTween.Play();
    }

}
