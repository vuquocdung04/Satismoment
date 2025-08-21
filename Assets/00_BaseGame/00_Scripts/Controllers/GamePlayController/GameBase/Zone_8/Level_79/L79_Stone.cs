using DG.Tweening;
using UnityEngine;

public class L79_Stone : MonoBehaviour
{
    public void ThrowStone(Vector3 target,System.Action callback = null, float duration = 0.4f)
    {
        // Di chuyển viên đá đến vị trí target
        transform.DOMove(target, duration)
            .SetEase(Ease.OutQuad).OnComplete(delegate
            {
                callback?.Invoke();
            }); // Hiệu ứng bay mượt hơn

        // Thu nhỏ kích thước đồng thời với việc di chuyển
        transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), duration)
            .SetEase(Ease.InQuad) // Có thể dùng Ease.Linear nếu muốn đều
            .OnComplete(() =>
            {
                SimplePool2.Despawn(gameObject);
                transform.localScale = Vector3.one;
            });
    }
}
