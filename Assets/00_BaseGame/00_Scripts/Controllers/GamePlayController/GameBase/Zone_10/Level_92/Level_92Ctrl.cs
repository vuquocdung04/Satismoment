using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_92Ctrl : BaseDragController<L92_Food>
{
    public int winProgress;
    public List<L92_Food> lsFoods;


    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckPositionCorrect())
        {
            winProgress++;
            if (winProgress == lsFoods.Count)
                StartCoroutine(HandleWinCondition());
        }
        else
        {
            draggableComponent.OnEndDrag();
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.2f);
        WinBox.SetUp().Show();
    }

    [Button("SetupAfter", ButtonSizes.Large)]
    void SetupAfter()
    {
        foreach (var item in this.lsFoods)
        {
            item.InitCorrect();
        }
    }
    [Button("SetupBefore", ButtonSizes.Large)]

    void SetupBefore()
    {
        foreach (var item in this.lsFoods)
        {
            item.InitDefault();
        }
    }
}
