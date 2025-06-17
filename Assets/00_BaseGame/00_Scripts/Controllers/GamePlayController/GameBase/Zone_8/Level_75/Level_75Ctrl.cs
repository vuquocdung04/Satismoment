using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_75Ctrl : BaseDragController<L75_MoosquitoSpray>
{
    public int winProgress;
    protected override void OnDragEnded()
    {
        draggableComponent.StopSpray();
        if(winProgress == 3)
            StartCoroutine(HandleWinCodition());
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.StartSpray();
    }

    IEnumerator HandleWinCodition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.4f);
        WinBox.SetUp().Show();
    }
}
