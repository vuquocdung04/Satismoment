
using System;
using System.Collections;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;
using Sirenix.OdinInspector;

public class Level_25Ctrl : BaseDragController<Transform>
{
    public Transform pullCore;
    public Transform windowBlind;
    public Vector2 posPullCoreStart;
    public Vector2 posWindowBlindStart;
    Vector3 newPos;


    float t;
    protected override void OnDragStarted()
    {
        GameController.Instance.musicManager.PlayPickItemSound();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        newPos = draggableComponent.transform.position;
        newPos.y += deltaMousePosition.y;
        newPos.x = posPullCoreStart.x;
        newPos.y = Mathf.Clamp(newPos.y, 0.15f, draggableComponent.transform.position.y);
        draggableComponent.transform.position = newPos;

        t = posPullCoreStart.y - draggableComponent.transform.position.y;

        windowBlind.transform.position = new Vector3(0, posWindowBlindStart.y + t * 1.3f);

        if (pullCore.transform.position.y < 0.2f)
        {
            StartCoroutine(HandleWin());
            isWin = true;
        }
    }

    protected override void OnDragEnded()
    {

    }


    IEnumerator HandleWin()
    {
        yield return new WaitForSeconds(0.7f);
        WinBox.SetUp().Show();

    }

    [Button("SetUp",ButtonSizes.Large)]
    void SetUp()
    {
        posPullCoreStart = pullCore.transform.position;
        posWindowBlindStart = windowBlind.transform.position;
    }
}
