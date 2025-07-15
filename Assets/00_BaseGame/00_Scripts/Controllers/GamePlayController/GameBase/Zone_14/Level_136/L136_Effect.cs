using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class L136_Effect : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public List<Sprite> lsSprites;

    public float moveDuration = 1f; // Thời gian bay
    public float minInitialY = 2f;
    public float maxInitialY = 4f;
    public float horizontalDistance = 2f; // Khoảng cách ngang

    public void Init(Vector3 positionSpawn)
    {
        // Chọn sprite ngẫu nhiên
        int rand = Random.Range(0, lsSprites.Count);
        objRenderer.sprite = lsSprites[rand];

        // Đặt vị trí ban đầu
        transform.position = positionSpawn;

        // Random hướng trái/phải
        float directionX = Random.value > 0.5f ? -1f : 1f;

        // Random độ cao Y
        float targetY = Random.Range(minInitialY, maxInitialY);

        // Tính toán điểm đích
        Vector3 targetPosition = new Vector3(
            positionSpawn.x + horizontalDistance * directionX,
            positionSpawn.y + targetY,
            positionSpawn.z
        );

        // Di chuyển bằng DOTween
        transform.DOMove(targetPosition, moveDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                SimplePool2.Despawn(gameObject);
            });
    }
}