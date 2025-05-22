using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_28Ctrl : BaseDragController<L28_Bottle>
{
    public int winProgress = 0;
    public float snapDistance = 0.4f;
    public float durationAnim = 2f;
    public Transform parent;
    public Transform hand;
    public Vector2 handPosDefault;
    public List<Transform> lsObjCosts;
    public List<Transform> lsPosSetupCosts;
    public List<L28_CategoryBottle> lsCategorys;

    protected override void OnDragStarted()
    {
        base.OnDragStarted();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        if(!draggableComponent.isMove)
         draggableComponent.transform.position += deltaMousePosition;
    }

    protected override void OnDragEnded()
    {
        base.OnDragEnded();

        L28_Bottle draggedBottle = draggableComponent;
        L28_Bottle closesBottleToSwapWith = null;
        float shortestDistanceFound = snapDistance;

        foreach(var category in this.lsCategorys)
        {
            foreach (var otherBottle in category.lsBottles)
            {
                if (otherBottle == draggedBottle) continue;

                float distance = Vector2.Distance(
                    new Vector2(draggedBottle.transform.position.x, draggedBottle.transform.position.y),
                    new Vector2(otherBottle.transform.position.x, otherBottle.transform.position.y)
                );

                if (distance < shortestDistanceFound)
                {
                    shortestDistanceFound = distance;
                    closesBottleToSwapWith = otherBottle;
                }
            }
        }

        if (closesBottleToSwapWith != null && !closesBottleToSwapWith.isMove)
        {
            HandleSwapAndCheckWin(draggedBottle, closesBottleToSwapWith);
        }
        else
        {
            draggedBottle.transform.DOMove(draggedBottle.posDefault, 0.3f);
        }

        CheckOverallWinConditionAndAnimate();
    }

    void HandleSwapAndCheckWin(L28_Bottle bottleA, L28_Bottle bottleB)
    {
        lsCategorys[bottleA.idCategory].lsBottles.Remove(bottleA);
        lsCategorys[bottleB.idCategory].lsBottles.Remove(bottleB);

        lsCategorys[bottleA.idCategory].lsBottles.Add(bottleB);
        lsCategorys[bottleB.idCategory].lsBottles.Add(bottleA);


        int idCateA = bottleB.idCategory;
        int idCateB = bottleA.idCategory;

        Vector2 slotPosForA_target = bottleB.posDefault;
        Vector2 slotPosForB_target = bottleA.posDefault;

        //swap pos
        bottleA.posDefault = slotPosForA_target;
        bottleB.posDefault = slotPosForB_target;

        //swap id
        bottleA.idCategory = idCateA;
        bottleB.idCategory = idCateB;

        bottleA.transform.position = slotPosForA_target;

        bottleB.transform.DOMove(slotPosForB_target, 0.4f);

        HandleWinCondition(lsCategorys[bottleA.idCategory].lsBottles, lsCategorys[bottleB.idCategory].lsBottles);
    }

    void HandleWinCondition(List<L28_Bottle> lsBottleA, List<L28_Bottle> lsBottleB)
    {
        bool allSameInA = true;
        int fistBottleA = lsBottleA[0].idBottle;
        for(int i = 1; i < lsBottleA.Count; i++)
        {
            if (lsBottleA[i].idBottle != fistBottleA)
            {
                allSameInA = false;
                break;
            }
        }

        if (allSameInA)
        {
            winProgress++;
            foreach (var bottle in lsBottleA) bottle.isMove = true;
        }

        bool allSameInB = true;
        int fistBottleB = lsBottleB[0].idBottle;

        for (int i = 1; i < lsBottleB.Count; i++)
        {
            if (lsBottleB[i].idBottle != fistBottleB)
            {
                allSameInB = false;
                break;
            }
        }

        if (allSameInB)
        {
            winProgress++;
            foreach (var bottle in lsBottleB) bottle.isMove = true;
        }

    }

    void CheckOverallWinConditionAndAnimate()
    {
        if (winProgress < 4) return;

        isWin = true;
        Sequence handSequense = DOTween.Sequence();

        for(int i = 0; i < lsPosSetupCosts.Count; i++)
        {
            int index = i;
            handSequense.Append(hand.DOMove(lsPosSetupCosts[i].transform.position, durationAnim).SetEase(Ease.InQuad));
            handSequense.AppendCallback(delegate
            {
                lsObjCosts[index].SetParent(parent);
                lsObjCosts[index].position = lsPosSetupCosts[index].position;
            });
            handSequense.Append(hand.DOMove(handPosDefault, durationAnim).SetEase(Ease.OutCubic));
        }

        handSequense.OnComplete(() => WinBox.SetUp().Show());
    }

    [Button("setup", ButtonSizes.Large)]
    void Setup()
    {
        int index = 0;
        foreach(var category in this.lsCategorys)
        {
            foreach(var bottle in category.lsBottles)
            {
                bottle.idCategory = index;
            }
            index++;
        }

        handPosDefault = hand.transform.position;
    }
    
}

[System.Serializable]
public class L28_CategoryBottle
{
    public List<L28_Bottle> lsBottles;
}