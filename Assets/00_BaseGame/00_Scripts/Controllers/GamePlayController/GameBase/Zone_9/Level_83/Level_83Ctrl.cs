using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_83Ctrl : BaseDragController<L83_GlassCleaner>
{
    public Transform effectGlass;
    public  bool isCompleteLevel = false;
    public List<Transform> lsEffects;
    protected override void OnDragEnded()
    {
        if (isCompleteLevel)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    Vector3 pointDraw = new Vector3(0,1.3f,0);
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
        draggableComponent.DrawAtPosition(draggableComponent.transform.position + pointDraw);
    }

    protected override void OnDragStarted()
    {

    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        effectGlass.DOMoveX(-4,0.5f).SetEase(Ease.Linear);
        Sequence sequence = DOTween.Sequence();

        foreach (var effect in lsEffects)
        {
            sequence.Join(effect.DOScale(Vector3.one, 1f).SetEase(Ease.Linear));
        }

        yield return sequence.WaitForCompletion();
        yield return new WaitForSeconds(0.4f);

        WinBox.SetUp().Show();
    }
}
