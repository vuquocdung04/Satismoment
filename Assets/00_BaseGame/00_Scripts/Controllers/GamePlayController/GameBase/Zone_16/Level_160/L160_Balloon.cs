using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L160_Balloon : MonoBehaviour
{
    public CircleCollider2D objCollider;
    bool isBroken;
    public Vector2 defaultPosition;
    public void InitState()
    {
        if (isBroken) return;
        transform.DOMoveY(9f, 10f).SetEase(Ease.Linear).OnComplete(delegate
        {
            transform.position = defaultPosition;
            InitState();
        });
    }

    public void StopMovement()
    {
        isBroken = true;
        transform.DOKill();
        Destroy(gameObject);
    }
}
