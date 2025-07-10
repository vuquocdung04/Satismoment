using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L124_TimmingBar : MonoBehaviour
{
    public float duration = 0.1f;
    public Transform mask;
    public IEnumerator Init()
    {
        Tween maskMove = mask.DOLocalMoveX(1.5f, duration).SetEase(Ease.Linear);
        yield return maskMove.WaitForCompletion();
        ResetState();
    }
    void ResetState()
    {
        mask.localPosition = Vector3.zero;
        SimplePool2.Despawn(gameObject);
    }
}
