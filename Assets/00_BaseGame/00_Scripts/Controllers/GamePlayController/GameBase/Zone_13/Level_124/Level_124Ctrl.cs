using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_124Ctrl : MonoBehaviour
{
    public L124_Penguin penguin;
    public L124_TimmingBar timmingBar;
    public L124_Seed seed;
    public L124_Apple apple;
    public L124_CanvasFakeBar canvas;
    public BoxCollider2D waterWellCollider;
    public BoxCollider2D dirCollider;

    public IEnumerator SpawnTimmingBar(Vector3 positonSpawn, System.Action callback = null)
    {
        var timmingClone = SimplePool2.Spawn(timmingBar);
        timmingClone.transform.position = positonSpawn;
        yield return StartCoroutine(timmingClone.Init());
        callback?.Invoke();
    }

    public void SpawnSeed(Vector3 positionSpawn)
    {
        var seedClone = SimplePool2.Spawn(seed);
        seedClone.transform.position = positionSpawn;
        seedClone.ResetSeedState();
        Debug.LogError("Spawn seed Success");
    }

    // ham spawn apple tren cay
    public void SpawnApple(List<Transform> lsPointSpawns)
    {
        for(int i =0; i < lsPointSpawns.Count; i++)
        {
            var appleClone = SimplePool2.Spawn(apple);
            appleClone.transform.position = lsPointSpawns[i].position;
            appleClone.InitState();
        }
    }
}
