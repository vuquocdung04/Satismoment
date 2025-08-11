using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_120Ctrl : BaseDragController<L120_LightBulb>
{
    public int winProgress;
    public Transform mask;
    public List<L120_LightBulb> lsLightBulb;
    protected override void OnDragEnded()
    {
        if(draggableComponent.IsRotationCompleted())
        {
            winProgress++;
            draggableComponent.DoAnimCompleted();
            if (winProgress == lsLightBulb.Count)
                StartCoroutine(HandleWinCondition());
        }
    }

    float rotationAmount;
    float movementAmount;
    Vector3 newPos;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        rotationAmount = mouseDelta.x * 15f;
        draggableComponent.RotateFilament(rotationAmount);

        movementAmount = rotationAmount * 0.25f/180; // xoay 180 thi di chuyen duoc 0.25
        draggableComponent.MoveBulbEnvelope(movementAmount,newPos);
    }

    protected override void OnDragStarted()
    {
        
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        mask.gameObject.SetActive(true);
        Tween maskScale = mask.transform.DOScale(new Vector3(11f,11f,11f),0.5f).SetEase(Ease.OutQuad);
        yield return maskScale.WaitForCompletion();
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();

    }
    [Button("Setup",ButtonSizes.Large)]
    void Setup()
    {
        foreach(var bulb in this.lsLightBulb) bulb.InitSetup();
    }
}
