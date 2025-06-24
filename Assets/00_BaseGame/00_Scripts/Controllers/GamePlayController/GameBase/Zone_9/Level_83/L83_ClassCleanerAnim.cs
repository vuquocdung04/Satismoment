using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L83_ClassCleanerAnim : MonoBehaviour
{
    public Transform mask;
    bool isFirstClean = false;
    public void ChangeSpriteFirst()
    {
        if (isFirstClean) return;
        isFirstClean = true;
        mask.transform.DOLocalMoveY(0f,1.5f).SetEase(Ease.Linear);
    }
}
