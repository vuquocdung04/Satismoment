using UnityEngine;
using DG.Tweening;
using System.Collections;
public class L97_Water : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public IEnumerator  InitEffect()
    {
        Tween blurWater = spriteRenderer.DOFade(0,0.5f);
        yield return blurWater.WaitForCompletion();
        spriteRenderer.color = Color.white;
        SimplePool2.Despawn(gameObject);
    }
}