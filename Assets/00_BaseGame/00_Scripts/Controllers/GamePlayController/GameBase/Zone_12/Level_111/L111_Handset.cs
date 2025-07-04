using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L111_Handset : MonoBehaviour
{
    public Level_111Ctrl levelCtrl;
    private void OnMouseDown()
    {
        if (!levelCtrl.isOpenHandseted)
        {
            levelCtrl.isOpenHandseted = true;
            transform.DORotate(new Vector3(0, 0, 15f), 0.5f);
        }
    }
}
