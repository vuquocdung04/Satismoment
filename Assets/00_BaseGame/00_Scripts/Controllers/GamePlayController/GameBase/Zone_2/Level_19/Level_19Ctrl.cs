using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Level_19Ctrl : BaseDragController<L19_Book>
{
    public float bookWidth = 0.5f;
    public float bookSpacing = 0.01f;
    public List<L19_Book> lsBooks;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
        HandleSortBook(draggableComponent, false);
    }

    protected override void OnDragEnded()
    {
        base.OnDragEnded();
    }


    private void HandleSortBook(L19_Book book, bool snapPosition = false)
    {
        if(book != null && lsBooks.Contains(book))
        {
            lsBooks.Remove(book);
            lsBooks.Sort((a, b) => a.transform.position.x.CompareTo(b.transform.position.x));
            int indexBook = 0;

            for(int i =0; i < lsBooks.Count; i++)
            {
                if(book.transform.position.x < lsBooks[i].transform.position.x)
                {
                    indexBook = i; break;
                }
                indexBook++;
            }

            lsBooks.Insert(indexBook,book);
        }

        L19_Book bookParam;
        Vector3 targetPosition;
        for(int i = 0; i < lsBooks.Count; i++)
        {
            bookParam = lsBooks[i];
            if (bookParam == null) continue;
            targetPosition = new Vector3(bookParam.transform.position.x, 0, 0);

            if(book == bookParam && !snapPosition) bookParam.transform.position = new Vector3(bookParam.transform.position.x, 0, 0);

            else
            {
                if (snapPosition) bookParam.transform.position = targetPosition;

                else
                    bookParam.transform.position = Vector3.Lerp(bookParam.transform.position, targetPosition, Time.deltaTime * 10f);

            }
        }
    }
}
