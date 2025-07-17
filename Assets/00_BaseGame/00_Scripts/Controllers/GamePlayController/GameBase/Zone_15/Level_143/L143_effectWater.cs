using UnityEngine;
using DG.Tweening;

public class L143_effectWater : MonoBehaviour
{
    public float lifeTime = 0.5f;
    public void Init()
    {
            float randX = Random.Range(-0.5f, 0.5f);
            float randY = Random.Range(1f, 1.5f);
            Vector3 endPosition = new Vector3(randX, randY, 0);
        transform.DOMove(transform.position + endPosition, lifeTime)
            .SetEase(Ease.OutQuad) // Hiệu ứng bay lên rồi rơi xuống nhẹ nhàng
            .OnComplete(() => SimplePool2.Despawn(gameObject));
    }
}