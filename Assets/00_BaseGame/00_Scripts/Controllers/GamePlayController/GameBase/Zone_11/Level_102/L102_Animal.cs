using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class L102_Animal : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Dùng để thay đổi sprite
    public List<Sprite> lsFrameAnimations; // Danh sách các frame animation
    private int currentFrame = 0;

    private Vector3 targetPosition; // Vị trí đích
    public float moveDuration = 1f; // Thời gian di chuyển mỗi lần
    public float timeChangeFrame = 0.1f;
    bool isWin = false; 
    void Start()
    {
        StartCoroutine(PlayAnimation());
        MoveBackAndForth();
    }
    Tween animalMove;
    public void MoveBackAndForth()
    {
        targetPosition = new Vector3(-3.5f, transform.position.y, transform.position.z);
        transform.localScale = Vector3.one;
        animalMove = transform.DOMoveX(-3.5f, moveDuration).SetEase(Ease.Linear).OnComplete(delegate
        {
            transform.localScale = new Vector3(-1,1,1);
            animalMove = transform.DOMoveX(3.5f, moveDuration).SetEase(Ease.Linear).OnComplete(delegate
            {
                MoveBackAndForth();
            });
        });
    }

    // Coroutine chạy animation đổi sprite
    public IEnumerator PlayAnimation()
    {
        var waitTime = new WaitForSeconds(timeChangeFrame);
        while (!isWin)
        {
            if (lsFrameAnimations.Count > 0)
            {
                spriteRenderer.sprite = lsFrameAnimations[currentFrame];
                currentFrame = (currentFrame + 1) % lsFrameAnimations.Count;
            }

            yield return waitTime;
        }
    }

    public void StopAll()
    {
        isWin = true;
        StopAllCoroutines();
        animalMove.Kill();
    }
}