using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L115_SaltContainer : MonoBehaviour
{
    public L115_SaltParticle saltParticle; 
    public Transform pointSpawn;
    public SpriteRenderer objRenderer;
    public Sprite salt;      
    public bool shouldSpawn = true;

    public void OnDraggEnded()
    {
        StopAllCoroutines();
    }

    public void OnDraggStarted()
    {
        StartCoroutine(SpawnSaltParticles());
    }

    IEnumerator SpawnSaltParticles()
    {
        var waitTime = new WaitForSeconds(0.3f);
        var waitTime2 = new WaitForSeconds(0.1f);
        while (shouldSpawn)
        {
            for (int i = 0; i < 2; i++)
            {
                var saltClone = Instantiate(saltParticle, pointSpawn.position, Quaternion.identity);
                saltClone.Falling();
                yield return waitTime2;
            }

            yield return waitTime; // Chờ 0.3s trước khi spawn tiếp
        }
    }

    public void Complete()
    {
        objRenderer.sprite = salt;
        StopAllCoroutines();
    }
}