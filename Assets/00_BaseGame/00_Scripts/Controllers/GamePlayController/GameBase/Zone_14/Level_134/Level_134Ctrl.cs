using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_134Ctrl : BaseDragController<L134_Nozzle>
{
    public Transform carStorage;
    public int cleanedItemCount;
    private float lastApplyTime;
    public float applyInterval = 0.05f;
    public List<SpriteRenderer> lsCarCleanedCount;
    public Sprite tickCleaned;


    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckDrawingCoverage())
        {
            lsCarCleanedCount[cleanedItemCount].sprite = tickCleaned;
            cleanedItemCount++;
            if (cleanedItemCount < 3)
            {
                carInTheStorageMoving();
            }
            else
            {
                StartCoroutine(HandleWinCondition());
            }
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
        draggableComponent.DrawAtPosition(draggableComponent.transform.position + new Vector3(0.8f,1.5f,0f));
        if (Time.time - lastApplyTime > applyInterval)
        {
            draggableComponent.ApplyMaskChanges();
            lastApplyTime = Time.time;
        }

    }

    protected override void OnDragStarted()
    {
        lastApplyTime = Time.time; // Đặt lại thời gian khi bắt đầu kéo

    }

    void carInTheStorageMoving()
    {
        carStorage.DOMoveX(carStorage.transform.position.x - 5, 0.4f).SetEase(Ease.Linear);
    }
    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}
