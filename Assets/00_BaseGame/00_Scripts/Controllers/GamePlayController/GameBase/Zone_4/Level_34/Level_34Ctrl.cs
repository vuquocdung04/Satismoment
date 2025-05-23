using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_34Ctrl : BaseDragController<L34_Lid>
{
    public float swapDistance = 0.2f;
    public List<L34_Lid> lsLids;
    public List<Vector2> lsPosWins;
    protected override void OnDragStarted()
    {
        base.OnDragStarted();
    }
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
        draggableComponent.spriteRenderer.sortingOrder = 4;
    }

    protected override void OnDragEnded()
    {
        base.OnDragEnded();
        L34_Lid draggedLid = draggableComponent;
        L34_Lid closesLidToSwapWith = null;

        float distance;
        foreach(var otherLid in this.lsLids)
        {
            if (otherLid == draggedLid) continue;

            distance = Vector2.Distance(draggedLid.transform.position, otherLid.transform.position);

            if(distance < swapDistance)
            {
                closesLidToSwapWith = otherLid;
            }
        }

        if(closesLidToSwapWith != null)
        {
            HandleSwapLid(draggedLid, closesLidToSwapWith);
        }
        else
        {
            draggedLid.transform.DOMove(draggedLid.posDefault,0.5f).SetEase(Ease.InOutQuad);
        }
        draggableComponent.spriteRenderer.sortingOrder=3;

        if (CheckWin())
        {
            WinBox.SetUp().Show();
        }
    }

    void HandleSwapLid(L34_Lid lid1, L34_Lid lid2)
    {
        Vector2 posLid1 = lid1.posDefault;
        Vector2 posLid2 = lid2.posDefault;

        lid1.posDefault = posLid2;
        lid2.posDefault = posLid1;

        lid1.transform.position = posLid2;
        lid2.transform.DOMove(posLid1, 0.5f).SetEase(Ease.InOutQuad);
    }

    bool CheckWin()
    {
        float distance = 0;
        for (int i = 0; i < lsLids.Count; i++)
        {
            distance = Vector2.Distance(lsLids[i].posDefault, lsPosWins[i]);
            if (distance < -0.2f || distance > 0.2f) return false;
        }
        return true;
    }


    [Button("Setup position Lid", ButtonSizes.Large)]
    void EditorSetUp()
    {
        foreach (var lid in this.lsLids)
        {
            lid.posDefault = lid.transform.position;
            lid.spriteRenderer = lid.transform.GetComponent<SpriteRenderer>();
        }

        
    }
}
