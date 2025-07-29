using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_162Ctrl : BaseDragController<L162_Item>
{
    public List<L162_Item> lsItems;
    public List<L162_AdhesiveHook> lsAdhesiveHooks;
    protected override void OnDragEnded()
    {
        bool foundValidPosition = false;

        foreach (var hook in this.lsAdhesiveHooks)
        {
            float distance = Vector2.Distance(draggableComponent.transform.position, hook.transform.position);
            if (distance < 0.4f)
            {
                Debug.LogError("Found valid hook position");
                draggableComponent.transform.position = hook.transform.position;
                draggableComponent.spriteRenderer.sortingOrder = draggableComponent.orderIndex;
                draggableComponent.curHook = hook;
                foundValidPosition = true;
                break; // Tìm thấy vị trí hợp lệ, thoát khỏi vòng lặp
            }
        }
        if (!foundValidPosition)
        {
            draggableComponent.OnEndDrag();
        }

        if (CheckWinCondition())
        {
            StartCoroutine(HandleWinCondition());
        }

    }


    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
    }

     bool CheckWinCondition()
    {
        foreach(var item in this.lsItems)
        {
            if (item.curHook == null) return false;
            if (item.id != item.curHook.id) return false;
        }
        return true;
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }


    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        foreach(var item in this.lsItems)
        {
            item.InitAfter();
            item.InitBefore();
        }
    }
}
