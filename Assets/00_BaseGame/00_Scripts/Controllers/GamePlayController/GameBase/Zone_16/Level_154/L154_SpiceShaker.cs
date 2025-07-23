using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L154_SpiceShaker : MonoBehaviour
{
    public L154_Spice spicePrefab;
    public SpriteRenderer objRenderer;
    public Sprite spriteDefault;
    public Sprite spriteShaker;


    public int count = 0; // Biến đếm số lượng spice đã rơi
    public bool isMaxSpiceReached = false; // Biến bool kiểm tra đã đủ 5 hạt

    public void ChangeSpriteShaker()
    {
        objRenderer.sprite = spriteShaker;
        StartSpawn();
    }

    public void ChangeSpriteDefault()
    {
        objRenderer.sprite = spriteDefault;
        StopSpawn();
    }

    public void MoveX(float speedAmount)
    {
        transform.position += new Vector3(speedAmount, 0);
    }

    public IEnumerator SpawnSpice(float interval)
    {
        var waitTime = new WaitForSeconds(interval);
        while (count < 5) // Chỉ spawn khi count < 5
        {
            var spiceClone = Instantiate(spicePrefab, transform.position - Vector3.up, Quaternion.identity);
            spiceClone.Falling();
            count++; // Tăng count lên 1
            yield return waitTime;
        }

        // Đánh dấu đã đủ 5 hạt và tự động dừng coroutine
        isMaxSpiceReached = true;
        coroutineSpawn = null;

    }

    Coroutine coroutineSpawn;

    void StartSpawn()
    {
        // Kiểm tra nếu đã đủ 5 hạt thì return không thực hiện
        if (isMaxSpiceReached)
        {
            return;
        }
        coroutineSpawn = StartCoroutine(SpawnSpice(0.2f));
    }

    void StopSpawn()
    {
        if (coroutineSpawn != null)
        {
            StopCoroutine(coroutineSpawn);
            coroutineSpawn = null;
        }
    }

    public void MoveWhenComplete()
    {
        transform.DOMoveX(transform.position.x - 5f, 0.5f).SetEase(Ease.Linear);
    }

}
