using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L151_PiecePenguin : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public int countTouch;
    public BoxCollider2D objCollider;
    bool isFall = false;
    public void Falling(Level_151Ctrl levelCtrl)
    {
        if(countTouch == 4)
        {
            objCollider.enabled = false;
            transform.DOMoveY(-4.5f,1f).SetEase(Ease.OutBack);
            levelCtrl.winProgress++;
            isFall = true;
        }
    }

    public void ChangeSprite(Sprite sprite)
    {
        if (isFall) return;
        objRenderer.sprite = sprite;
    }
}
