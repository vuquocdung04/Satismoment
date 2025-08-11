using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_122Ctrl : BaseDragController<L122_FishTank>
{
    public L122_Fish fish;
    public L122_WaterDrop waterDropPrefab;
    public List<Transform> lsPointSpawn;

    private void Start()
    {
        StartCoroutine(SpawnWaterDrop());
        fish.InitState();
    }

    protected override void OnDragEnded()
    {
        if (draggableComponent.IsFishTankFull())
        {
            isWin = true;
            StartCoroutine(HandleWinCondition());
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        
    }

    IEnumerator HandleWinCondition()
    {
        fish.StopAll();
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }


    IEnumerator SpawnWaterDrop()
    {
        var waitTime2 = new WaitForSeconds(0.1f);
        while (!isWin)
        {
            foreach(var pos in this.lsPointSpawn)
            {
                var waterClone = SimplePool2.Spawn(waterDropPrefab, pos.position, Quaternion.identity);
                waterClone.Falling();
                waterClone.transform.SetParent(transform);
                yield return waitTime2;
            }
        }


    }
    
}
