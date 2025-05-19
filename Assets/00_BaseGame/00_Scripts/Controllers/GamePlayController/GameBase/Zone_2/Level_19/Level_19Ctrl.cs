using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class Level_19Ctrl : BaseDragController<L19_Book>
{
    public Transform hand;
    public Sprite bookRed;
    public float animationDuration = 1f;
    public float startPosX;
    public float spacing = 0.5f;
    public float totalActualWidth;
    public int validBookCount;
    public List<L19_Book> lsBooks;
    Vector3 newPos;
    private void Start()
    {
        HandleSortBook(null, true);
    }
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        newPos = draggableComponent.transform.position;
        newPos.x += deltaMousePosition.x;
        newPos.y = -2.95f;
        draggableComponent.transform.position = newPos;
        lsBooks.Sort((a, b) => a.transform.position.x.CompareTo(b.transform.position.x));

        HandleSortBook(draggableComponent,false);
    }

    protected override void OnDragEnded()
    {
        base.OnDragEnded();
        StartCoroutine(Wai());
    }

    IEnumerator Wai()
    {
        HandleSortBook(null, true);
        yield return null;
        HandleWin();
    }

    private void HandleSortBook(L19_Book draggedBook, bool snapPosition = false)
    {
        float currentEdgeX = this.startPosX;
        L19_Book book = null;
        Vector3 targetPosition;
        for (int i =0; i < lsBooks.Count; i++)
        {
            book = lsBooks[i];
            if (book == null) continue;
            float targetBookCenterX = currentEdgeX + book.bookWidth/ 2;

            targetPosition = new Vector3(targetBookCenterX, -2.95f,0);

            if(book == draggedBook && !snapPosition)
            {
                //TODO
            }
            else
            {
                if (snapPosition) book.transform.position = targetPosition;
                else book.transform.position = Vector3.Lerp(book.transform.position, targetPosition, 10f * Time.deltaTime);
            }
            currentEdgeX += book.bookWidth + spacing;

        }

    }

    void HandleWin()
    {
        if (!CheckWinCondition()) return;

        foreach(var book in this.lsBooks) book.colli.enabled = false;

        hand.DOMoveY(-3.5f, animationDuration).SetEase(Ease.InQuad).OnComplete(delegate
        {
            lsBooks[lsBooks.Count - 1].spriteRenderer.sprite = bookRed;
            lsBooks[lsBooks.Count - 1].transform.SetParent(hand);
            hand.DOMoveY(-11f, animationDuration).SetEase(Ease.OutQuad);
            
            for(int i =0; i < lsBooks.Count- 1; i++)
            {
                lsBooks[i].transform.position = new Vector2(lsBooks[i].transform.position.x, -2.88f);
                lsBooks[i].transform.DORotate(new Vector3(0, 0, -12.4f), 1f).SetEase(Ease.OutCubic);
            }

            DOVirtual.DelayedCall(2f, () => WinBox.SetUp().Show());

        });
    }

    bool CheckWinCondition()
    {
        for(int i = 0; i < lsBooks.Count; i++)
        {
            if (lsBooks[i].idBook != i) return false;
        }
        return true;
    }



    [Button("Caculate Width", ButtonSizes.Large)]
    void Caculate()
    {
        this.startPosX = lsBooks[0].posDefault.x  - lsBooks[0].bookWidth/2f;
        totalActualWidth = 0;
        validBookCount = 0;
        foreach (var book in this.lsBooks)
        {
            totalActualWidth += book.bookWidth;
            validBookCount++;
        }
        totalActualWidth += (validBookCount - 1) * spacing;

    }
}
