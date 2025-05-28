using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L43_Penguin : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> lsSprites;

    public void DoChangingSprite()
    {
        StartCoroutine(ChangingAnimation());
    }

    IEnumerator ChangingAnimation()
    {
        spriteRenderer.sprite = lsSprites[0];
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = lsSprites[1];
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = lsSprites[2];
    }
}
