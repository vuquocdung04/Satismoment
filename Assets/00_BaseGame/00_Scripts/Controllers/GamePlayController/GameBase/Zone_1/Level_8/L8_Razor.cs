using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L8_Razor : MonoBehaviour
{
    public int amount = 0;
    public SpriteRenderer brother;
    public List<Sprite> lsSprites;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hair = collision.GetComponent<L8_BackHair>();
        if (hair == null) return;
        hair.TriggerFall();
        amount++;

        CheckWin();
    }

    void CheckWin()
    {
        if (amount < 20) return;

        brother.sprite = lsSprites[0];
        DOVirtual.DelayedCall(0.5f, delegate
        {
            brother.sprite = lsSprites[1];
            DOVirtual.DelayedCall(0.5f, () => brother.sprite = lsSprites[0]);
            DOVirtual.DelayedCall(1.5f, () => WinBox.SetUp().Show());
        });
        


    }
}
