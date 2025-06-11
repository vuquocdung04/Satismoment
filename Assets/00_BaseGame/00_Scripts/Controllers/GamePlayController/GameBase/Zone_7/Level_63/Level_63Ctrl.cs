using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_63Ctrl : BaseDragController<L63_Pig>
{
    public float winProgress;
    public Transform pigShadow;
    private float limitMaxPosY;
    private float limitMinPosY;
    protected override void OnDragEnded()
    {
        limitMaxPosY = draggableComponent.transform.position.y;
        winProgress += limitMaxPosY;


        draggableComponent.transform.DOMoveY(limitMinPosY, 0.5f)
        .SetEase(Ease.OutBounce);

        pigShadow.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
        StartCoroutine(HandleProgressBreakPig());
        StartCoroutine(HandleWinCodition());
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;

        if (draggableComponent.transform.position.y < limitMinPosY)
        {
            draggableComponent.transform.position = new Vector2(draggableComponent.transform.position.x, limitMinPosY);
        }
        HandleShadowScalePig();
    }

    protected override void OnDragStarted()
    {
        minHeight = limitMinPosY = draggableComponent.transform.position.y;
    }
    float minHeight;
    float maxHeight = 4f;
    float t;
    float scale;
    void HandleShadowScalePig()
    {
        pigShadow.position = new Vector2(draggableComponent.transform.position.x, pigShadow.transform.position.y);
        t = Mathf.InverseLerp(minHeight, maxHeight, draggableComponent.transform.position.y);
        scale = Mathf.Lerp(1.0f, 0.5f, t);
        pigShadow.localScale = new Vector3(scale, scale, 1f);
    }

    IEnumerator HandleProgressBreakPig()
    {
        var pig = draggableComponent;
        yield return new WaitForSeconds(0.4f);
        int pigIndex = GetPigIndexByProgress(winProgress);
        pig.DoChangingSpritePig(pigIndex);
    }
    int GetPigIndexByProgress(float progress)
    {
        if (progress > 30f) return 3;
        if (progress > 20f) return 2;
        if (progress > 10f) return 1;
        return 0;
    }

    IEnumerator HandleWinCodition()
    {
        if(winProgress > 30)
        {
            isWin = true;
            yield return new WaitForSeconds(1f);
            WinBox.SetUp().Show();
        }
        
    }
}
