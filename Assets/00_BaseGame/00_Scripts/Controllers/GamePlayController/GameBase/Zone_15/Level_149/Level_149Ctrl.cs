using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_149Ctrl : BaseDragControllerVer2<L149_Fruit>
{
    public List<L149_Hand> lsHands;
    protected override void OnDragEnded()
    {
        draggableComponent.HandleCorrectPosition(this);
        if(winProgress == lsHands.Count)
        {
            StartCoroutine(HandleWinCondition());
        }
        PullAllHand();

    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
        ReachAllHand();
    }

    void ReachAllHand()
    {
        foreach(var hand in this.lsHands)
        {
            hand.ReachOut();
        }
    }

    void PullAllHand()
    {
        foreach (var hand in this.lsHands)
        {
            hand.PullBack();
        }
    }


    public L149_Hand GetHandById(int id )
    {
        foreach(var hand in this.lsHands) if(hand.id == id) return hand;
        return null;
    }


    protected override void SetupComponent_PositionCorrect()
    {
        for(int i = 0; i < lsT_ItemDragables.Count; i++)
        {
            lsT_ItemDragables[i].InitCorrect();
            lsHands[i].targetPosition = lsHands[i].transform.position;
        }
    }

    protected override void SetupPositionDefault()
    {
        for (int i = 0; i < lsT_ItemDragables.Count; i++)
        {
            lsT_ItemDragables[i].InitDefault();
            lsHands[i].originalPosition = lsHands[i].transform.position;
            lsHands[i].objCollider = lsHands[i].transform.GetComponentInChildren<CircleCollider2D>();
            lsHands[i].id = i;
            lsT_ItemDragables[i].id = i;
        }
    }
}
