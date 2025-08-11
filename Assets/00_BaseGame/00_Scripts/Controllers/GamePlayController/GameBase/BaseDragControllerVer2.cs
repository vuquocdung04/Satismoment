using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public abstract class BaseDragControllerVer2<T> : BaseDragController<T> where T : Component
{
    public int winProgress;
    public List<T> lsItems;

    public virtual IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }

    [Button("Setup After", ButtonSizes.Large)]
    protected abstract void SetupAfter();

    [Button("Setup Before", ButtonSizes.Large)]
    protected abstract void SetupBefore();
}
