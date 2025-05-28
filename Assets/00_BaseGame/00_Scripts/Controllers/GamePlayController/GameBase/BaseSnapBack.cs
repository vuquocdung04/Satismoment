using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public abstract class BaseSnapBack : LoadAutoComponents
{
    [Title("Cài đặt hiển thị")]
    [SerializeField]
    private bool showTransform = false;
    [ShowIf("showTransform")]
    public Transform pointCorrect;
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

    public virtual void RotateToZero()
    {
        this.transform.DORotate(Vector3.zero, 0.4f, RotateMode.Fast);
    }



    public virtual float GetDistance()
    {
        if (!showTransform) return 0;
        float distance = Vector2.Distance(this.transform.position, pointCorrect.position);
        return distance;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        positionDefault = transform.position;
        angleDefault = transform.position.z;
        _collider = GetComponent<Collider2D>();
    }
}
