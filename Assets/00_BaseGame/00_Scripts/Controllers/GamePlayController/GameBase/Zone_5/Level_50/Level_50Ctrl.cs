using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Level_50Ctrl : MonoBehaviour
{
    public bool isWin;
    public bool isClick = true;
    [Header("Handle win  codition")]
    public Transform board;
    public SpriteRenderer boy;
    public Sprite boySprite;
    public Transform anim;

    [Space(5)]
    public L50_fishingRod fishingRod;
    public Transform fishingLine;
    public L50_hook hook;
    void Update()
    {
        if (isWin) return;
        if (!isClick) return; 
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(HandleHookAction());
        }       
    }

    IEnumerator HandleHookAction()
    {
        isClick = false;
        fishingRod.rotationEnabled = false;
        hook.ResetCollisionState();
        Tween downTween = fishingLine.DOLocalMoveY(-3f,1f);
        hook.SetCurrentLineMovement(downTween);
        yield return downTween.WaitForCompletion();
        hook._collider.enabled = false;
        Tween upTween = fishingLine.DOLocalMoveY(3f,1f);
        yield return upTween.WaitForCompletion();

        if (hook.item != null)
        {
            if (hook.item.itemType != L50_ItemType.Treasure_Chest)
            {
                if (hook.item != null)
                {
                    Destroy(hook.item.gameObject);
                }
            }
            else
            {
                StartCoroutine(HandleWinCodition());
            }
        }
        hook._collider.enabled = true;
        fishingRod.rotationEnabled = true;
        isClick = true;
    }


    IEnumerator HandleWinCodition()
    {
        isWin = true;
        anim.gameObject.SetActive(false);
        boy.sprite = boySprite;
        Tween moveBoard =  board.DOMoveX(-2.62f,3f).SetEase(Ease.OutQuad);
        yield return moveBoard.WaitForCompletion();
        yield return new WaitForSeconds(0.1f);
        WinBox.SetUp().Show();
    }
    void OnDestroy()
    {
        if (fishingLine != null) fishingLine.DOKill();
        if (board != null) board.DOKill();
        hook.KillTween();
    }
}
