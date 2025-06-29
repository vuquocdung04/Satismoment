using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L97_DeviceAnim : MonoBehaviour
{
    [Tooltip("Kích hoạt để bắt đầu animation")]
    public bool playAnimation = false; // Thay thế isAnimated bằng playAnimation để dễ hiểu hơn

    public SpriteRenderer spriteRenderer;
    public List<Sprite> spriteFrames; // Đổi tên từ lsSprite → spriteFrames

    private int currentFrameIndex = 0;
    public IEnumerator PlayAnimationCoroutine()
    {
        var waitTime = new WaitForSeconds(0.1f);

        while (playAnimation)
        {
            spriteRenderer.sprite = spriteFrames[currentFrameIndex];
            currentFrameIndex = (currentFrameIndex + 1) % spriteFrames.Count;
            yield return waitTime;
        }
    }

    public void StartAnimation()
    {
        gameObject.SetActive(true);
        playAnimation = true;
        StartCoroutine(PlayAnimationCoroutine());
    }
    public void StopAnimation()
    {
        playAnimation = false;
        gameObject.SetActive(false);
    }
}