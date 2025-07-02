using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L105_Pocket : MonoBehaviour
{
    public Level_105Ctrl levelCtrl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var objBallClone = collision.GetComponent<L105_ObjBall>();
        if (objBallClone == null) return;
        objBallClone.OnBallStopped();
        levelCtrl.winProgress++;

        if(objBallClone.ballType == L105_ObjBallType.Black && levelCtrl.winProgress < levelCtrl.ballArranger.totalBalls)
        {
            levelCtrl.ResetGame();
        }

        if(levelCtrl.winProgress == levelCtrl.ballArranger.totalBalls)
        {
            StartCoroutine(levelCtrl.HandleWinConditon());
        }
    }
}
