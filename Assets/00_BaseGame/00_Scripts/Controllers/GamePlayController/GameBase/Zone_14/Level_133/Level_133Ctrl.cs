using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_133Ctrl : BaseDragControllerVer2<L133_RoomItems>
{
    public L133_BoxSetup bookSetup;
    public L133_BookSelf bookSelf;

    private void Start()
    {
        bookSetup.Init();
    }

    protected override void OnDragEnded()
    {
        HandleItemToCorrectPosition();
        draggableComponent.OnEndDrag();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }
    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
    }

    void HandleItemToCorrectPosition()
    {
        if (draggableComponent.CheckCorrectPosition())
        {
            switch (draggableComponent.itemType)
            {
                case L133_ItemType.None:
                    draggableComponent.objectCollider.enabled = false;
                    break;
                case L133_ItemType.StackOfBooks:
                    if (bookSelf.amount == 0)
                    {
                        bookSelf.amount++;
                        bookSelf.bookShelfRenderer.sprite = bookSelf.spriteHaveBooks;
                    }
                    else
                        bookSelf.bookShelfRenderer.sprite = bookSelf.spriteAll;
                    break;
                case L133_ItemType.Box:
                    if (bookSelf.amount == 0)
                    {
                        bookSelf.amount++;
                        bookSelf.bookShelfRenderer.sprite = bookSelf.spriteHaveBoxs;
                    }
                    else
                        bookSelf.bookShelfRenderer.sprite = bookSelf.spriteAll;
                    break;
            }
            winProgress++;
            draggableComponent.MoveItemToCorrectPosition();
            if (winProgress == lsItems.Count)
            {
                isWin = true;
                StartCoroutine(HandleWinCondition());
            }
        }
    }




    protected override void SetupAfter()
    {
        foreach (var item in this.lsItems) item.InitAfter();
    }

    protected override void SetupBefore()
    {
        foreach (var item in this.lsItems) item.InitBefore();
    }
}
