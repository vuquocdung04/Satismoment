using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using Unity.VisualScripting;
using UnityEngine;

public class Level_99Ctrl : BaseDragController<L99_Item>
{
    public int winProgress;
    public Sprite coffee_Filter_Holder;
    public Sprite cupCaffee;
    public L99_CaffeeDrop caffeeDrop;
    public List<L99_Item> lsItems;
    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckPositionCorrect())
        {
            winProgress++;
            if (winProgress == lsItems.Count)
                StartCoroutine(HandleWinCondition());
        }
        else
        {
            draggableComponent.OnEndDrag();

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

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        lsItems[6].spriteRenderer.sprite = coffee_Filter_Holder;
        lsItems[7].spriteRenderer.sprite = cupCaffee;
        caffeeDrop.transform.DOMoveY(0f,0.3f).SetEase(Ease.Linear);
        caffeeDrop.StartAnimation();
        yield return new WaitUntil(()=> caffeeDrop.isCompleteAnimated);
        yield return new WaitForSeconds(0.4f);
        WinBox.SetUp().Show();
    }

    [Button("Setup Item After",ButtonSizes.Large)]
    void SetupItemAfter()
    {
        foreach (L99_Item item in lsItems)
        {
            item.InitCorrect();
        }
    }
    [Button("Setup Item Before",ButtonSizes.Large)]
    void SetupItemBefore()
    {
        foreach (L99_Item item in lsItems)
        {
            item.InitDefault();
        }
    }

}
