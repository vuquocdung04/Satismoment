using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L37_Card : MonoBehaviour
{
    public int idCard;
    public Vector2 pos;
    public SpriteRenderer spriteRenderer;
    public Sprite spriteDefault;


    public void DoFlipingCard()
    {
        transform.DORotate(new Vector3(0,90,0),0.2f, RotateMode.Fast).OnComplete(delegate
        {
            spriteRenderer.sprite = spriteDefault;
            transform.DORotate(new Vector3(0, 0, 0), 0.1f, RotateMode.Fast);
        });
    }

    public void DoHidenCard(Sprite spriteHiden)
    {
        transform.DORotate(new Vector3(0, 90, 0), 0.2f, RotateMode.Fast).OnComplete(delegate
        {
            spriteRenderer.sprite = spriteHiden;
            transform.DORotate(new Vector3(0, 0, 0), 0.1f, RotateMode.Fast);
        });
    }

    public void DoFlyingAfterDuplicate()
    {
        transform.DOMove(new Vector2(2,-4.2f),0.4f).SetEase(Ease.Linear);
        spriteRenderer.sortingOrder += 1;
    }
}
