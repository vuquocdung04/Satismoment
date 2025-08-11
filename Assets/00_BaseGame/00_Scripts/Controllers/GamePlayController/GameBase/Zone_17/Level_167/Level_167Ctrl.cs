using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_167Ctrl : BaseDragController<L167_Eraser>
{
    public L167_Shaving earserShavingPrefab;
    private float lastApplyTime;
    public float applyInterval = 0.05f;
    protected override void OnDragEnded()
    {
        draggableComponent.objRenderer.sprite = draggableComponent.spriteOffDrag;
        if (draggableComponent.CheckDrawingCoverage())
        {
            isWin = true;
            StartCoroutine(HandleWinCondition());
        }
    }

    float lastShavingTime;
    public float shavingInterval;
    bool painted;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;

        // Kiểm tra thao tác vẽ có thực sự làm lộ mask không
        painted = draggableComponent.DrawAtPosition(draggableComponent.transform.position);

        if (Time.time - lastApplyTime > applyInterval)
        {
            draggableComponent.ApplyMaskChanges();
            lastApplyTime = Time.time;

            // Sinh hiệu ứng shaving NẾU VÀ CHỈ NẾU vừa thực sự vẽ lên mask
            if (painted && Time.time - lastShavingTime > shavingInterval)
            {
                var shavingClone = SimplePool2.Spawn(earserShavingPrefab, draggableComponent.transform.position, Quaternion.identity);
                shavingClone.InitState();
                lastShavingTime = Time.time;
            }
        }
    }


    protected override void OnDragStarted()
    {
        draggableComponent.objRenderer.sprite = draggableComponent.spriteOnDrag;

    }

    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}
