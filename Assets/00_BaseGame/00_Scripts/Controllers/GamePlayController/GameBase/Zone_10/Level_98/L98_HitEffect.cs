using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L98_HitEffect : MonoBehaviour
{
    public SpriteRenderer effectSprite;
    public List<Sprite> lsEffectFrames;

    public IEnumerator DesSpawn()
    {
        int rand = Random.Range(0,lsEffectFrames.Count);
        effectSprite.sprite = lsEffectFrames[rand];
        yield return new WaitForSeconds(1f);
        SimplePool2.Despawn(gameObject);

    }
}
