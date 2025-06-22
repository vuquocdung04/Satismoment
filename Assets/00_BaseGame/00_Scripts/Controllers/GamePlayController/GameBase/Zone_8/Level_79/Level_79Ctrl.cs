using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_79Ctrl : BaseDragController<L79_Piece>
{
    public int winProgress = 0;
    public L79_Stone stonePrefab;

    protected override void OnDragEnded()
    {
        
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        
    }

    protected override void OnDragStarted()
    {
        var cloneStone = SimplePool2.Spawn(stonePrefab, new Vector3(0,-2.7f,0),Quaternion.identity);
        StartCoroutine(HandleThrowStone(cloneStone, draggableComponent));
    }

    IEnumerator HandleThrowStone(L79_Stone stone, L79_Piece piece)
    {
        stone.ThrowStone(mouseWorldPos);
        yield return new WaitForSeconds(0.41f);
        piece.ScatterPiece();
    }

    public IEnumerator HandleWinCodition()
    {
        winProgress++;
        if(winProgress == 9)
        {
            isWin = true;
            yield return new WaitForSeconds(0.4f);
            WinBox.SetUp().Show();
        }
    }
}
