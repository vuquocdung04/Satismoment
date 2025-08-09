using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L179_Btn : MonoBehaviour
{
    public Level_179Ctrl levelCtrl;
    bool isDone;
    bool isReady = true;
    private void OnMouseDown()
    {
        if (isDone) return;
        if (!isReady) return;
        isReady = false;

        if (levelCtrl.isWin)
        {
            levelCtrl.OnWin();
            isDone = true;
        }

        transform.DOMoveY(1.34f, 0.2f).SetEase(Ease.Linear).OnComplete(delegate
        {
            transform.DOMoveY(1.61f, 0.1f).OnComplete(delegate
            {
                isReady = true;
            });
        });
    }
}
