using UnityEngine;

public class L14_Nozzle : MonoBehaviour
{
    public Transform waterPrefab;          // Prefab của tia nước (phải có script L14_Water)
    public Transform spawnPoint;            // Điểm cụ thể để tia nước xuất hiện
    public void SpawnWater()
    {
        SimplePool2.Spawn(waterPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}