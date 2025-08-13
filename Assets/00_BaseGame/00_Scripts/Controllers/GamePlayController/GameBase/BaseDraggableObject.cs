using DG.Tweening;
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
        transform.DORotate(new Vector3(0, 0, angleDrag), 0.3f);
        spriteRenderer.sortingOrder = orderIndex + 2;
    }
    public virtual void OnEndDrag()
    {
        transform.DORotate(new Vector3(0,0, angleDefault), 0.3f);
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
    public virtual void InitCorrect()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer == null) { Debug.LogError("SpriteRenderer null, bo qua neu can thiet"); }
        objectCollider = GetComponent<Collider2D>();
        posCorrect = transform.position;
        angleDrag = transform.eulerAngles.z;
        if (spriteRenderer != null)
            orderIndex = spriteRenderer.sortingOrder;
    }

    public virtual void InitDefault()
    {
        angleDefault = transform.eulerAngles.z;
        posDefault = transform.position;
    }


    protected abstract void ReturnToOriginalPosition();
}
