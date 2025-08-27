
using System;
using UnityEngine;

public class LevelGameCtrl : MonoBehaviour
{
    public void Init()
    {
        GameController.Instance.levelBundleManager.InstantiateLevelFromPreloaded(UseProfile.CurrentLevel);
    }

    private void OnDestroy()
    {
        GameController.Instance.levelBundleManager.UnloadCurrentLevel();
        GameController.Instance.levelBundleManager.UnloadPreloadedAsset();
    }
}
