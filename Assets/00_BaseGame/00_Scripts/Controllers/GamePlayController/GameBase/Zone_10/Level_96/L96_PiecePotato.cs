using UnityEngine;
using DG.Tweening;

public class L96_PiecePotato : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    [Tooltip("Thời gian mỗi miếng khoai rơi xuống")]
    public float fallDuration = 1f;

    [Tooltip("Độ trễ xuất hiện/ngẫu nhiên trước khi rơi")]
    public float delayRange = 0.5f;

    [Tooltip("Góc xoay tối đa trong lúc rơi")]
    public float rotationRange = 180f;

    public Transform targetPosition;
    public void FallDown()
    {
        // Di chuyển xuống dưới
        transform.DOMove(targetPosition.position, fallDuration)
            .SetEase(Ease.OutBack); // Hiệu ứng rơi tự nhiên

        // Xoay quanh trục Z một góc ngẫu nhiên
        float randomRotation = Random.Range(-rotationRange, rotationRange);
        transform.DORotate(new Vector3(0, 0, randomRotation), fallDuration)
            .SetEase(Ease.Linear);
    }
}