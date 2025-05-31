using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L49_Btn : MonoBehaviour
{
    public Level_49Ctrl levelCtrl;
    public L49_item item;
    public SpriteRenderer spriteRenderer;
     void OnMouseDown()
    {
        spriteRenderer.sprite = levelCtrl.spriteBtn[0];

        item.StopAndSnap();
        item.GetIdByPosStop();
        levelCtrl.test++;

        if(levelCtrl.test > 2)
        {
            if (levelCtrl.HandleCheckWin())
            {
                WinBox.SetUp().Show();
            }
            else
            {
                levelCtrl.ResetBtnSprite();
                levelCtrl.HandleLose();
                levelCtrl.test = 0;
            }
        }
    }
}
