using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_177Ctrl : BaseDragController<L177_Hand>
{
    public L177_Effect effect;
    protected override void OnDragEnded()
    {
        
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.OnLogic(mouseDelta, delegate
        {
            StartCoroutine(HandleWinCondition());
        });
    }

    protected override void OnDragStarted()
    {
        
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        effect.gameObject.SetActive(true);
        effect.StartAnimation();
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
        yield return new WaitForSeconds(0.2f);
        effect.StopAnimation();
        effect.gameObject.SetActive(false);
    }

}
