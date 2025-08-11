using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_119Ctrl : BaseDragController<L119_Tissue>
{
    public int tissueDropped; // Số hạt dưa đã bỏ\
    public int totalTissuesNeeded = 15;
    public L119_Tissue tissuePrefab;
    public L119_TissueBox tissueBox;
    public Transform posSpawn;

    private void Start()
    {
        SpawnTissue();
    }
    protected override void OnDragEnded()
    {
        if (draggableComponent.HasProperlyDraggedTissueOut(posSpawn))
        {
            IncreaseTissueDropped();
            if (CheckWin())
            {
                isWin = true;
                StartCoroutine(HandleWinCondition());
            }
            else
            {
                SpawnTissue();
            }
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {

    }
    public void IncreaseTissueDropped()
    {
        tissueDropped++;

        if (tissueDropped == 5 || tissueDropped == 10 || tissueDropped == 12 || tissueDropped == 15)
        {
            tissueBox.ChangeSprite();
        }

        // Nếu muốn thêm hiệu ứng hoặc log
        Debug.Log($"Tissue dropped: {tissueDropped}");
    }

    bool CheckWin()
    {
        if (tissueDropped == totalTissuesNeeded) return true;
        return false;
    }

    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }

    public void SpawnTissue()
    {
        var tissueClone = SimplePool2.Spawn(tissuePrefab, posSpawn.position, Quaternion.identity);
        tissueClone.transform.localScale = Vector3.zero;
        tissueClone.transform.DOScale(Vector3.one, 0.3f);
        tissueClone.transform.SetParent(transform);
        tissueClone.Init();
    }
}
