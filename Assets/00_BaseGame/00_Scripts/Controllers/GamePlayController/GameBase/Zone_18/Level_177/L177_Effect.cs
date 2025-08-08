using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L177_Effect : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public List<Sprite> lsFrame;

    private Coroutine animationCoroutine;
    private int currentFrameIndex = 0;

    public void StartAnimation()
    {
        if (animationCoroutine == null && lsFrame.Count > 0)
        {
            animationCoroutine = StartCoroutine(AnimateSprite());
        }
    }

    // Hàm dừng animation
    public void StopAnimation()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }
    }

    // Coroutine đổi sprite liên tục
    private IEnumerator AnimateSprite()
    {
        var waitTime = new WaitForSeconds(0.2f);
        while (true)
        {
            // Đổi sprite hiện tại
            objRenderer.sprite = lsFrame[currentFrameIndex];

            // Tăng index và reset về 0 khi đến cuối list
            currentFrameIndex = (currentFrameIndex + 1) % lsFrame.Count;

            // Chờ 0.5 giây
            yield return waitTime;
        }
    }
}
