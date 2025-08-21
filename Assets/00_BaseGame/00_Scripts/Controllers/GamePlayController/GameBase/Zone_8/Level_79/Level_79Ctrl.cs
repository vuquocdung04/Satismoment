using System.Collections;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_79Ctrl : BaseDragController<L79_Piece>
{
    public AudioClip breakSound;
    public int winProgress;
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
        stone.ThrowStone(mouseWorldPos, delegate
        {
            GameController.Instance.musicManager.PlaySingle(breakSound);
        });
        yield return new WaitForSeconds(0.41f);
        piece.ScatterPiece();
    }

    public IEnumerator HandleWinCodition()
    {
        winProgress++;
        if(winProgress == 9)
        {
            isWin = true;
            yield return new WaitForSeconds(1f);
            WinBox.SetUp().Show();
        }
    }
}
