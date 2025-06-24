using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_82Ctrl : BaseDragController<L82_PicturePiece>
{
    public int winProgress = 0;
    public Transform mask;
    public L82_BuffterFly buffterFly;
    public List<L82_PicturePiece> lsPicturePieces;
    protected override void OnDragEnded()
    {
        float distance = Vector2.Distance(draggableComponent.transform.position, draggableComponent.posCorrect);

        if (distance < 0.4f)
        {
            draggableComponent.HandleCorrectPosition();
            winProgress++;
        }
        else
        {
            draggableComponent.spriteRenderer.sortingOrder = 4;
        }

        if(winProgress == lsPicturePieces.Count)
        {
            StartCoroutine(HandleWinCodition());
        }

    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.StateStart();
    }


    IEnumerator HandleWinCodition()
    {
        isWin = true;
        buffterFly.gameObject.SetActive(true);
        HiddenPicturePiece();
        Tween maskMove = mask.DOMoveY(0, 1f).SetEase(Ease.Linear);
        yield return maskMove.WaitForCompletion();
        // goi den hieu ung cua con buom
        buffterFly.DoFlying();
        yield return new WaitUntil(()=> buffterFly.isCompleteFly);
        yield return  new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }

    void HiddenPicturePiece()
    {
        foreach(var piece in this.lsPicturePieces)
        {
            piece.spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }
    }

    [Button("Setup piece Picture", ButtonSizes.Large)]
    void SetupPiece()
    {
        foreach(var piecePicture in this.lsPicturePieces)
        {
            piecePicture._collider2d = piecePicture.GetComponent<BoxCollider2D>();
            piecePicture.posCorrect = piecePicture.transform.position;
            piecePicture.spriteRenderer = piecePicture.GetComponent<SpriteRenderer>();
        }
    }



}
