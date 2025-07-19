using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L149_Hand : MonoBehaviour
{
    public int id;
    public bool hadFruit;
    public Vector2 originalPosition;
    public Vector2 targetPosition;
    public CircleCollider2D objCollider;
    public void ReachOut()
    {
        if (hadFruit) return;
        transform.DOMove(targetPosition,0.1f).SetEase(Ease.Linear);
    }
    public void PullBack()
    {
        transform.DOMove(originalPosition,0.3f).SetEase(Ease.Linear);
    }
}
