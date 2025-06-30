using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_100Ctrl : BaseDragControllerVer2<L100_Item>
{
    public List<L100_Plate> lsPlates;
    public List<Transform> lsHands;
    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckPositionCorrect())
        {
            winProgress++;
            if (winProgress == lsItems.Count)
                StartCoroutine(HandleWinCondition());
        }
        else
        {
            draggableComponent.OnEndDrag();
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.localPosition += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
    }

    public override IEnumerator HandleWinCondition()
    {
        isWin = true;
        lsHands[0].DOMove(new Vector2(2,-2),0.5f).SetEase(Ease.Linear);
        lsHands[1].DOMove(new Vector2(2,2),0.5f).SetEase(Ease.Linear);
        lsHands[2].DOMove(new Vector2(-2,2),0.5f).SetEase(Ease.Linear);
        lsHands[3].DOMove(new Vector2(-2,-2),0.5f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.51f);
        foreach (var plate in this.lsPlates) plate.SetParent();
        lsHands[0].DOMove(new Vector2(6, -6), 0.5f).SetEase(Ease.Linear);
        lsHands[1].DOMove(new Vector2(6, 6), 0.5f).SetEase(Ease.Linear);
        lsHands[2].DOMove(new Vector2(-6, 6), 0.5f).SetEase(Ease.Linear);
        lsHands[3].DOMove(new Vector2(-6, -6), 0.5f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.51f);
        yield return base.HandleWinCondition();

    }




    protected override void SetupAfter()
    {
        foreach(var item in this.lsItems)
        {
            item.InitAfter();
        }
    }

    protected override void SetupBefore()
    {
        foreach (var item in this.lsItems)
        {
            item.InitBefore();
        }
    }

}
