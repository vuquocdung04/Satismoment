using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L72_Screw : MonoBehaviour
{
    public Level_72Ctrl levelCtrl;
    bool isBeat = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBeat) return;
        levelCtrl.winProgress++;
        transform.DOMoveY(-2.45f,0.04f).SetEase(Ease.Linear);
        isBeat = true;
        if(levelCtrl.winProgress == 10)
        {
            StartCoroutine(levelCtrl.HandleWinCondition());
        }
    }
}
