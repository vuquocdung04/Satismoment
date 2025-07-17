using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Level_143Ctrl : BaseDragController<L143_ItemInThePool>
{
    public L143_effectWater waterPrefabs;
    public Transform water;
    public int spawnCount = 20;
    public Transform waterInThePool;
    public int cleanCount;
    bool isDrained = false;
    private float lastApplyTime;
    public float applyInterval = 0.05f;
    private Coroutine spawnCoroutine;
    protected override void OnDragEnded()
    {
        if(draggableComponent.type != L143_ItemType.PoolLid)
        {
            if (draggableComponent.CheckDrawingCoverage())
            {
                cleanCount++;
                Debug.LogError(cleanCount);

                if(cleanCount == 2)
                {
                    StartCoroutine(HandleWinCondition());
                }
            }
        }
        StopSpawning();

    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        if (!isDrained) return;

        draggableComponent.transform.position += mouseDelta;

        if(draggableComponent.type == L143_ItemType.PoolBrush)
        {
            draggableComponent.DrawAtPosition(draggableComponent.transform.position + Vector3.up/2);
        }
        else if(draggableComponent.type == L143_ItemType.PoolNozzle)
        {
            draggableComponent.DrawAtPosition(draggableComponent.transform.position + Vector3.up * 1.5f);
        }

        if (Time.time - lastApplyTime > applyInterval)
        {
            draggableComponent.ApplyMaskChanges();
            lastApplyTime = Time.time;
        }
    }

    protected override void OnDragStarted()
    {
        lastApplyTime = Time.time; // Đặt lại thời gian khi bắt đầu kéo

        if (draggableComponent.type == L143_ItemType.PoolLid)
        {
            draggableComponent.gameObject.SetActive(false);
            waterInThePool.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.5f).SetEase(Ease.Linear).OnComplete(delegate
            {
                waterInThePool.GetComponent<SpriteRenderer>().DOFade(0,0.4f);
                isDrained = true;
            });
        }
        if(draggableComponent.type == L143_ItemType.PoolNozzle)
        {
            StartSpawning();
        }
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        water.gameObject.SetActive(true);
        Tween fade = waterInThePool.GetComponent<SpriteRenderer>().DOFade(1f, 1f).SetEase(Ease.Linear);
        yield return fade.WaitForCompletion();
        Tween fade2 = waterInThePool.DOScale(Vector3.one,1f).SetEase(Ease.Linear);
        yield return fade2.WaitForCompletion();
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }



    public void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnEffectWaterCoroutine());
        }
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
    IEnumerator SpawnEffectWaterCoroutine()
    {
        var spawnDelay = new WaitForSeconds(0.01f);
        while (true)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                Vector2 currentNozzlePosition = draggableComponent.transform.position + Vector3.up /2;
                var waterClone = SimplePool2.Spawn(waterPrefabs, currentNozzlePosition, Quaternion.identity);
                waterClone.Init();
                waterClone.transform.SetParent(draggableComponent.transform);

                yield return spawnDelay;
            }
        }
    }
}
