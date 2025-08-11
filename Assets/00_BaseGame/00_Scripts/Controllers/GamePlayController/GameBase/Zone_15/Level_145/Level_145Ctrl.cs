using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_145Ctrl : BaseDragController<L145_SlideOfCheese>
{
    public Transform mintLeaf;
    public Transform cheeseGrater;
    public int winProgress = 0;
    protected override void OnDragEnded()
    {
        if(winProgress == 7)
        {
            isWin = true;
            StartCoroutine(HandleWinCondition());
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {

    }

    IEnumerator HandleWinCondition()
    {
        cheeseGrater.DOMoveX(6f,0.5f).SetEase(Ease.Linear);
        mintLeaf.localScale = Vector3.zero;
        mintLeaf.gameObject.SetActive(true);
        Tween mintScale = mintLeaf.DOScale(Vector3.one,1f).SetEase(Ease.OutBack);
        yield return mintScale.WaitForCompletion();
        Tween mintMove = mintLeaf.DOMoveY(-0.85f,0.5f).SetEase(Ease.Linear);
        yield return mintMove.WaitForCompletion();
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }
}
