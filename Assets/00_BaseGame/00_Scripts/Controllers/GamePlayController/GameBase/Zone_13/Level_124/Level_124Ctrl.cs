using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum L124_ObjType
{
    Seed,
    Bucket,
    Apple,
}
public class Level_124Ctrl : MonoBehaviour
{
    public int currentAppleAmount;
    public L124_DragObjCtrl dragObjCtrl;
    public L124_Penguin penguin;
    public L124_TimmingBar timmingBar;
    public L124_Seed seed;
    public L124_Apple apple;
    public L124_CanvasFakeBar canvas;
    public L124_Dir dir;
    public BoxCollider2D waterWellCollider;


    private void Start()
    {
        var postionSpawn = new Vector3(-1.5f,0,0);
        SimplePool2.Spawn(apple.gameObject, postionSpawn, Quaternion.identity);
    }

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
            appleClone.objCollider.enabled = false;
            appleClone.InitState();
            dir.GetCurrentSeed().lsApples.Add(appleClone);
        }
    }

    public void IncreaseAmountApple()
    {
        currentAppleAmount++;
        if(currentAppleAmount == canvas.lsPoints.Count)
        {
            dragObjCtrl.isWin = true;
            StartCoroutine(HandleWinCondition());
        }
    }

    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}
