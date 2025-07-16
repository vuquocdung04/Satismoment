using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L138_Effect : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public List<Sprite> lsFrames;

    [SerializeField] private float frameRate = 0.3f; // thời gian giữa các frame

    private int currentFrame = 0;
    private Coroutine animationCoroutine;

    void Start()
    {
        if (lsFrames.Count > 0 && objRenderer != null)
        {
            // Bắt đầu hiệu ứng đổi sprite
            animationCoroutine = StartCoroutine(PlayAnimation());
        }
    }

    IEnumerator PlayAnimation()
    {
        var waitTime = new WaitForSeconds(frameRate);
        while (true)
        {
            // Kiểm tra nếu có frame để hiển thị
            if (lsFrames.Count > 0)
            {
                objRenderer.sprite = lsFrames[currentFrame];
                currentFrame = (currentFrame + 1) % lsFrames.Count;
            }

            yield return waitTime;
        }
    }

    public void StopAnimation()
    {
        StopAllCoroutines();
        animationCoroutine = null;
        gameObject.SetActive(false);
    }
}