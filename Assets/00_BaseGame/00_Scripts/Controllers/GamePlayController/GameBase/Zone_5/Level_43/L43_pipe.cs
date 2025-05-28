using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class L43_pipe : MonoBehaviour
{
    public BoxCollider2D _collider;
    public float angleCorrect = 0;
    public Vector3 rotate;
    public void DoRotating()
    {
        rotate.z += 90;
        transform.DORotate(rotate,0f,RotateMode.Fast);
    }
}
