using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class L173_Stone : MonoBehaviour
{
    [Header("Stone Settings")]
    public SpriteRenderer objRenderer;
    public List<Sprite> lsSprites;


    public void InitState()
    {
        // Random sprite
        int rand = Random.Range(0, lsSprites.Count);
        objRenderer.sprite = lsSprites[rand];
        transform.position = new Vector3(3.5f,5f);
        int randX = Random.Range(-5,2);
        transform.DOMove(new Vector3(randX,-5f,0),0.5f).SetEase(Ease.Flash);
        transform.DORotate(new Vector3(0, 0, 720), 1f, RotateMode.WorldAxisAdd).OnComplete(delegate
        {
            SimplePool2.Despawn(gameObject);
        });
    }

    
}
