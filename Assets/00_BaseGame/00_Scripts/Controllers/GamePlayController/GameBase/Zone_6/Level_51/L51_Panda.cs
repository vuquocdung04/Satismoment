using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L51_Panda : MonoBehaviour
{
    public SpriteRenderer spriteEye;
    public SpriteRenderer spriteMounts;
    public Transform boxThink;
    public Transform leaf;
    public Sprite[] arrMounts;
    public Sprite[] arrEyes;

    public void HandleWinAnimation()
    {
        boxThink.gameObject.SetActive(false);
        StartCoroutine(PlayAnim(spriteEye, arrEyes, false));
        StartCoroutine(PlayAnim(spriteMounts, arrMounts, true));
    }

    IEnumerator PlayAnim(SpriteRenderer spriteRenderer, Sprite[] arrSprite, bool isAmountCorotine = false)
    {
        spriteRenderer.sprite = arrSprite[0];
        yield return new WaitForSeconds(1f);
        spriteRenderer.sprite = arrSprite[1];
        if(isAmountCorotine) leaf.gameObject.SetActive(false);
    }
}
