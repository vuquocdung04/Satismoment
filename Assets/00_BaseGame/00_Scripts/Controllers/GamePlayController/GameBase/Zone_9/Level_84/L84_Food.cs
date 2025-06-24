using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L84_Food : MonoBehaviour
{
    public Vector2 posCorrect;
    public float angle;
    public BoxCollider2D boxCollider2D;
    public SpriteRenderer spriteRenderer;
    public int orderIndex;
    public void StateStartDrag()
    {
        transform.DORotate(new Vector3(0, 0, angle), 0.3f,RotateMode.Fast);
        spriteRenderer.sortingOrder = orderIndex + 2;
    }
    public void StateEndDrag()
    {
        transform.DORotate(Vector3.zero, 0.3f, RotateMode.Fast);
        spriteRenderer.sortingOrder = orderIndex;
    }
    public void HandleConditionCorrect()
    {
        transform.DOMove(posCorrect, 0.2f).SetEase(Ease.InBounce);
        boxCollider2D.enabled = false;
        spriteRenderer.sortingOrder = orderIndex;
    }

}
