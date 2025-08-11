using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_104Ctrl : BaseDragController<L104_SoftDrink>
{
    public int winProgress;
    public List<L104_SoftDrink> lsSoftDrinks;
    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckDistanceCorrect())
        {
            winProgress++;
            if (winProgress == lsSoftDrinks.Count)
                StartCoroutine(HandleWinCondition());
        }
    }

    float distance;
    Vector3 newPos;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        newPos = draggableComponent.icon.transform.localPosition + new Vector3(mouseDelta.x, 0);
        draggableComponent.icon.transform.localPosition = newPos;
        distance = draggableComponent.icon.transform.localPosition.x;
        if(Mathf.Abs(distance) >= draggableComponent.maxDistanceX)
        {
            draggableComponent.icon.transform.localPosition = Vector3.zero;
        }
    }

    protected override void OnDragStarted()
    {

    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return StartCoroutine(PlayAnimationLoop());
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }

    IEnumerator PlayAnimationLoop()
    {
        for (int i = 0; i < 4; i++)
        {
            foreach (var softDrink in lsSoftDrinks)
            {
                // Gọi hàm nhún icon
                TweenJumpY(softDrink.icon);

                yield return new WaitForSeconds(0.05f); // Đợi 0.2s trước khi icon tiếp theo
            }
        }

    }
    void TweenJumpY(Transform target)
    {
        float jumpPower = 0.2f;
        float duration = 0.05f;

        target.DOLocalMoveY(target.localPosition.y + jumpPower, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                target.DOLocalMoveY(target.localPosition.y - jumpPower, duration)
                    .SetEase(Ease.Linear);
            });
    }
    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        foreach (var child in this.lsSoftDrinks)
        {
            child.Init();
        }
    }
}
