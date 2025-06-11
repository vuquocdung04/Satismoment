using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_65Ctrl : BaseDragController<L65_Planet>
{
    public Transform grid;
    public int winProgress;
    protected override void OnDragEnded()
    {
        float distance = Vector2.Distance(draggableComponent.transform.position, draggableComponent.posCorrect);
        if (distance < 0.5f)
        {
            draggableComponent.transform.position = draggableComponent.posCorrect;
            draggableComponent.transform.SetParent(grid);
            draggableComponent._collider2d.enabled = false;
            winProgress++;
        }
        else
        {
            draggableComponent.DoMovingDefaultPos();
        }

        StartCoroutine(HandleWinCodition());
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {

    }

    IEnumerator HandleWinCodition()
    {
        if (winProgress == 5)
        {
            isWin = true;
            Tween rotateRid =  grid.DORotate(new Vector3(0,0,-45f),2f,RotateMode.Fast);
            yield return rotateRid.WaitForCompletion();
            yield return new WaitForSeconds(0.2f);
            WinBox.SetUp().Show();
        }
    }
}
