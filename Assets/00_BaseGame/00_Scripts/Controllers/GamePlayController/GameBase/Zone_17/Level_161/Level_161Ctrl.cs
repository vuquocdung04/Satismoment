using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_161Ctrl : BaseDragController<L161_ScrewDriver>
{
    public L161_Wood wood;
    public float speedPattern = 5f;
    public int screwsCompleted = 0;
    protected override void OnDragEnded()
    {
        wood.StopMove();
        draggableComponent.OnDragEnd();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        if (draggableComponent.canDrag)
            draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.StartPatternMove(speedPattern);
    }

    public void CheckWin()
    {
        if(screwsCompleted == 4)
        {
            isWin = true;
            StartCoroutine(HandleWinCondition());
        }
    }
    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}
