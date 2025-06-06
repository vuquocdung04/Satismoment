using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L54_Hand : MonoBehaviour
{
    public float minXPosition = -3f;
    public float maxXPosition = -1f;
    public float moveDuration = 1f;

    private Tween handTween;
    public bool isResumeGame = false;
    bool isWin = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isWin =  true;
        Debug.LogError(collision.name);
    }

    public IEnumerator DoMoveDown()
    {
        isResumeGame = false;
        DoHandPausing();
        Tween moveHandDown = transform.DOMoveY(0.8f,1f);
        yield return moveHandDown.WaitForCompletion();
        if (!isWin)
        {
            Tween moveHandUp =  transform.DOMoveY(2.18f,1f);
            yield return moveHandUp.WaitForCompletion();
            isResumeGame = true;
        }
    }

    public void DoHandMoving()
    {
        float fullDistance = maxXPosition - minXPosition;
        float initialDistance = maxXPosition - (-2);
        float initialDuration = moveDuration * (initialDistance / fullDistance);

        handTween = transform.DOMoveX(maxXPosition, initialDuration)
            .SetEase(Ease.OutSine)
            .OnComplete(() =>
            {
                handTween = transform.DOMoveX(minXPosition, moveDuration)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo);
            });
    }

    public void DoHandPausing()
    {
        if (handTween != null && handTween.IsActive()) handTween.Pause();
    }
    public void DoHandResuming()
    {
        if (handTween != null) handTween.Play();
    }
}
