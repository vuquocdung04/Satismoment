
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_41Ctrl : BaseDragController<L41_Screw>
{
    public int winProgress;

    protected override void OnDragStarted()
    {
        GameController.Instance.musicManager.PlayPickItemSound();
    }
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.DoScalingScrew();
    }

    protected override void OnDragEnded()
    {
        if (draggableComponent.isScale)
        {
            winProgress++;
            Destroy(draggableComponent.gameObject);
        }
        HandleWinCodition();
    }

    void HandleWinCodition()
    {
        if(winProgress > 5)
        {
            WinBox.SetUp().Show();
        }
    }
}
