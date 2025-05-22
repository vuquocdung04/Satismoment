using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L30_SpinLog : MonoBehaviour
{
    public Level_30Ctrl levelCtrl;
    public void DoSpinning()
    {
        transform.DORotate(new Vector3(0, 0, 360), 5f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1);
    }

    public void IncreaseWinProgress()
    {
        levelCtrl.winProgess++;
        levelCtrl.HandleStatusGame();
    }
    

}
