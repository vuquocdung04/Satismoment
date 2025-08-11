using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_153Ctrl : BaseDragController<L153_Clicker>
{
    public Transform leadPencil;
    int clickedCount;
    public int totalClicked = 7;
    protected override void OnDragEnded()
    {
        if(clickedCount == totalClicked)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        
    }

    protected override void OnDragStarted()
    {
        if (!draggableComponent.coolDown)
        {
            draggableComponent.OnStateStart();
            leadPencil.DOMoveY(leadPencil.transform.position.y - 0.2f,0.1f).SetEase(Ease.Linear);
            clickedCount++;
        }
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        leadPencil.DOMoveY(-8f,0.4f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }
}
