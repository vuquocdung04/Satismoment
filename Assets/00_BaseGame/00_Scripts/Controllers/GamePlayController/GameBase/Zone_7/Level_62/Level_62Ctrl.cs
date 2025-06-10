using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_62Ctrl : BaseDragController<L62_Food>
{
    public Transform foodContainerLid;
    public int winProgress;
    public List<L62_Food> lsFoods;
    protected override void OnDragEnded()
    {
        float distance = Vector2.Distance(draggableComponent.transform.position, draggableComponent.posCorrect);
        if (distance < 0.5f)
        {
            draggableComponent.transform.position = draggableComponent.posCorrect;
            draggableComponent._collider2d.enabled = false;
            winProgress++;
        }

        StartCoroutine(HandleWinCodition());
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.transform.SetParent(transform);
    }

    IEnumerator HandleWinCodition()
    {
        if (winProgress == 5)
        {
            isWin = true;
            Tween moveLid = foodContainerLid.DOMoveY(-1.37f, 1f).SetEase(Ease.InBack);
            yield return moveLid.WaitForCompletion();
            yield return new WaitForSeconds(0.1f);
            WinBox.SetUp().Show();
        }
    }

    [Button("Setup", ButtonSizes.Large)]
    void SetupFood()
    {
        foreach(var food in this.lsFoods)
        {
            food.posCorrect = food.transform.position;
            food._collider2d = food.transform.GetComponent<BoxCollider2D>();
        }
    }
}
