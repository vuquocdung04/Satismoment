using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L122_WaterDrop : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public List<Sprite> lsSprite;
    public void Falling()
    {
        int rand = Random.Range(0,lsSprite.Count);
        objRenderer.sprite = lsSprite[rand];
        float randTimeFalling = Random.Range(2f,2.5f);
        transform.DOMoveY(-6f, randTimeFalling).SetEase(Ease.OutBack).OnComplete(delegate
        {
            SimplePool2.Despawn(gameObject);
        });
    }
}
