using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_141Ctrl : BaseDragController<L141_CandyJarLid>
{
    protected override void OnDragEnded()
    {
        
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        if (deltaMousePosition.x <= 0)
        {
            draggableComponent.MoveDecor(deltaMousePosition, delegate
            {
                StartCoroutine(HandleWinCondition());
            });
        }
    }

    protected override void OnDragStarted()
    {
        
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(2f);
        WinBox.SetUp().Show();
    }
}
