using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_49Ctrl : MonoBehaviour
{
    public int test = 0;
    public Sprite[] spriteBtn;
    public List<L49_Btn> lsBtn;
    public List<L49_item> lsItems;

    public bool HandleCheckWin()
    {
        int idCorrect = lsItems[0].curID;
        for(int i = 1; i < lsItems.Count; i++)
        {
            if (lsItems[i].curID != idCorrect) return false;
        }
        return true;
    }

    public void HandleLose()
    {
        foreach(var item in this.lsItems)
        {
            item.isMove = false;
        }
    }

    public void ResetBtnSprite()
    {
        foreach(var btn in this.lsBtn)
        {
            btn.spriteRenderer.sprite = spriteBtn[1];
        }
    }
}
