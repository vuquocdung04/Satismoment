using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L82_PicturePiece : MonoBehaviour
{
    public Vector2 posCorrect;
    public BoxCollider2D _collider2d;
    public SpriteRenderer spriteRenderer;
    public void StateStart()
    {
        spriteRenderer.sortingOrder = 5;
    }

    public void HandleCorrectPosition()
    {
        transform.DOMove(posCorrect,0.2f).SetEase(Ease.OutElastic);
        _collider2d.enabled = false;
        StateEnd();
    }
    public void StateEnd()
    {
        spriteRenderer.sortingOrder = 3;
    }

}
