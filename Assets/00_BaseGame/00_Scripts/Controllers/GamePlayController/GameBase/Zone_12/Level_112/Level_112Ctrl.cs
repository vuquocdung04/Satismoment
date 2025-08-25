using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_112Ctrl : BaseDragController<L112_Item>
{
    public L112_Beam beam;
    public List<L112_Item> lsItems;
    protected override void OnDragEnded()
    {
        draggableComponent.OnDragEnded();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.OnDragLogic(mouseDelta);
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnDragStarted();
        GameController.Instance.musicManager.PlayPick();
    }

    public IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }

    [Button("Setup Item", ButtonSizes.Large)]
    void SetupItem()
    {
        foreach (var item in this.lsItems) item.Init();
    }
}
