using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L124_TimmingBar : MonoBehaviour
{
    public Transform mask;
    public IEnumerator Init()
    {
        Tween maskMove = mask.DOLocalMoveX(1.5f, 0.5f).SetEase(Ease.Linear);
        yield return maskMove.WaitForCompletion();
        ResetState();
    }
    void ResetState()
    {
        mask.localPosition = Vector3.zero;
        SimplePool2.Despawn(gameObject);
    }
}
