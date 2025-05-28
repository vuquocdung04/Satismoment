using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_46Ctrl : BaseDragController<L46_Scissors>
{
    public int winProgress;
    public List<L46_ClothScrapFall> lsClothScraps;
    protected override void OnDragEnded()
    {
        draggableComponent.DoAnimation();
        foreach(var cloth in this.lsClothScraps)
        {
            if (cloth.isClear) continue;
            float distance = Vector2.Distance(
                    new Vector2(draggableComponent.transform.position.x, draggableComponent.transform.position.y),
                    new Vector2(cloth.transform.position.x, cloth.transform.position.y)
                );
            if (distance < 0.5f)
            {
                cloth.PlayFallAnimation();
                cloth.isClear = true;
                winProgress++;
                Debug.LogError("test");
            }
        }

        HandleWinCodition();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        
    }

    void HandleWinCodition()
    {
        if(winProgress == lsClothScraps.Count)
        {
            isWin=  true;
            WinBox.SetUp().Show();
        }
    }
}
