using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_157Ctrl : BaseDragController<L157_Cork>
{
    public L157_Effect effectPrefab;
    protected override void OnDragEnded()
    {
        
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.MovingY(mouseDelta.y);
        draggableComponent.CheckCorkOpened(delegate
        {
            StartCoroutine(SpawnEffect());
            Debug.LogError("Wtf");
        });
    }

    protected override void OnDragStarted()
    {
        
    }
    IEnumerator SpawnEffect()
    {
        var waitTime1 = new WaitForSeconds(0.05f);
        StartCoroutine(DebugAfter5Seconds());

        while (!isWin)
        {
            for (int i = 0; i < 20; i++)
            {
                var effectClone = SimplePool2.Spawn(effectPrefab, Vector3.zero, Quaternion.identity);
                effectClone.InitState();
                yield return waitTime1;
            }
        }
    }

    IEnumerator DebugAfter5Seconds()
    {
        yield return new WaitForSeconds(5f); 
        isWin = true;
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }
}
