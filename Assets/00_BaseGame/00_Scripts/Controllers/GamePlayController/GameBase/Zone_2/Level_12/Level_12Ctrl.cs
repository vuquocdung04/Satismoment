
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;
using System.Collections;

public class Level_12Ctrl : BaseDragController<L12_PaintRoller>
{
    private float lastApplyTime;
    public float applyInterval = 0.05f;
    protected override void OnDragStarted()
    {
        
    }
    float lastShavingTime;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
        draggableComponent.DrawAtPosition(draggableComponent.transform.position);

        if (Time.time - lastApplyTime > applyInterval)
        {
            draggableComponent.ApplyMaskChanges();
            lastApplyTime = Time.time;
            
        }
    }

    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckDrawingCoverage())
        {
            isWin = true;
            StartCoroutine(HandleWinCondition());
        }
    }

    private IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}