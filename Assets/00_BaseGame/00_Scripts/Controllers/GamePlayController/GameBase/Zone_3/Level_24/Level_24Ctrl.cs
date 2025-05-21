using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_24Ctrl : BaseDragController<Transform>
{
    public Transform toiletPaperRoll;
    public Transform toiletPaper;
    public Transform cardBoardTube;
    public float posStartToiletPaper = 5;
    Vector3 newPos;
    float t;
    float scalePaperRoll;

    protected override void OnDragStarted()
    {
        base.OnDragStarted();
        
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        newPos = draggableComponent.transform.position;
        newPos.y += deltaMousePosition.y;
        newPos.x = 0;

        draggableComponent.transform.position = newPos;

        t = (posStartToiletPaper - draggableComponent.transform.position.y) /10;
        scalePaperRoll = 1.25f + t * (0.75f - 1.25f);

        toiletPaperRoll.transform.localScale = new Vector3(1, scalePaperRoll, 1);

        if(toiletPaperRoll.transform.localScale.y < 0.77f)
        {
            StartCoroutine(HandleWin());
            isWin = true;
        }
    }

    protected override void OnDragEnded()
    {
        base.OnDragEnded();
    }


    IEnumerator HandleWin()
    {
        toiletPaperRoll.gameObject.SetActive(false);
        toiletPaper.transform.DOMoveY(-16f,2f);
        cardBoardTube.DOJump(
            cardBoardTube.position,
            0.3f,
            1,
            0.7f
            ).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(0.7f);
        WinBox.SetUp().Show();

    }
}
