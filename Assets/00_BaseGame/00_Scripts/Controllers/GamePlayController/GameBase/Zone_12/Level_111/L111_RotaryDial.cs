using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L111_RotaryDial : MonoBehaviour
{
    public CircleCollider2D objColllider;
    public L111_NumberDial numberDial;
    public List<Transform> lsPoints;

    
    public bool CheckNumberCorrect(BoxCollider2D collider2d)
    {
        if (numberDial.circleCollider.IsTouching(collider2d))
        {
            return true;
        }
        return false;
    }
    public void SetPositionAndSpriteNumber(int index)
    {
        numberDial.SetPosition(lsPoints[index]);
        numberDial.SetSpriteNumber(index);
    }

    public void OnDragEnded()
    {
        transform.DORotate(Vector3.zero,0.5f,RotateMode.Fast);
    }
}
