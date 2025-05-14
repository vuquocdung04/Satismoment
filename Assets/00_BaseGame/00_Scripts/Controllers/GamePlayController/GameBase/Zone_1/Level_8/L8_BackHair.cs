using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L8_BackHair : LoadAutoComponents
{
    [Header("Fall Settings")]
    public float fallDuration = 1.2f;          // Thời gian lông rơi (giây)
    public float fallDistanceY = 1.5f;         // Khoảng cách rơi xuống theo trục Y
    public float horizontalSwayRange = 0.3f;   // Phạm vi lông lắc lư ngang khi rơi
    public Ease fallEaseType = Ease.InQuad;

    [Header("Rotation Settings")]
    public bool enableRotation = true;         // Bật/tắt xoay khi rơi
    public float rotationAmount = 180f;        // Độ xoay tối đa (ngẫu nhiên giữa -rotationAmount và +rotationAmount)
    public float rotationDurationMultiplier = 1f; // Thời gian xoay (so với thời gian rơi)

    [Header("Fade Settings")]
    public bool enableFadeOut = true;          // Bật/tắt mờ dần khi rơi
    public float fadeOutDelay = 0.5f;          // Thời gian chờ trước khi bắt đầu mờ dần
    public float fadeOutDuration = 0.7f;       // Thời gian để mờ hẳn


    public CapsuleCollider2D hairCollider;
    public SpriteRenderer sr;

    public void TriggerFall()
    {
        // Vô hiệu hóa collider để không bị trigger nhiều lần hoặc tương tác không mong muốn
        if (hairCollider != null)
        {
            hairCollider.enabled = false;
        }

        // --- Tạo Sequence của DOTween ---
        Sequence fallSequence = DOTween.Sequence();

        // 1. Chuyển động rơi (Move)
        float targetX = transform.position.x + Random.Range(-horizontalSwayRange, horizontalSwayRange);
        float targetY = transform.position.y - fallDistanceY;
        fallSequence.Append(
            transform.DOMove(new Vector3(targetX, targetY, transform.position.z), fallDuration)
                .SetEase(fallEaseType)
        );

        // 2. Xoay (Rotate) - Chạy song song với chuyển động rơi
        if (enableRotation)
        {
            float randomRotation = Random.Range(-rotationAmount, rotationAmount);
            fallSequence.Join( // Join để chạy song song với tween trước đó (DOMove)
                transform.DORotate(new Vector3(0, 0, randomRotation), fallDuration * rotationDurationMultiplier, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear) // Xoay đều
            );
        }

        // 3. Mờ dần (FadeOut) - Chạy sau một khoảng delay
        if (enableFadeOut)
        {
            if (sr != null)
            {
                fallSequence.Insert(fadeOutDelay, // Bắt đầu tween này sau fadeOutDelay giây
                    sr.DOFade(0f, fadeOutDuration)
                        .SetEase(Ease.InQuad)
                );
            }
        }

        // 4. Hủy GameObject sau khi tất cả các hiệu ứng hoàn thành
        fallSequence.OnComplete(() => {
            Destroy(gameObject);
        });

        // Bắt đầu chạy sequence
        fallSequence.Play();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        hairCollider = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

}
