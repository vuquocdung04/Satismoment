using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_156Ctrl : BaseDragController<L156_NewsPaper>
{
    public int cutCount;
    public List<L156_NewsPaper> lsNewsPapers;
    protected override void OnDragEnded()
    {
        float distance = draggableComponent.transform.position.y - 1.4f;
        if(Mathf.Abs(distance) < 0.05f && !draggableComponent.state1_Completed)
        {
            draggableComponent.state1_Completed = true;
        }
        else
        {
            draggableComponent.objRenderer.sortingOrder = 2;
        }
        if (draggableComponent.transform.position.y < -0.44f)
        {
            draggableComponent.objCollider.enabled = false;
            cutCount++;
            if (cutCount == lsNewsPapers.Count)
                StartCoroutine(HandleWinCondition());
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        if (!draggableComponent.state1_Completed)
            draggableComponent.Moving(mouseDelta);
        else
        {
            draggableComponent.MoveY(mouseDelta.y/6);
        }
    }

    protected override void OnDragStarted()
    {
        draggableComponent.objRenderer.sortingOrder += 2;
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }


    [Button("Setup",ButtonSizes.Large)]
    void Setup()
    {
        foreach(var news in this.lsNewsPapers)
        {
            news.objCollider = news.transform.GetComponent<BoxCollider2D>();
            news.objRenderer = news.transform.GetComponent<SpriteRenderer>();
        }
    }

}
