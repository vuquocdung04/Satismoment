using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L56_MoveItem : BaseDragController<L56_Item>
{
    public Transform shakerLid;
    public Level_56Ctrl levelCtrl;
    public L56_ShakerCup shakerCup;
    public SpriteRenderer icon;
    public Sprite iconCup;
    public Transform mask;
    private L56_Item curItem;
    private int winProgress = 0;
    protected override void OnDragEnded()
    {
        if (curItem._collider.IsTouching(shakerCup._collider1))
        {
            curItem.GetComponent<SpriteRenderer>().sortingOrder = 3;
            StartCoroutine(curItem.DoItemMoving());
            winProgress++;
            StartCoroutine(HandleTimingBar(winProgress));
            if (winProgress == 3)
            {
                StartCoroutine(DoMovingSharkerLid());
                shakerCup._collider1.enabled = false;
                icon.sprite = iconCup;
                mask.localPosition = Vector3.zero;
                levelCtrl.enabled = true;
            }
        }
        else
        {
            curItem.DoMovingPosDefault();
            curItem.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        curItem = draggableComponent;
    }

    IEnumerator DoMovingSharkerLid()
    {
        Tween shakerMoveX = shakerLid.DOMoveY(0.67f, 0.5f);
        yield return shakerMoveX.WaitForCompletion();
        shakerLid.DOMove(new Vector3(-1.32f, 0.37f,0),0.5f);
        shakerLid.SetParent(shakerCup.transform);

    }
    IEnumerator HandleTimingBar(float targetXMove)
    {
        Tween moveMask = mask.transform.DOMoveX(targetXMove,0.5f);
        yield return moveMask.WaitForCompletion();
        if(targetXMove == 3)
        {
            mask.transform.localPosition = Vector2.zero;
        }
    }
}
