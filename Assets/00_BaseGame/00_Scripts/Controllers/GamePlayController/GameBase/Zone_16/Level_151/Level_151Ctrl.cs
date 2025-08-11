using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_151Ctrl : BaseDragController<L151_PiecePenguin>
{
    public Sprite spriteStart_0;
    public Sprite spriteStart_1;
    public Sprite spriteDefault_0;
    public Sprite spriteDefault_1;
    public int winProgress;
    public L151_Candy candyPrefab;

    public List<L151_PiecePenguin> lsPieces;
    protected override void OnDragEnded()
    {
        lsPieces[0].ChangeSprite(spriteDefault_0);
        lsPieces[1].ChangeSprite(spriteDefault_1);
        draggableComponent.Falling(this);
        if(winProgress == 4)
        {
            isWin = true;
            HandleStateWin();
            for (int i  =0; i < 15; i++)
            {
                var candyClone = Instantiate(candyPrefab,Vector3.zero,Quaternion.identity);
                candyClone.InitState();
            }
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        
    }

    protected override void OnDragStarted()
    {
        draggableComponent.countTouch++;
        lsPieces[0].ChangeSprite(spriteStart_0);
        lsPieces[1].ChangeSprite(spriteStart_1);
    }


    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }
    void HandleStateWin()
    {
        foreach(var piece in this.lsPieces)
        {
            piece.objCollider.enabled = false;
            piece.transform.DOMoveY(-4.5f, 0.5f).SetEase(Ease.OutBack);
        }
    }

    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        foreach(var piece in this.lsPieces)
        {
            piece.objCollider = piece.transform.GetComponent<BoxCollider2D>();
            piece.objRenderer = piece.transform.GetComponent<SpriteRenderer>();
        }
    }
}
