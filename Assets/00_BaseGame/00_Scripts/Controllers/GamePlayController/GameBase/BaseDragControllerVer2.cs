using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class BaseDragControllerVer2<T> : BaseDragController<T> where T : Component
{
    public int winProgress;
    [FormerlySerializedAs("lsItems")] public List<T> lsT_ItemDragables;

    protected virtual IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }

    [Button("Setup Position Correct", ButtonSizes.Large)]
    protected abstract void SetupComponent_PositionCorrect();

    [Button("Setup Position Default", ButtonSizes.Large)]
    protected abstract void SetupPositionDefault();
}
