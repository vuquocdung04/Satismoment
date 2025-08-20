using DG.Tweening;
using System.Collections;
using UnityEngine;

public class L125_Food : MonoBehaviour
{
    public Level_125Ctrl levelCtrl;
    public L125_Effect smokeEffect;
    public BoxCollider2D foodCollider;
    public SpriteRenderer foodDoneRenderer;
    public Vector2 defaultPosition;
    public float durationCook;
    public bool isCooked;
    bool isOnGrillFirstTime;
    bool isConsumed;

    public void InitState(Level_125Ctrl levelController)
    {
        foodDoneRenderer.color = new Color(1, 1, 1, 0);
        this.levelCtrl = levelController;
    }
    
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
                Cooking();
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



    public void Cooking()
    {
        if (isCooked) return;
        levelCtrl.PlaySoundCook();
        foodDoneRenderer.DOFade(1, durationCook).SetEase(Ease.Linear).OnComplete(delegate
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
        // ReSharper disable once IteratorNeverReturns
    }
    
    void PauseMoveMask()
    {
        foodDoneRenderer.DOPause();
    }

    void ResumeMoveMask()
    {
        foodDoneRenderer.DOPlay();
    }

    private void OnDestroy()
    {
        foodDoneRenderer.DOKill();
        StopAllCoroutines();
    }

}
