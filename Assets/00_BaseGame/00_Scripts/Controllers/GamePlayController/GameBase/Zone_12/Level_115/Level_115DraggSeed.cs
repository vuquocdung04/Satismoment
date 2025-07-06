using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_115DraggSeed : BaseDragController<L115_Seed>
{
    public Level_115Ctrl levelCtrl;
    public List<L115_Seed> lsSeeds;
    protected override void OnDragEnded()
    {
        if (draggableComponent.HasProperlyDraggedSeedOut())
        {
            levelCtrl.watermelonSeedsDropped++;
            CheckDoneState();
        }
        else
        {
            draggableComponent.OnEndDrag();
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
    }

    void CheckDoneState()
    {
        if (levelCtrl.watermelonSeedsDropped == lsSeeds.Count)
        {
            isWin = true;
            levelCtrl.ActiveState2();
        }
    }

    [Button("Setup ",ButtonSizes.Large)]
    void Setup()
    {
        foreach (var seed in this.lsSeeds)
        {
            seed.InitAfter();
            seed.InitBefore();
        }

    }
    
}
