using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L156_scrap : MonoBehaviour
{
    public void Moving()
    {
        transform.DOMoveY(transform.position.y - 2f, 0.5f).SetEase(Ease.Linear).OnComplete(delegate
        {
            SimplePool2.Despawn(gameObject);
        });
    }
}
