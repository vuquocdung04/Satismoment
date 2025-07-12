using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L129_Police : MonoBehaviour
{
    public L129_Circle currentCirle;
    public SpriteRenderer policeRenderer;
    public List<Sprite> lsFrames;
    private Coroutine animateCoroutine;
    public void Moving(L129_Circle targetMove)
    {
        // Bắt đầu di chuyển
        var moveTween = transform.DOMove(targetMove.transform.position, 0.5f).SetEase(Ease.Linear);

        // Bắt đầu hiệu ứng đổi sprite
        if (animateCoroutine != null)
            StopCoroutine(animateCoroutine);
        animateCoroutine = StartCoroutine(AnimatePolice());

        // Đăng ký sự kiện hoàn thành của DOTween
        moveTween.OnComplete(() =>
        {
            if (animateCoroutine != null)
            {
                StopCoroutine(animateCoroutine);
                animateCoroutine = null;
            }
            Debug.Log("Move Complete");
        });

        currentCirle = targetMove;
    }

    IEnumerator AnimatePolice()
    {
        int frameIndex = 0;
        var waitTime = new WaitForSeconds(0.1f);
        while (true)
        {
            policeRenderer.sprite = lsFrames[frameIndex];
            frameIndex = (frameIndex + 1) % lsFrames.Count;
            yield return waitTime;
        }
    }

    private void OnDestroy()
    {
        animateCoroutine = null;
        StopAllCoroutines();
    }
}
