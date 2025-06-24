using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_85Ctrl : BaseDragController<L85_LearningTool>
{
    public int winProgress = 0;
    public List<L85_LearningTool> lsTools;

    protected override void OnDragEnded()
    {
        float distance = Vector2.Distance(draggableComponent.transform.position, draggableComponent.posCorrect);
        if(distance < 0.2f)
        {
            winProgress++;
            draggableComponent.HandleCorrectCondition();
            if(winProgress == lsTools.Count)
            {
                StartCoroutine(HandleWinCondition());
            }
        }
        else
        {
            draggableComponent.OnEndDrag();
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }

    [Button("Setup After", ButtonSizes.Large)]
    void SetupAfter()
    {
        foreach(var tool in this.lsTools)
        {
            tool.InitAfter();
        }
    }
    [Button("Setup Before", ButtonSizes.Large)]
    void SetupBefore()
    {
        foreach(var tool in this.lsTools)
        {
            tool.InitBefore();
        }
    }
}
