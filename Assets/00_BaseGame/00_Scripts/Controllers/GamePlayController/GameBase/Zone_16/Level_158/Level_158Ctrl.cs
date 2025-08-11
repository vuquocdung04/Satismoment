using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_158Ctrl : BaseDragController<L158_Car>
{
    protected override void OnDragEnded()
    {
        draggableComponent.StopMovement();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.Move(mouseDelta);
        CheckWin();
    }

    protected override void OnDragStarted()
    {
        draggableComponent.ResetCollisionState();
    }


    void CheckWin()
    {
        if(draggableComponent.transform.position.y > 4.35f)
        {
            isWin = true;
            draggableComponent.transform.DOMoveY(6f,0.3f).SetEase(Ease.Linear);
            StartCoroutine(HandleWinCondition());
        }
    }

    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }
}
