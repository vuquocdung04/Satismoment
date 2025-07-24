using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class L157_Effect : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public List<Color> lsColors;


    Vector3 target;
    public void InitState()
    {
        int rand = Random.Range(0, lsColors.Count);
        objRenderer.color = lsColors[rand];
        float targetX = Random.Range(-1f,1f);
        float targetY = Random.Range(3.5f,5f);
        target = new Vector3(targetX, targetY);
        transform.DOMove(target, 0.3f).SetEase(Ease.Linear).OnComplete(delegate
        {
            SimplePool2.Despawn(gameObject);
        });
    }
}
