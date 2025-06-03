using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_51Ctrl : BaseDragController<L51_Bush>
{
    public L51_Panda panda;
    public int winProgress;
    private bool isMove;
    protected override void OnDragEnded()
    {
        isMove = false;
        StartCoroutine(HandleWinCodition());
    }

    Vector3 newPos;
    float distance;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        if (isMove) return;
        newPos = draggableComponent.transform.position;
        newPos.x += deltaMousePosition.x;
        newPos.y = draggableComponent.transform.position.y;
        draggableComponent.transform.position = newPos;
        distance = Vector2.Distance(draggableComponent.transform.position, draggableComponent.posDefault);
        if(distance > 1.3f)
        {
            isMove = true;
            winProgress++;
            switch (draggableComponent.isDirectionLeft)
            {
                case true:
                    draggableComponent.transform.DOMoveX(-6f, 1.5f).SetEase(Ease.InOutQuad);
                    break;
                case false:
                    draggableComponent.transform.DOMoveX(6f, 1.5f).SetEase(Ease.InOutQuad);
                    break;
            }
        }
    }

    protected override void OnDragStarted()
    {

    }

    IEnumerator HandleWinCodition()
    {
        if(winProgress > 2)
        {
            isWin = true;
            panda.HandleWinAnimation();
            yield return new WaitForSeconds(1.2f);
            WinBox.SetUp().Show();
        }
    }

}
