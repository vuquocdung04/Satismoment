using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_94Ctrl : BaseDragController<L94_PaintBucket>
{
    public int winProgress;
    public List<L94_PaintBucket> lsIcons;
    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckCorrectCondition())
        {
            winProgress++;
            GameController.Instance.musicManager.PlayPlace();
            if (winProgress == lsIcons.Count)
                StartCoroutine(HandleWinCondition());
        }
    }

    Vector2 newPos;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        newPos = draggableComponent.icon.transform.localPosition + new Vector3(mouseDelta.x,0);
        newPos.x = Mathf.Clamp(newPos.x, -draggableComponent.maxDistanceX, draggableComponent.maxDistanceX);
        draggableComponent.icon.transform.localPosition = newPos;
    }

    protected override void OnDragStarted()
    {
        GameController.Instance.musicManager.PlayPick();
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.3f);
        WinBox.SetUp().Show();
    }
    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        foreach (var child in this.lsIcons)
        {
            child.Init();
        }
    }
}
