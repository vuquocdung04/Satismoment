using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class L39_Garbage : MonoBehaviour
{
    public Level_39Ctrl levelCtrl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        levelCtrl.isWin = true;
        Debug.LogError(collision.name);
        var paper = collision.transform;
        paper.DOKill();
        // Tạo hiệu ứng lăn về điểm (0,0)
        Sequence seq = DOTween.Sequence();

        seq.Append(paper.DOMove(new Vector3(0,-0.5f,0), 0.8f).SetEase(Ease.InOutSine));

        seq.Join(paper.DORotate(new Vector3(0, 0, 270), 0.8f, RotateMode.FastBeyond360));


        seq.OnComplete(() =>
        {
            WinBox.SetUp().Show();
        });
    }

}
