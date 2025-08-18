
using System.Collections;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_60Ctrl : BaseDragController<L60_Picture>
{
    public int winProgress;
    protected override void OnDragEnded()
    {
        float distance = Vector2.Distance(draggableComponent.transform.position, draggableComponent.posCorrect);
        if (distance < 0.5f)
        {
            draggableComponent.transform.position = draggableComponent.posCorrect;
            draggableComponent._collider2d.enabled = false;
            winProgress++;
            GameController.Instance.musicManager.PlayPlaceItemSoundTrue();
        }
        else
        {
            GameController.Instance.musicManager.PlayPlaceItemSoundFalse();
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
        GameController.Instance.musicManager.PlayPickItemSound();
    }

    IEnumerator HandleWinCodition()
    {
        if(winProgress == 7)
        {
            isWin = true;
            yield return new WaitForSeconds(0.5f);
            WinBox.SetUp().Show();
            
        }
    }

}
