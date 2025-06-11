using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L65_Planet : MonoBehaviour
{
    public Vector2 posCorrect;
    public BoxCollider2D _collider2d;
    public Vector2 defaultPos;
    public void DoMovingDefaultPos()
    {
        transform.DOMove(defaultPos, 0.4f).SetEase(Ease.Linear);
    }

    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        //posCorrect = transform.position;
        _collider2d = GetComponent<BoxCollider2D>();
        defaultPos = transform.position;
    }
}
