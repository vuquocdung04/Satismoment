using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_181Ctrl : BaseDragController<L181_FireFly>
{
    public Transform jarTrans;
    public Transform maskLight;
    int fireFlyAmount;
    Vector2 scaleAmount;
    protected override void OnDragEnded()
    {
        draggableComponent.CheckCollisionWithJar(jarTrans, delegate
        {
            fireFlyAmount++;
            if (fireFlyAmount == 3)
            {
                scaleAmount = Vector3.one * fireFlyAmount * 10;
                maskLight.DOScale(scaleAmount, 0.4f).OnComplete(delegate
                {
                    Debug.Log("Win Game!");
                    isWin = true;
                    StartCoroutine(HandleWinCondition());
                });
            }
            else
            {
                scaleAmount = Vector3.one * fireFlyAmount * 5;
                maskLight.DOScale(scaleAmount, 0.4f);
            }
        });

    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnDragStart();
    }

    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}
