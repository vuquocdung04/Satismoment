using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L172_Effect : MonoBehaviour
{
    public void InitState()
    {
        transform.localScale = Vector3.zero;
        float randScale = Random.Range(0.3f,1f);
        transform.DOScale(Vector3.one * randScale, 0.5f).SetEase(Ease.Linear).OnComplete(delegate
        {
            SimplePool2.Despawn(gameObject);
        });
    }
}
