using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_90Ctrl : BaseDragController<L90_Lego>
{
    public AudioClip pickSound;
    public AudioClip placeSound;
    public int winProgress;
    public Transform boxLid;
    public List<L90_Lego> lsLegos;
    protected override void OnDragEnded()
    {
        if (draggableComponent.IsItemInCorrectCompartment())
        {
            winProgress++;
            if(winProgress == lsLegos.Count)
            {
                isWin = true;
                GameController.Instance.musicManager.PlaySingle(placeSound);
                StartCoroutine(HandleWinCondition());
            }
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
        
        GameController.Instance.musicManager.PlaySingle(pickSound);
    }
    IEnumerator HandleWinCondition()
    {
        Tween moveLidBox = boxLid.DOMoveY(0.15f,1f).SetEase(Ease.Linear);
        yield return moveLidBox.WaitForCompletion();
        yield return new WaitForSeconds(0.2f);
        WinBox.SetUp().Show();
    }

    [Button("Setup After",ButtonSizes.Large)]
    void SetupAfter()
    {
        foreach(var lego in this.lsLegos)
        {
            lego.InitCorrect();
        }
    }
    [Button("Setup Before",ButtonSizes.Large)]
    void SetupBefore()
    {
        foreach (var lego in this.lsLegos)
        {
            lego.InitDefault();
        }
    }
}
