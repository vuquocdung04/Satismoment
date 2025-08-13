using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_180Ctrl : BaseDragControllerVer2<L180_Coin>
{
    public BoxCollider2D colli;
    public Transform bottle;
    public List<Transform> lsPoints;

    private void Start()
    {
        foreach(var point in this.lsPoints) point.gameObject.SetActive(false);
    }

    protected override void OnDragEnded()
    {
        draggableComponent.CheckCorrectToPosition(colli,this);
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
    }

    public IEnumerator CheckWin()
    {
        if(winProgress == lsT_ItemDragables.Count)
        {
            isWin = true;
            Tween moveBottle = bottle.DOMoveY(-2f, 0.5f).SetEase(Ease.Linear);
            yield return moveBottle.WaitForCompletion();
            Tween rotateBottle = bottle.DORotate(new Vector3(0, 0, -90f), 0.2f, RotateMode.Fast);
            yield return rotateBottle.WaitForCompletion();
            StartCoroutine(HandleWinCondition());
        }
    }

    public void ActivePoint()
    {
        lsPoints[winProgress].gameObject.SetActive(true);
    }

    protected override void SetupComponent_PositionCorrect()
    {
        foreach(var coin in this.lsT_ItemDragables) coin.InitCorrect();
    }

    protected override void SetupPositionDefault()
    {
        foreach(var coin in this.lsT_ItemDragables) coin.InitDefault();
    }
}
