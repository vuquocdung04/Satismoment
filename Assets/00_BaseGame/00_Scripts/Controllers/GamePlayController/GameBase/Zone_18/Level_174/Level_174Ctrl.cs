using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_174Ctrl : BaseDragController<L174_Item>
{
    bool checkDoneState1 = false;
    public L174_Item tem;
    public L174_Item letter;
    protected override void OnDragEnded()
    {
        CheckCoveredItems();
        draggableComponent.CheckCorrectToPosition(this);
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
    }

    private void CheckCoveredItems()
    {
        if (checkDoneState1) return;
        if (letter.objRenderer.sortingOrder > tem.objRenderer.sortingOrder)
        {
            if (letter.objRenderer.bounds.Intersects(tem.objRenderer.bounds))
            {
                checkDoneState1 = false;
            }
            else
            {
                checkDoneState1 = true;
                tem.objRenderer.sortingOrder = 5;
                tem.objCollider.enabled = true;
            }
        }
    }

    public IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }
}
