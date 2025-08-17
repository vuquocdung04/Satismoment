using DG.Tweening;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_44Ctrl : BaseDragController<L44_Clue>
{
    public int winProgress;
    protected override void OnDragEnded()
    {
        CheckDistanceDraggedComponent();
        StartCoroutine(HandleWinCodition());
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        GameController.Instance.musicManager.PlayPickItemSound();
        draggableComponent.RotateToZero();

    }

    void CheckDistanceDraggedComponent()
    {
        if(draggableComponent.GetDistance() < 0.3f)
        {
            winProgress++;
            GameController.Instance.musicManager.PlayPlaceItemSoundTrue();
            draggableComponent.transform.position = draggableComponent.pointCorrect.localPosition;
            draggableComponent._collider.enabled = false;
            draggableComponent.transform.DOShakePosition(0.5f, 0.1f, vibrato: 10, randomness: 90, snapping: false, fadeOut: true);
        }
        else
        {
            draggableComponent.SnapBackPostion(Ease.Flash);
            draggableComponent.RotateAngleDefault(RotateMode.Fast);
        }
    }

    System.Collections.IEnumerator HandleWinCodition()
    {
        if(winProgress > 3)
        {
            yield return new WaitForSeconds(0.5f);
            WinBox.SetUp().Show();
        }
    }
}
