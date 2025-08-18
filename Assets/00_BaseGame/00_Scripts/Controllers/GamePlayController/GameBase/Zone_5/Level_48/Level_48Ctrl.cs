using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;
public class Level_48Ctrl : BaseDragController<L48_Eraser>
{
    public AudioClip eraserBoardSound;
    public int cleanCount;

    protected override void OnDragStarted()
    {
        GameController.Instance.musicManager.PlaySingle(eraserBoardSound);
    }
    
    private int frameCount;

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
        draggableComponent.DrawAtPosition(draggableComponent.transform.position);

        frameCount++;
        if (frameCount % 3 == 0 && deltaMousePosition != Vector3.zero)
        {
            draggableComponent.ApplyMaskChanges();
            Debug.Log("Save");
        }
    }


    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckDrawingCoverage())
        {
            cleanCount++;
            if (cleanCount == draggableComponent.stages.Count)
                StartCoroutine(HandleWinCondition());
        }
    }

    System.Collections.IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}
