using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class L96_EffectSmoke : MonoBehaviour
{
    public List<Sprite> lsSprites;         // Danh sách các sprite để chọn ngẫu nhiên

    [Header("Hiệu ứng")]
    public float duration = 1f;            // Thời gian hiệu ứng bay lên
    public float moveDistance = 2f;        // Khoảng cách di chuyển theo trục Y
    public float startScale = 1f;          // Kích thước ban đầu
    public float endScale = 0f;            // Kích thước cuối (nhỏ dần)

    public SpriteRenderer spriteRenderer;

    public void SpawnEffect()
    {
        // Chọn sprite ngẫu nhiên
        if (lsSprites.Count > 0)
        {
            Sprite randomSprite = lsSprites[Random.Range(0, lsSprites.Count)];
            spriteRenderer.sprite = randomSprite;
        }

        // Hiệu ứng DOTween: Bay lên + thu nhỏ + fade out
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMoveY(transform.position.y + moveDistance, duration));
        seq.Join(transform.DOScale(Vector3.one * endScale, duration));

        // Fade out
        Color startColor = spriteRenderer.color;
        seq.Join(spriteRenderer.DOColor(new Color(startColor.r, startColor.g, startColor.b, 0), duration));

        // Tự hủy sau khi hiệu ứng xong
        seq.OnComplete(() => Destroy(gameObject));
    }
}