using DG.Tweening;
using System;
using UnityEngine;

public class L114_HeadVacuum : MonoBehaviour
{
    public Level_114Ctrl levelCtrl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        levelCtrl.winProgress++;
        collision.gameObject.SetActive(false);
        if(levelCtrl.winProgress == 10)
        {
            StartCoroutine(levelCtrl.HandleWinCondition());
        }
    }
}