using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_44Ctrl : BaseDragController<L44_Clue>
{
    public int winProgress;
    protected override void OnDragEnded()
    {
        CheckDistanceDraggedComponent();
        HandleWinCodition();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.RotateToZero();

    }

    void CheckDistanceDraggedComponent()
    {
        if(draggableComponent.GetDistance() < 0.3f)
        {
            winProgress++;
            draggableComponent.transform.position = draggableComponent.pointCorrect.localPosition;
            draggableComponent._collider.enabled = false;
            draggableComponent.transform.DOShakePosition(0.5f, 0.1f, vibrato: 10, randomness: 90, snapping: false, fadeOut: true);
        }
        else
        {
            draggableComponent.SnapBackPostion(Ease.Flash);
            draggableComponent.SnapBackRotation(RotateMode.Fast);
        }
    }

    void HandleWinCodition()
    {
        if(winProgress > 3)
        {
            WinBox.SetUp().Show();
        }
    }
}
