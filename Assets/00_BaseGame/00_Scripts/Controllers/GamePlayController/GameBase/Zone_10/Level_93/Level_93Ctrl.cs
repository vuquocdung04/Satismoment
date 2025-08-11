using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_93Ctrl : BaseDragController<L93_FakeMoveParent>
{
    public int winProgress;
    public List<L93_FakeMoveParent> lsFakes;
    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckDistanceCorrect())
        {
            winProgress++;
            if (winProgress == lsFakes.Count)
                StartCoroutine(HandleWinCondition());
        }
    }

    Vector3 newPos;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        newPos = draggableComponent.transform.localPosition + new Vector3(mouseDelta.x,0,0);
        newPos.x = Mathf.Clamp(newPos.x, -draggableComponent.maxDistanceX/2,draggableComponent.maxDistanceX/2);
        draggableComponent.transform.localPosition = newPos;
    }

    protected override void OnDragStarted()
    {

    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.3f);
        WinBox.SetUp().Show();
    }


    [Button("Setup",ButtonSizes.Large)]
    void Setup()
    {
        foreach(var child in this.lsFakes)
        {
            child.Init();
        }
    }
}
