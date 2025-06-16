using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_74Ctrl : BaseDragController<Transform>
{
    public L74_CardReader cardReader;
    public Transform slidingGlassDoor;
    protected override void OnDragEnded()
    {
        float distance = Vector2.Distance(draggableComponent.transform.position, cardReader.transform.position);
        if(distance < 0.5f)
        {
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

    }

    Tween DoMovingDoor()
    {
         return slidingGlassDoor.DOMoveX(4.61f,0.5f).SetEase(Ease.Linear);
    }

    IEnumerator HandleWinCodition()
    {
        isWin =true;
        cardReader.ChangeSpriteLed();
        Tween moveDoor = DoMovingDoor();
        yield return moveDoor.WaitForCompletion();
        yield return new WaitForSeconds(0.3f);
        WinBox.SetUp().Show();
    }
}
