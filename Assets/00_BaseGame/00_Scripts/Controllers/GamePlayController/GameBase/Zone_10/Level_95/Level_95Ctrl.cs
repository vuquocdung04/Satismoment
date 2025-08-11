using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_95Ctrl : BaseDragController<L95_PieceStone>
{
    public int winProgress;
    public L95_Duck duck;
    public List<L95_PieceStone> lsPieceStones;

    protected override void OnDragEnded()
    {

    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {

    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
        winProgress++;
        if(winProgress == lsPieceStones.Count)
        {
            isWin = true;
            StartCoroutine(HandleWinCondition());
        }
    }

    IEnumerator HandleWinCondition()
    {
        yield return StartCoroutine(duck.PlayBlinkThenMoveEgg());
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }

    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        foreach (var child in this.lsPieceStones)
        {
            child.InitAfter();
            child.InitBefore();
        }
    }
}
