using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L114_Button : MonoBehaviour
{
    public Level_114Ctrl levelCtrl;
    public bool isOpened;
    private void OnMouseDown()
    {
        if (isOpened) return;
        PlayingAnimation();
        levelCtrl.effect.StartAnimation();
        isOpened = true;
    }

    void PlayingAnimation()
    {
        transform.DOMoveY(transform.position.y - 0.15f, 0.4f).SetEase(Ease.Linear);
    }
}
