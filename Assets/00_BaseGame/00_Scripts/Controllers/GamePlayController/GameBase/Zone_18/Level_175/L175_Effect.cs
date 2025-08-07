using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L175_Effect : MonoBehaviour
{
    public SpriteRenderer objRenderer;

    public void InitState()
    {
        objRenderer.color = Color.white;

        objRenderer.DOFade(0f, 1f).OnComplete(delegate
        {
            SimplePool2.Despawn(gameObject);
        });
    }
}
