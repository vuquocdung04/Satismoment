using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L124_Dir : MonoBehaviour
{
    public Level_124Ctrl levelCtrl;
    public BoxCollider2D dirCollider;
    [SerializeField] bool hasSeeded;
    [SerializeField] L124_Seed currentSeed;

    public void SetCurrentSeed(L124_Seed seed)
    {
        currentSeed = seed;
        hasSeeded = true;
    }
    public L124_Seed GetCurrentSeed() => currentSeed;
    public bool HasSeeded()
    {
        return hasSeeded;
    }

    public void HandleGrowSeed()
    {
        if(currentSeed == null) return;
        currentSeed.GrownSeed();
    }


    public void OnSeedGrowthComplete()
    {
        if(currentSeed == null) return;
        currentSeed = null;
        hasSeeded = false;
    }
}
