using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L81_Spawner : MonoBehaviour
{
    public Transform earth;

    public List<L81_Meteorite> lsHolds;

    public List<Transform> lsSpawnerPoss;
    public List<L81_Meteorite> lsMeteorites;
    
    [SerializeField] private float regularSpawnInterval = 2f; // Spawn định kỳ mỗi 2s

    private bool shouldStopSpawning = false; // Biến điều kiện dừng
    public void SpawningMeteorite()
    {
        int randMeteorite = Random.Range(0, lsMeteorites.Count);
        int randPos = Random.Range(0, lsSpawnerPoss.Count);

        var meteoriteClone = SimplePool2.Spawn(lsMeteorites[randMeteorite], lsSpawnerPoss[randPos].position, Quaternion.identity);
        meteoriteClone.Init(earth.position);
        lsHolds.Add(meteoriteClone);
    }
    public IEnumerator SpawnRegularly()
    {
        var waitTime = new WaitForSeconds(regularSpawnInterval);
        while (!shouldStopSpawning)
        {
            SpawningMeteorite();
            yield return waitTime;
        }

        Debug.Log("Spawn đã dừng!");
    }

    // Hàm này có thể được gọi từ nơi khác để dừng spawn
    public void StopSpawning()
    {
        shouldStopSpawning = true;
    }
}