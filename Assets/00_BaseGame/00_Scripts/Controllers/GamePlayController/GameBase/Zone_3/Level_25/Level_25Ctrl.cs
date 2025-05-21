using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Level_25Ctrl : BaseDragController<Transform>
{
    public bool isWin = false;
    public Transform pullCore;
    public Transform windowBlind;
    public Vector2 posPullCoreStart;
    public Vector2 posWindowBlindStart;
    Vector3 newPos;


    float t;
    protected override void Update()
    {
        if (isWin) return;
        base.Update();
    }
    protected override void OnDragStarted()
    {
        base.OnDragStarted();

    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        newPos = draggableComponent.transform.position;
        newPos.y += deltaMousePosition.y;
        newPos.x = posPullCoreStart.x;

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
        base.OnDragEnded();
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
