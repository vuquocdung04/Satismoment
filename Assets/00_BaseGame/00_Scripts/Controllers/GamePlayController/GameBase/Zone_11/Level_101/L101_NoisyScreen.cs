using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L101_NoisyScreen : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> lsScreenFrames;
    public bool isPlayingAnimation = false; 

    private float animationInterval = 0.2f;
    private Color currentColor;
    private float lastAlpha = -1f; // Lưu giá trị alpha trước để so sánh


    public IEnumerator PlayingAnimation()
    {
        int index = 0;
        var wait = new WaitForSeconds(animationInterval);

        while (!isPlayingAnimation)
        {
            spriteRenderer.sprite = lsScreenFrames[index];
            index = (index + 1) % lsScreenFrames.Count;

            yield return wait;
        }
    }

    // Chỉ cập nhật alpha nếu khác với giá trị hiện tại
    public void SetAlpha(float alpha)
    {
        if (spriteRenderer == null) return;

        // Làm tròn alpha đến 3 chữ số để tránh sai số nhỏ do float
        alpha = Mathf.Round(alpha * 1000f) / 1000f;

        if (alpha == lastAlpha) return;

        currentColor = spriteRenderer.color;
        currentColor.a = alpha;
        spriteRenderer.color = currentColor;

        lastAlpha = alpha;
    }

}