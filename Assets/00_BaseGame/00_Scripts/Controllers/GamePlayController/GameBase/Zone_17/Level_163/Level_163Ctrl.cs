using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_163Ctrl : BaseDragController<L163_PumpHandle>
{
    public Transform bike;
    public SpriteRenderer wheel;
    public Sprite wheelDone;
    public int amountPump = 0;
    public bool canPump;
    protected override void OnDragEnded()
    {
        draggableComponent.OnDragEnd(this);
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.Pumping(mouseDelta.y, this);
        if(amountPump == 5)
        {
            wheel.sprite = wheelDone;
            bike.DOJump(bike.position + new Vector3(0,0.07f,0),0.2f,1,0.3f);
            isWin = true;
            StartCoroutine(HandleWinCondition());
        }
    }

    protected override void OnDragStarted()
    {
        
    }

    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}
