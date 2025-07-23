using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L154_Smoke : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public List<Sprite> lsFrames;

    public void InitState()
    {
        int rand = Random.Range(0,lsFrames.Count);
        objRenderer.sprite = lsFrames[rand];
        float targetY = Random.Range(1f,2f);
        float targetX = Random.Range(0.5f, 1f);
        transform.DOMove(new Vector2(targetX, targetY), 0.5f).SetEase(Ease.Linear).OnComplete(delegate
        {
            SimplePool2.Despawn(gameObject);
        });
    }
}
