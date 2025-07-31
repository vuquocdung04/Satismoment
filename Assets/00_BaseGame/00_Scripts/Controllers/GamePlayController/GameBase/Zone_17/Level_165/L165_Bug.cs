using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;   // Nhớ import DOTween

public class L165_Bug : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public Sprite spriteDeath;
    public List<Sprite> lsFrames;
    public float animInterval = 0.3f; // thời gian đổi sprite
    public float moveTime = 2.0f;     // thời gian di chuyển mỗi lần

    private bool isDead = false;
    private Coroutine spriteCoroutine;

    public void Init()
    {
        spriteCoroutine = StartCoroutine(SpriteChangeCoroutine());
        MoveToNextPoint();
    }

    IEnumerator SpriteChangeCoroutine()
    {
        int frame = 0;
        while (!isDead)
        {
            if (lsFrames.Count > 0)
            {
                objRenderer.sprite = lsFrames[frame % lsFrames.Count];
                frame++;
            }
            yield return new WaitForSeconds(animInterval);
        }
    }

    // Di chuyển đến một điểm ngẫu nhiên và tiếp tục khi tới nơi
    void MoveToNextPoint()
    {
        if (isDead) return; // chết thì không di chuyển nữa

        Vector3 nextPosition = GetRandomPosition();
        Vector3 dir = nextPosition - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.DORotateQuaternion(Quaternion.Euler(0, 0, angle - 90), 0.5f);
        transform.DOMove(nextPosition, moveTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            MoveToNextPoint();
        });
    }

    public void Kill()
    {
        if (isDead) return;
        isDead = true;
        transform.DOKill();

        if (spriteCoroutine != null)
            StopCoroutine(spriteCoroutine);

        objRenderer.sprite = spriteDeath;
        StartCoroutine(WaitDestroy());
    }
    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }


    Vector3 GetRandomPosition()
    {
        float minX = -2.8f;
        float maxX = 2.8f;
        float minY = -4.8f;
        float maxY = 4.8f;
        return new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
    }
}

