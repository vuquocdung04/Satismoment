using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_123Ctrl : BaseDragController<L123_HairClipper>
{
    public L123_Cat cat;
    public List<L123_CatHair> lsCatHairs;

    protected override void OnDragEnded()
    {
        draggableComponent.OnStateEnd();
        cat.ChangeSpriteDefault();

    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStateStart();
        cat.ChangeSpriteSuprise();
    }

    int winProgress = 0;
    public void InCreaseWinAmount()
    {
        winProgress++;
        if (winProgress == lsCatHairs.Count)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }

    [Button("Setup",ButtonSizes.Large)]
    void Setup()
    {
        foreach(var catHair in this.lsCatHairs)
        {
            catHair.InitSetup();
        }
    }
}
