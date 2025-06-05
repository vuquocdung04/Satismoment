using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L53_Button : MonoBehaviour
{
    public Level_53Ctrl levelCtrl;
    public SpriteRenderer spriteRenderer;
    public Sprite[] arrSprites;

    private void OnMouseDown()
    {
        if (!levelCtrl.hookRod.isPullReady)
            StartCoroutine(levelCtrl.hookRod.PullDown());
        StartCoroutine(PlayAnimClickButton());
    }

    IEnumerator PlayAnimClickButton()
    {
        spriteRenderer.sprite = arrSprites[0];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = arrSprites[1];
    }


}
