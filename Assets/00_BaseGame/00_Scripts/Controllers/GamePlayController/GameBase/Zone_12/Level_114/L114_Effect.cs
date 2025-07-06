using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L114_Effect : MonoBehaviour
{
    public SpriteRenderer effectRenderer;
    public List<Sprite> lsFrames;

    private Coroutine animationCoroutine;
    IEnumerator PlayAnimation()
    {
        int frameIndex = 0;
        var waitTime = new WaitForSeconds(0.3f);
        while (true) 
        {
            effectRenderer.sprite = lsFrames[frameIndex];
            frameIndex = (frameIndex + 1) % lsFrames.Count; // Tăng index và lặp lại khi hết danh sách

            yield return waitTime; // Chờ 0.3 giây trước khi đổi frame tiếp theo
        }
    }

    public void StopAnimation()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }
    }

    public void StartAnimation()
    {
        gameObject.SetActive(true);
        if (animationCoroutine == null)
        {
            animationCoroutine = StartCoroutine(PlayAnimation());
        }
    }

    private void OnDisable()
    {
        StopAnimation();
        StopAllCoroutines();
    }
}