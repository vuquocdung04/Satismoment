using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_169Ctrl : BaseDragController<L169_Tube>
{
    public Transform toothpasteStrip;
    public Transform toothBrush;
    public Transform mask;
    int winProgress = 0;
    protected override void OnDragEnded()
    {
        if (winProgress == 3)
        {
            isWin = true;
            StartCoroutine(HandleAnimation());
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        
    }

    protected override void OnDragStarted()
    {
        var targetMove = toothpasteStrip.transform.localPosition - new Vector3(0,0.3f,0);
        toothpasteStrip.transform.DOLocalMoveY(targetMove.y, 0.1f).SetEase(Ease.Linear);
        winProgress++;

    }

    IEnumerator HandleAnimation()
    {
        var tubeClone = draggableComponent;
        var move1 = tubeClone.transform.DOMove(new Vector3(-1, 1.45f, 0), 0.2f);
        yield return move1.WaitForCompletion();
        var move2 = toothBrush.transform.DOMoveX(0.43f, 0.3f);
        yield return move2.WaitForCompletion();
        mask.transform.DOMoveX(0f, 0.3f);
        tubeClone.transform.DOMoveX(0f, 0.3f);
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }
}
