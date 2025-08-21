using DG.Tweening;
using System.Collections;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_74Ctrl : BaseDragController<Transform>
{
    public AudioClip doorSound;
    public L74_CardReader cardReader;
    public Transform slidingGlassDoor;
    protected override void OnDragEnded()
    {
        float distance = Vector2.Distance(draggableComponent.transform.position, cardReader.transform.position);
        if(distance < 0.5f)
        {
            GameController.Instance.musicManager.PlayPlace();
            draggableComponent.transform.position = cardReader.transform.position;
            StartCoroutine(HandleWinCodition());
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        GameController.Instance.musicManager.PlayPick();
    }

    IEnumerator HandleWinCodition()
    {
        isWin =true;
        cardReader.ChangeSpriteLed();
        Tween moveDoor = slidingGlassDoor.DOMoveX(4.61f,0.5f).SetEase(Ease.Linear);
        yield return moveDoor.WaitForCompletion();
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}
