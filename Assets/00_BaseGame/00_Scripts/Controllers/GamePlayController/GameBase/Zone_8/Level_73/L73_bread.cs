using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L73_bread : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite sprite;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void DoActionBeforeToasting()
    {
        transform.DOMoveY(1.146f,0.5f).SetEase(Ease.OutBounce);
        spriteRenderer.sprite = sprite;
    }
}
