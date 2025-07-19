using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L147_Effect : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public List<Sprite> lsSprites;

    public void InitEffect()
    {
        StartCoroutine(PlayEffectCoroutine());
    }

    IEnumerator PlayEffectCoroutine()
    {
        var waitTime = new WaitForSeconds(0.05f);
        foreach (Sprite sprite in lsSprites)
        {
            objRenderer.sprite = sprite;
            yield return waitTime;
        }

        SimplePool2.Despawn(gameObject);
    }
}