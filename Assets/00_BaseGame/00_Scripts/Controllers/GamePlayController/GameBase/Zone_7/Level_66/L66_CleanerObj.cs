using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class L66_CleanerObj : MonoBehaviour
{
    public Level_66Ctrl levelCtrl;
    public List<Transform> lsMasks;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.SetParent(transform);
        levelCtrl.winProgress++;

        if(levelCtrl.winProgress == 13)
        {
            levelCtrl.isWin = true;
            DoAnimWinCodition();
        }
    }



    public void DoAnimWinCodition()
    {
        Sequence animSequence = DOTween.Sequence();

        animSequence.Append(this.transform.DOMove(new Vector2(-1.12f, -2.05f), 0.5f).SetEase(Ease.InOutQuad));
        animSequence.Join(this.transform.DORotate(new Vector3(0, 0, -90f), 0.5f).SetEase(Ease.InOutQuad));

        animSequence.Append(this.transform.DOMoveY(4.15f, 1f).SetEase(Ease.InOutQuad));
        animSequence.Join(lsMasks[0].DOMoveY(6.66f, 1f).SetEase(Ease.InOutQuad));

        animSequence.Append(this.transform.DORotate(new Vector3(0, 0, 90f), 0.5f).SetEase(Ease.InOutQuad));
        animSequence.Join(this.transform.DOMove(new Vector2(1f, 2.05f), 0.5f).SetEase(Ease.InOutQuad));

        animSequence.Append(this.transform.DOMoveY(-3.81f, 1f).SetEase(Ease.InOutQuad));
        animSequence.Join(lsMasks[1].DOMoveY(-6.72f, 1f).SetEase(Ease.InOutQuad));

        animSequence.AppendInterval(0.3f);
        animSequence.OnComplete(() => HandleWinCodition());
    }

    void HandleWinCodition()
    {

        WinBox.SetUp().Show();
    }
}