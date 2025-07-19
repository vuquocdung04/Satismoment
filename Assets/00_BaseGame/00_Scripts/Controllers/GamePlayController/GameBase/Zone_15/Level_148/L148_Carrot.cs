using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L148_Carrot : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public Sprite fall;

    public void InitEffect()
    {
        float randX = Random.Range(-0.3f,0.3f);
        Vector2 targetMove = new Vector2(randX, -3.4f);
        transform.DOMove(targetMove, 0.5f).SetEase(Ease.Linear).OnComplete(delegate
        {
            objRenderer.sprite = fall;
        });
    }
}
