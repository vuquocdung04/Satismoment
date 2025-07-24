using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L157_Cork : MonoBehaviour
{
    public float limitY;
    Vector3 newPos;
    bool canMove = false;
    public void MovingY(float speed)
    {
        if (canMove) return;
        newPos = transform.position + new Vector3(0,speed,0);
        newPos.y = Mathf.Clamp(newPos.y,transform.position.y,limitY);
        transform.position = newPos;
    }

    public void CheckCorkOpened(System.Action callback =null)
    {
        if (canMove) return;
        if (Mathf.Abs(transform.position.y - limitY) < 0.1f)
        {
            canMove = true;
            transform.DOMoveY(7f,0.2f).SetEase(Ease.Linear);
            callback?.Invoke();
        }
    }
}
