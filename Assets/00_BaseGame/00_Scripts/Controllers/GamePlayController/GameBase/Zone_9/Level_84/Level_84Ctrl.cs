using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_84Ctrl : BaseDragController<L84_Food>
{
    public int winProgress;
    public List<L84_Food> lsFoods;

    protected override void OnDragEnded()
    {
        float distance = Vector2.Distance(draggableComponent.transform.position, draggableComponent.posCorrect);
        if(distance < 0.3f)
        {
            GameController.Instance.musicManager.PlayPlace();
            winProgress++;
            draggableComponent.HandleConditionCorrect();
            if(winProgress == lsFoods.Count)
            {
                StartCoroutine(HandleWinCondition());
            }
            
        }
        else
        {
            GameController.Instance.musicManager.PlayWrong();
            draggableComponent.StateEndDrag();
        }

        

    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.StateStartDrag();
        GameController.Instance.musicManager.PlayPick();
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }

    [Button("Setup Food", ButtonSizes.Large)]
    void SetupFood()
    {
        foreach(var food in this.lsFoods)
        {
            food.posCorrect = food.transform.position;
            food.angle = food.transform.eulerAngles.z;
            food.spriteRenderer = food.transform.GetComponent<SpriteRenderer>();
            food.boxCollider2D = food.transform.GetComponent<BoxCollider2D>();
            food.orderIndex = food.spriteRenderer.sortingOrder;
        }
    }




}
