using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class BaseSnapBack : LoadAutoComponents
{
    [Header("Snap back Base")]
    public Vector2 positionDefault;
    public float angleDefault;
    public float timeSnapBack = 0.4f;
    public Collider2D _collider;
    public virtual void SnapBackPostion(Ease ease)
    {
        this.transform.DOMove(positionDefault, timeSnapBack).SetEase(ease);
    }

    public virtual void SnapBackRotation(RotateMode rotateMode)
    {
        this.transform.DORotate(new Vector3(0, 0, angleDefault), timeSnapBack, rotateMode);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        positionDefault = transform.position;
        angleDefault = transform.position.z;
        _collider = GetComponent<Collider2D>();
    }
}
