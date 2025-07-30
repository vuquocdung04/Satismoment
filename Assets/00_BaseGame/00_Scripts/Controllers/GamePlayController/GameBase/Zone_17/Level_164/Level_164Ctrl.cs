using Sirenix.OdinInspector;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Level_164Ctrl : BaseDragController<L164_CornHusk>
{
    public int progressWin = 0;
    public Transform holdCorn;
    public L164_CornKernel kernelPrefabs;
    public List<L164_CornHusk> lsCornHusks;
    public List<Vector2> lsPointSpawnCornKernels;
    public List<L164_CornKernel> lsCornKernels;

    private void Start()
    {
        InitializeColliders();
        InitCornKernel();
    }

    void InitCornKernel()
    {
        for(int i = 0; i < lsPointSpawnCornKernels.Count; i++)
        {
            var corne = Instantiate(kernelPrefabs, lsPointSpawnCornKernels[i], Quaternion.identity);
            corne.levelCtrl = this;
            corne.transform.SetParent(holdCorn);
            lsCornKernels.Add(corne);
        }
    }

    private void InitializeColliders()
    {
        if (lsCornHusks == null || lsCornHusks.Count == 0)
            return;

        for (int i = 0; i < lsCornHusks.Count; i++)
        {
            if (lsCornHusks[i].boxCollider != null)
                lsCornHusks[i].boxCollider.enabled = (i == lsCornHusks.Count - 1);
        }
    }

    protected override void OnDragStarted()
    {
        draggableComponent.StartHold();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.UpdateHold(Time.deltaTime);
    }

    protected override void OnDragEnded()
    {
        if (draggableComponent == null) return;

        draggableComponent.EndHold();

        // Nếu thằng vừa drop, kích hoạt collider lá trước đó
        if (draggableComponent.IsDropped())
        {
            int droppedIndex = lsCornHusks.IndexOf(draggableComponent);
            if (droppedIndex > 0)
            {
                // Tắt collider của thằng vừa drop
                draggableComponent.boxCollider.enabled = false;

                // Bật collider thằng đứng trước
                L164_CornHusk nextCornHusk = lsCornHusks[droppedIndex - 1];
                if (nextCornHusk.boxCollider != null)
                    nextCornHusk.boxCollider.enabled = true;
            }
            else
            {
                // Đây là thằng cuối cùng của list rồi, tắt collider luôn
                draggableComponent.boxCollider.enabled = false;
            }
        }
    }

    public void CheckWin()
    {
        progressWin++;
        if(progressWin == lsCornKernels.Count)
        {
            isWin = true;
            Debug.LogError("Win");
            StartCoroutine(HandleWinCondition());
        }
    }

    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }


    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        lsCornKernels.Clear();
        foreach(var cornhusk in this.lsCornHusks)
        {
            cornhusk.boxCollider = cornhusk.transform.GetComponent<BoxCollider2D>();
        }
    }
}
