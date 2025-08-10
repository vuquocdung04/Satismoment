using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L181_FireFly : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public BoxCollider2D objCollider;
    public List<Sprite> lsFrames;

    private Coroutine animationCoroutine;
    private bool isAnimationPaused = false;
    private Vector2 originalPosition;
    private void Start()
    {
        InitState();
        StartSpriteAnimation();
        originalPosition = transform.position;
    }

    public void InitState()
    {
        float randDuration = Random.Range(4f, 8f);
        transform.DOMoveY(7f, randDuration).SetEase(Ease.Linear).OnComplete(delegate
        {
            transform.position = new Vector3(transform.position.x, originalPosition.y);
            InitState();
        });
    }

    Vector2 pointCatched;

    public void OnDragStart()
    {
        transform.DOPause();
        PauseSpriteAnimation();
        pointCatched = transform.position;
    }

    public void CheckCollisionWithJar(Transform jarTrans, System.Action callback = null)
    {
        float distance = Vector2.Distance(transform.position, jarTrans.position);
        var bondSizeJar = jarTrans.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        var bondSize = objRenderer.bounds.size.x / 2;
        float limit = bondSizeJar - bondSize;
        if (distance < limit)
        {
            objCollider.enabled = false;
            transform.DOKill();
            StopSpriteAnimation();
            Debug.LogError("Touch jar");
            callback?.Invoke();
        }
        else
        {
            objCollider.enabled = false;
            transform.DOMove(pointCatched, 0.2f).OnComplete(delegate
            {
                objCollider.enabled = true;
                transform.DOPlay();
                ResumeSpriteAnimation();
            });
        }
    }

    private void StartSpriteAnimation()
    {
        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        animationCoroutine = StartCoroutine(SpriteAnimationCoroutine());
    }

    private void StopSpriteAnimation()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }
        isAnimationPaused = false;
    }

    private void PauseSpriteAnimation()
    {
        isAnimationPaused = true;
    }

    private void ResumeSpriteAnimation()
    {
        isAnimationPaused = false;
    }

    private IEnumerator SpriteAnimationCoroutine()
    {
        int currentFrame = 0;
        var waitTime = new WaitForSeconds(0.2f);
        while (true)
        {
            if (!isAnimationPaused && lsFrames.Count > 0)
            {
                objRenderer.sprite = lsFrames[currentFrame];
                currentFrame = (currentFrame + 1) % lsFrames.Count;
            }

            yield return waitTime;
        }
    }

    private void OnDestroy()
    {
        StopSpriteAnimation();
    }
}
