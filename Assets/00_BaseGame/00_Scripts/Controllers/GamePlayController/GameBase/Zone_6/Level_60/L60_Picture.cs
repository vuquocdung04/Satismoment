using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L60_Picture : MonoBehaviour
{
    public Vector2 posCorrect;
    public BoxCollider2D _collider2d;
    public Vector2 defaultPos;
    public void DoMovingDefaultPos()
    {
        transform.DOMove(defaultPos,1f).SetEase(Ease.InBack);
    }
}
