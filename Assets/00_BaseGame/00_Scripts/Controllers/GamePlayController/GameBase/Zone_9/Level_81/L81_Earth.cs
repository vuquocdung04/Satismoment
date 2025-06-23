using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L81_Earth : MonoBehaviour
{
    public Level_81Ctrl levelCtrl;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> lsSpriteAnim;
    public List<Sprite> lsSpriteDestroys;

    public void ChangeSpriteSleep()
    {
        spriteRenderer.sprite = lsSpriteAnim[0];
    }

    public void ChangeSpritePlay()
    {
        spriteRenderer.sprite = lsSpriteAnim[1];
    }

    public void ChangeSpriteWin()
    {
        spriteRenderer.sprite = lsSpriteAnim[2];
    }

    int indexSprite = 0;
    public IEnumerator ChangeSpriteDestroy()
    {
        var waitTime = new WaitForSeconds(0.2f);
        while (indexSprite < lsSpriteDestroys.Count)
        {
            spriteRenderer.sprite = lsSpriteDestroys[indexSprite];
            yield return waitTime;
            indexSprite++;
        }
        levelCtrl.HandleLoseCodition();
        //
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(ChangeSpriteDestroy());

    }

}
