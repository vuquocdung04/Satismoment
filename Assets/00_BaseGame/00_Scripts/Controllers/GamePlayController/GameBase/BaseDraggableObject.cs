using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDraggableObject : MonoBehaviour
{
    [Header("Sorting Layer")]
    public int orderIndex = 0;

    [Header("Components")]
    public SpriteRenderer spriteRenderer;
    public Collider2D objectCollider;
    [Header("Vi tri chinh xac")]
    public Vector2 posCorrect;
    public Vector2 posDefault;
    [Header("Goc xoay")]
    public float angleDrag;
    public float angleDefault;


    public virtual void OnStartDrag()
    {
        transform.DORotate(new Vector3(0, 0, angleDrag), 0.3f, RotateMode.Fast);
        spriteRenderer.sortingOrder = orderIndex + 2;
    }
    public virtual void OnEndDrag()
    {
        transform.DORotate(new Vector3(0,0, angleDefault), 0.3f, RotateMode.Fast);
        spriteRenderer.sortingOrder = orderIndex;
        ReturnToOriginalPosition();
    }

    public virtual void HandleCorrectCondition()
    {
        transform.DOMove(posCorrect, 0.2f).SetEase(Ease.InBounce);
        spriteRenderer.sortingOrder = orderIndex;
        objectCollider.enabled = false;
    }

    /// <summary>
    ///  Khai bao odin
    /// </summary>
    public void InitAfter()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectCollider = GetComponent<Collider2D>();
        posCorrect = transform.position;
        angleDrag = transform.eulerAngles.z;
        orderIndex = spriteRenderer.sortingOrder;
    }

    public void InitBefore()
    {
        angleDefault = transform.eulerAngles.z;
        posDefault = transform.position;
    }


    public abstract void ReturnToOriginalPosition();
}
