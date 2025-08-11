using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_140Ctrl : BaseDragController<L140_LintRoller>
{
    public int currentClothScrapsCount;
    public List<L140_ClothScraps> lsClothSCraps;
    protected override void OnDragEnded()
    {
        if(currentClothScrapsCount == lsClothSCraps.Count)
        {
            isWin = true;
            StartCoroutine(HandleWinCondition());
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
        draggableComponent.MoveEffect(mouseDelta.y);
    }
    protected override void OnDragStarted()
    {
        
    }

    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }



    public void CheckClothOverlaping()
    {
        foreach(var clothScrap in this.lsClothSCraps)
        {
            clothScrap.CheckOverlap();
        }
    }

    [Button("Setup",ButtonSizes.Large)]
    void Setup()
    {
        foreach(var clothScrap in this.lsClothSCraps)
        {
            clothScrap.myCollider = clothScrap.transform.GetComponent<BoxCollider2D>();
            clothScrap.objRenderer = clothScrap.transform.GetComponent<SpriteRenderer>();
        }
    }
}
