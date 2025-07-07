using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L119_Tissue : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D objCollider;
    public List<Sprite> lsSprites;

    public bool HasProperlyDraggedTissueOut(Transform posCorrect)
    {
        float distance = Vector2.Distance(transform.position, posCorrect.position);
        if(Mathf.Abs(distance) > 0.1f)
        {
            Falling();
            return true;
        }
        return false;
    }

    public void Init()
    {
        int rand = Random.Range(0,lsSprites.Count);
        spriteRenderer.sprite = lsSprites[rand];
    }

    public void Falling()
    {
        objCollider.enabled = false;
        transform.DOMoveY(-7f, 0.5f).SetEase(Ease.InOutExpo).OnComplete(delegate
        {
            objCollider.enabled = true;
            SimplePool2.Despawn(gameObject);
        });
    }
}
