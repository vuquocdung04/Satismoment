using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L123_HairClipper : MonoBehaviour
{
    public Transform headClip;
    private Tween currentTween;
    private void MoveLeft()
    {
        currentTween = headClip.DOLocalMoveX(-0.04f, 0.1f)
            .SetEase(Ease.Flash)
            .OnComplete(MoveRight);
    }

    private void MoveRight()
    {
        currentTween = headClip.DOLocalMoveX(0.04f, 0.1f)
            .SetEase(Ease.Flash)
            .OnComplete(OnStateStart);
    }

    public void OnStateStart()
    {
        MoveLeft();
    }
    public void OnStateEnd()
    {
        currentTween.Pause();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var catHair = collision.GetComponent<L123_CatHair>();
        if (catHair == null) return;
        catHair.Init();
    }

}
