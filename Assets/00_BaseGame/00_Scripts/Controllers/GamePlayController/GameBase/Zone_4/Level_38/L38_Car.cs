using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L38_Car : MonoBehaviour
{
    public Vector2 pos;
    public Vector3 angle;
    public BoxCollider2D colli;
    public void DoRotating()
    {
        transform.DORotate(Vector3.zero, 0.3f, RotateMode.Fast);
    }
    public void DoRotatingAngleDefault()
    {
        transform.DORotate(angle, 0.3f, RotateMode.Fast);
    }

    
}
