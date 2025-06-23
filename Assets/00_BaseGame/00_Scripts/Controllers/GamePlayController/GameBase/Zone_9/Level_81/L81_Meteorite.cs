using UnityEngine;
using DG.Tweening;

public class L81_Meteorite : MonoBehaviour
{
    public float moveSpeed = 10f;

    // Hàm dùng để khởi tạo hướng bay từ bên ngoài
    public void Init(Vector3 targetPosition)
    {
        // Tính hướng từ vị trí hiện tại đến chuột
        Vector2 direction = (targetPosition - transform.position).normalized;

        // Tính góc xoay
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Xoay thiên thạch (giả sử sprite ban đầu hướng lên)
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        // Di chuyển theo hướng đã xoay
        transform.DOMove(transform.position + transform.up * 15f, moveSpeed)
            .SetSpeedBased()
            .SetEase(Ease.Linear).OnComplete(delegate
            {
                SimplePool2.Despawn(gameObject);
            });
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}