using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L156_NewsPaper : MonoBehaviour
{
    public L156_scrap scrap;
    public BoxCollider2D objCollider;
    public SpriteRenderer objRenderer;
    public bool state1_Completed;
    Vector3 newPos;
    public float limitY = -0.45f;

    // Thêm biến để kiểm soát việc spawn scrap
    private float lastSpawnY;
    public float spawnInterval = 0.1f; // Khoảng cách để spawn scrap mới

    void Start()
    {
        lastSpawnY = transform.position.y;
    }

    public void Moving(Vector3 mouseDelta)
    {
        newPos = transform.position + mouseDelta;
        newPos.y = Mathf.Clamp(newPos.y, 1.4f, 4f);
        newPos.x = Mathf.Clamp(newPos.x, -1.5f, 1.5f);
        transform.position = newPos;
    }

    public void MoveY(float speed)
    {
        float oldY = transform.position.y;
        newPos = transform.position + new Vector3(0, speed, 0);
        newPos.y = Mathf.Clamp(newPos.y, limitY, transform.position.y);
        transform.position = newPos;

        // Spawn scrap khi di chuyển xuống
        if (speed < 0) // Chỉ spawn khi di chuyển xuống
        {
            CheckAndSpawnScrap(oldY);
        }
    }

    private void CheckAndSpawnScrap(float oldY)
    {
        // Spawn scrap dựa trên khoảng cách di chuyển
        if (lastSpawnY - transform.position.y >= spawnInterval)
        {
            SpawnScrap();
            lastSpawnY = transform.position.y;
        }
    }

    public void SpawnScrap()
    {
        float randX = Random.Range(-1.5f, 1.5f);
        Vector2 posSpawn = new Vector2(randX, -0.4f);
        var scrapClone = SimplePool2.Spawn(scrap, posSpawn, Quaternion.identity);
        scrapClone.Moving();
    }
}
