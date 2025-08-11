using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_135Ctrl : BaseDragController<L135_AnswerChoice>
{
    public int correctedAnswerCount;
    public Transform hand;
    public Transform mask;
    public List<L135_Point> lsPoints;
    public List<L135_AnswerChoice> lsAnswerChoices;
    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckToCorrectPosition())
        {
            if(correctedAnswerCount == lsAnswerChoices.Count)
            {
                isWin = true;
                AnimationHand();
            }
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {

    }

    void AnimationHand()
    {
        var targetMove = new Vector2(1.64f,2.91f);
        hand.DOMove(targetMove, 1f).SetEase(Ease.Linear).OnComplete(delegate
        {
            mask.DOMoveX(5f,1f).SetEase(Ease.Linear);
            hand.DOMoveX(5f,1f).SetEase(Ease.Linear);
            StartCoroutine(HandleWinCondition());
        });
    }
    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }


    [Button("Setup",ButtonSizes.Large)]
    void Setup()
    {
        for(int i = 0; i < 3; i++)
        {
            lsAnswerChoices[i].objCollider = lsAnswerChoices[i].transform.GetComponent<BoxCollider2D>();
            lsAnswerChoices[i].id = i;
            lsPoints[i].id = i;
            lsPoints[i].transform.position = lsAnswerChoices[i].transform.position;
            lsAnswerChoices[i].levelCtrl = this;
        }
    }
}
