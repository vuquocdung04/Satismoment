using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L21_Squirrel : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> lsWinSprites;
    public List<Sprite> lsRunSprites;


    public void ChangeAnimWin()
    {
        rb.velocity = Vector2.zero;
        StartCoroutine(Test());

        IEnumerator Test()
        {
            int i = 0;
            var waitTime = new WaitForSeconds(0.5f);
            while( i < 2)
            {
                spriteRenderer.sprite = lsWinSprites[0];
                yield return waitTime;
                spriteRenderer.sprite = lsWinSprites[1];
                yield return waitTime;
                i++;
            }
        }


        DOVirtual.DelayedCall(1f, () => WinBox.SetUp().Show());
    }
}
