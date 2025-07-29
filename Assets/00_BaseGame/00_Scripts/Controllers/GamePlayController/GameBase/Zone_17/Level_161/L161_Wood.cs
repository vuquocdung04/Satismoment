using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L161_Wood : MonoBehaviour
{
    Tween woodTween;
    bool moveDone = false;
    void Moving()
    {
        if (moveDone) return;
        woodTween = transform.DOMoveY(3.5f, 2f).SetEase(Ease.Linear).OnComplete(delegate
        {
            moveDone = true;
            woodTween.Kill();
            woodTween = null;
        });
    }

    public void StopMove()
    {
        if (woodTween != null)
            woodTween.Pause();
    }

    public void StartMove()
    {
        if (moveDone) return;
        if(woodTween == null)
        {
            Moving();
        }
        else woodTween.Play();
    }
}
