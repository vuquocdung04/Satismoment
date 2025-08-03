using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // nhớ import DOTween

public class L167_Shaving : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public List<Sprite> lsSprites;

    public void InitState()
    {
        int rand = Random.Range(0, lsSprites.Count);
        objRenderer.sprite = lsSprites[rand];

        objRenderer.DOFade(1f, 0f); // Đảm bảo hiện 100% trước khi fade
        objRenderer.DOFade(0f, 1f).OnComplete(delegate
        {
            SimplePool2.Despawn(gameObject);
        });
    }
}
