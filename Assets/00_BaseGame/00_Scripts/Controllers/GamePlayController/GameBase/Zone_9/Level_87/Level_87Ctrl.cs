using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_87Ctrl : BaseDragController<L87_Stick>
{
    public int winProgress = 0;
    public Sprite stickSprite;
    public L87_Buffterfly buffterfly;
    public List<L87_Stick> lsSticks;
    protected override void OnDragEnded()
    {
        float distance = Vector2.Distance(draggableComponent.transform.position, draggableComponent.posCorrect);
        if(distance < 0.3f)
        {
            draggableComponent.HandleCorrectCondition();
            winProgress++;

            if(winProgress == lsSticks.Count)
            {
                StartCoroutine(HandleWinCondition());
            }
        }
        else
        {
            draggableComponent.OnEndDrag();
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
    }

    void AnimateBuffterfly()
    {

    }


    IEnumerator HandleWinCondition()
    {
        isWin = true;
        buffterfly.gameObject.SetActive(true);
        lsSticks[0].spriteRenderer.sprite = stickSprite;
        buffterfly.DoFlying();
        yield return new WaitUntil(()=>buffterfly.reachedEnd);
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }


    [Button("Test game", ButtonSizes.Large)]
    void Test()
    {
        buffterfly.DoFlying();
    }


    [Button("Setup After", ButtonSizes.Large)]
    void SetupAfter()
    {
        foreach(var stick in this.lsSticks)
        {
            stick.InitCorrect();
        }
    }

    [Button("Setup before", ButtonSizes.Large)]
    void SetupBefore()
    {
        foreach(var stick in this.lsSticks)
        {
            stick.InitDefault();
        }
    }
}
