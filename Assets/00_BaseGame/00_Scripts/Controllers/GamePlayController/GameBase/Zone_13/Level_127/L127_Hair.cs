using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L127_Hair : MonoBehaviour
{
    public SpriteRenderer hairRenderer;
    public Sprite defaultSprite;
    public Sprite hairStraight;
    public BoxCollider2D hairCollider;

    public bool CheckAngle()
    {
        if(transform.eulerAngles.z < 1f && transform.eulerAngles.z > -1f)
        {
            hairCollider.enabled = false;
            FlyingHair();
            return true;
        }
        return false;
    }

    public void FlyingHair()
    {
        transform.DOMoveY(7f,0.5f).SetEase(Ease.OutBack);
    }


    public void OnDragStart()
    {
        hairRenderer.sprite = hairStraight;
    }
    public void OnDragEnd()
    {
        hairRenderer.sprite = defaultSprite;
        transform.DORotate(new Vector3(0,0,31f),0.2f,RotateMode.Fast);
    }
}
