using UnityEngine;
using DG.Tweening;

public class L123_CatHair : MonoBehaviour
{
    [SerializeField] private float limitX = 3f; // Giới hạn X là ±3
    [SerializeField] private float moveDuration = 0.8f; // Thời gian mỗi bước di chuyển
    [SerializeField] private int totalMoves = 5; // Số lần di chuyển

    private int moveCount = 0;
    private Vector3 currentTargetPosition;

    public void Init()
    {
        // Chọn ngẫu nhiên giữa -3 và 3 làm hướng đầu tiên
        currentTargetPosition = new Vector3(Random.Range(0, 2) == 0 ? -limitX : limitX, transform.position.y, 0);

        MoveToCurrentTarget();
    }

    private void MoveToCurrentTarget()
    {
        // Dùng DOTween để di chuyển đến vị trí mục tiêu (X và Y)
        transform.DOMove(currentTargetPosition, moveDuration)
            .SetEase(Ease.InOutSine) // Hiệu ứng mượt
            .OnComplete(() =>
            {
                // Sau khi di chuyển xong, giảm Y xuống 1 đơn vị
                transform.position += Vector3.down * 1f;

                // Đảo chiều cho bước tiếp theo
                currentTargetPosition.x *= -1;

                // Tăng bộ đếm bước
                moveCount++;

                // Tiếp tục nếu chưa đủ số lần
                if (moveCount < totalMoves)
                {
                    MoveToCurrentTarget();
                }
            });
    }
}