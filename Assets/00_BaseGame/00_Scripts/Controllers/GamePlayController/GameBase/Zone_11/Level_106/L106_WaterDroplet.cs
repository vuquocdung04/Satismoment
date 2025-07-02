using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L106_WaterDroplet : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public CircleCollider2D circleCollider;
    public void OnExitState()
    {
        circleCollider.enabled = false;
        spriteRenderer.DOFade(0, 0.5f).OnComplete(delegate
        {
            Destroy(gameObject);
        });
    }

    public void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
}
