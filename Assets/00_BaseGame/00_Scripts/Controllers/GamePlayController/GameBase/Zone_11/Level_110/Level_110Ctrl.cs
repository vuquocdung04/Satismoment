using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_110Ctrl : BaseDragController<L110_Diem>
{
    public L110_Fire firePrefab;
    public int winProgress = 0;
    protected override void OnDragEnded()
    {
        if (CheckWin()) isWin = true;
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        
    }
    bool CheckWin()
    {
        if(winProgress == 4)
        {
            StartCoroutine(HandleWinCondition());
            return true;
        }
        return false;
    }

    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }
    public L110_Fire SpawnFire(Vector3 scaleAmount)
    {
        var fireClone = Instantiate(firePrefab);
        fireClone.PlayingAnim();
        fireClone.transform.localScale = scaleAmount;
        winProgress++;
        return fireClone;
    }
}
