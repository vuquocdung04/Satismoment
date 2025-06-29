using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L99_CaffeeDrop : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> lsSprites;
    private int indexSprite = 0;
    public bool isCompleteAnimated;
    private Coroutine animationCoroutine;

    // Hàm bắt đầu chạy hoạt ảnh
    public void StartAnimation()
    {
        if (animationCoroutine == null)
        {
            animationCoroutine = StartCoroutine(AnimateDrop());
        }
    }

    private IEnumerator AnimateDrop()
    {
        while (indexSprite < lsSprites.Count)
        {
            spriteRenderer.sprite = lsSprites[indexSprite];
            indexSprite++;

            yield return new WaitForSeconds(0.2f);
        }
        isCompleteAnimated = true;
        // Kết thúc, đặt coroutine về null để có thể chạy lại nếu cần
        animationCoroutine = null;
    }
}