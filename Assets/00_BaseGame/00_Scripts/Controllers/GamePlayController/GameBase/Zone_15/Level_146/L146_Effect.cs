using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L146_Effect : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public List<Sprite> lsFrames;

    public void Init(Transform posSpawn)
    {
        int rand = Random.Range(0, lsFrames.Count);
        transform.localPosition = posSpawn.localPosition;
        objRenderer.sprite = lsFrames[rand];
        float randY = Random.Range(-0.1f,0.2f);
        float ranX = Random.Range(1f,2f);
        transform.DOLocalMove(posSpawn.localPosition + new Vector3(ranX, randY), 1f).SetEase(Ease.Linear).OnComplete(delegate
        {
            SimplePool2.Despawn(gameObject);
        });
    }
}
