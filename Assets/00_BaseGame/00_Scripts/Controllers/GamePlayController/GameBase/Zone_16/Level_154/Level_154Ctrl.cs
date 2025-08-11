using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_154Ctrl : BaseDragController<L154_SpiceShaker>
{
    public Transform plate;
    public SpriteRenderer beefCooked;
    public Transform fryingPan;
    public int spiceCount;
    public L154_Oil oil;
    public L154_Smoke smokePrefab;
    protected override void OnDragEnded()
    {
        draggableComponent.ChangeSpriteDefault();
        if (draggableComponent.isMaxSpiceReached)
        {
            draggableComponent.MoveWhenComplete();
            spiceCount++;
            if(spiceCount == 2)
            {
                StartCoroutine(HandleNextState());
            }
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.MoveX(mouseDelta.x);
    }

    protected override void OnDragStarted()
    {
        draggableComponent.ChangeSpriteShaker();
    }

    IEnumerator HandleNextState()
    { 
        Tween plateMove = plate.DOMoveY(2.5f,0.5f).SetEase(Ease.Linear);
        yield return plateMove.WaitForCompletion();
        Tween fryingPanMove = fryingPan.DOMoveX(0,0.5f).SetEase(Ease.Linear);
        yield return fryingPanMove.WaitForCompletion();
        oil.StartAnimation();
        StartCoroutine(SpawnSmokeEffect());
        yield return new WaitForSeconds(1f);
        Tween beefFade = beefCooked.DOFade(1f,1f).SetEase(Ease.Linear);
        yield return beefFade.WaitForCompletion();
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
        isWin = true;
    }

    IEnumerator SpawnSmokeEffect()
    {
        var waitTime = new WaitForSeconds(0.2f);
        Vector2 randPos;
        while (!isWin)
        {
            for (int i = 0; i < 5; i++)
            {
                randPos = beefCooked.transform.localPosition + new Vector3(Random.Range(-1f,1f), Random.Range(0,0.5f));
                var smokeClone = SimplePool2.Spawn(smokePrefab, randPos, Quaternion.identity);
                smokeClone.InitState();
                yield return waitTime;
            }
        }
    }
}
