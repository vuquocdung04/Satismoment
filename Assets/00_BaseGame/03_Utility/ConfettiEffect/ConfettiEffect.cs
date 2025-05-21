using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfettiEffect : MonoBehaviour
{
    public float endPosY = -5f;
    public Image icon;
    public CanvasGroup canvasGroup;
    [SerializeField] RectTransform childObj;
    public void SetSpriteIcon(List<Sprite> lsSprites)
    {
        int rand = Random.Range(0, lsSprites.Count);
        icon.sprite = lsSprites[rand];
    }

    public void SetRandomFallEffect()
    {
        childObj.DOKill();
        canvasGroup.DOKill();
        canvasGroup.alpha = 1.0f;

        // Random hướng rơi nghiêng và khoảng cách
        float offsetX = Random.Range(-200f, 200f); // Lệch trái/phải
        float offsetY = Random.Range(-400f, -600f); // Xuống dưới
        Vector2 targetOffset = new Vector2(offsetX, offsetY);
        Vector3 targetPos = childObj.anchoredPosition + (Vector2)targetOffset;

        float fallTime = Random.Range(1.5f, 2.2f);

        childObj.localScale = new Vector3(2f,2f,2f);
        // Tween rơi theo hướng random
        childObj.DOAnchorPos(targetPos, fallTime)
            .SetEase(Ease.InQuad);

        // Tween xoay nhẹ
        float rotateAmount = Random.Range(90f, 360f);
        childObj.DORotate(new Vector3(0, 0, rotateAmount), fallTime, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear);

        // Mờ dần
        canvasGroup.DOFade(0f, fallTime)
            .SetEase(Ease.InQuad)
            .OnComplete(() => gameObject.SetActive(false));
    }




}
