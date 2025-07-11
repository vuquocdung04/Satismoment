using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L125_Food : MonoBehaviour
{
    public L125_Effect smokeEffect;
    public BoxCollider2D foodCollider;
    public Transform mask;
    public Vector2 defaultPosition;
    public float durationCook;
    public bool isCooked;
    bool isOnGrillFirstTime;
    bool isConsumed = false;
    public void HandleConllisionWithGrill(Collider2D colli)
    {
        if (isConsumed)
        {
            StartSpawningSmoke();
            return;
        }

        if (!foodCollider.IsTouching(colli))
        {
            MoveToDefaultPosition();
        }
        else
        {
            if (isOnGrillFirstTime)
            {
                ResumeMoveMask();
            }
            else
            {
                MoveMask();
                isOnGrillFirstTime = true;
            }
        }

    }

    public bool CheckCollisitonWithMount(Collider2D mountCollider)
    {
        if (!isCooked) return false;
        if (!foodCollider.IsTouching(mountCollider)) return false;
        isConsumed = true;
        return true;
    }


    public void OnStartDrag()
    {
        if (isOnGrillFirstTime)
            PauseMoveMask();

        StopSpawningSmoke();
    }

    public void MoveToDefaultPosition()
    {
        transform.DOMove(defaultPosition,0.5f).SetEase(Ease.InBack);
    }



    public void MoveMask()
    {
        if (isCooked) return;
        mask.DOLocalMoveY(0, durationCook).SetEase(Ease.Linear).OnComplete(delegate
        {
            isCooked = true;
            StartSpawningSmoke();
            
        });
    }
    private Coroutine smokeCoroutine;
    public void StartSpawningSmoke()
    {
        if (smokeCoroutine == null)
        {
            smokeCoroutine = StartCoroutine(SpawnSmokePeriodically(0.75f));
        }
    }
    public void StopSpawningSmoke()
    {
        if (smokeCoroutine != null)
        {
            StopCoroutine(smokeCoroutine);
            smokeCoroutine = null;
        }
    }
    private IEnumerator SpawnSmokePeriodically(float interval)
    {
        var waiTime = new WaitForSeconds(interval);
        while (true)
        {
            var smoke = SimplePool2.Spawn(smokeEffect, transform.position, Quaternion.identity);
            smoke.Init();

            yield return waiTime;
        }
    }
    void PauseMoveMask()
    {
        mask.DOPause();
    }

    void ResumeMoveMask()
    {
        mask.DOPlay();
    }

}
