using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_56Ctrl : BaseDragController<L56_ShakerCup>
{
    public int winProgress;
    public Transform maskTiming;
    public Transform maskMartini;
    public Transform lid;
    public Transform hand;
    public Transform lemon;

    protected override void OnDragEnded()
    {
        StartCoroutine(HandleWinCodition());
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        DoCupShaking();
    }

    L56_ShakerCup cup;
    void DoCupShaking()
    {
        cup = draggableComponent;

        winProgress++;
        maskTiming.transform.DOMoveX(winProgress / 2f, 0.6f);
        cup.transform.DORotate(new Vector3(0, 0, -10f), 0.3f).OnComplete(delegate
        {
            cup.transform.DORotate(new Vector3(0, 0, 10f), 0.3f);
        });
    }
    IEnumerator HandleWinCodition()
    {
        if(winProgress == 6)
        {
            isWin = true;
            lid.SetParent(transform);
            lid.DOMove(new Vector3(0,-1.2f,0),0.5f);
            Tween cupMove = cup.transform.DOMove(new Vector2(-0.33f, 0.6f),0.4f);
            yield return cupMove.WaitForCompletion();
            Tween cupRotate = cup.transform.DORotate(new Vector3(0,0,-60f),0.4f);
            yield return cupMove.WaitForCompletion();
            Tween maskMove = maskMartini.DOMoveY(1.3f,1f);
            yield return maskMove.WaitForCompletion();
            hand.DOMove(new Vector2(2.18f, -0.05f),1f);
            yield return new WaitForSeconds(1.05f);
            lemon.SetParent(transform);
            hand.DOMove(new Vector2(3.62f, -1.44f),1f);

            yield return new WaitForSeconds(0.1f);
            WinBox.SetUp().Show();
        }
    }
}
