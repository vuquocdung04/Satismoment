
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class L98_Fruit : MonoBehaviour
{
    public CircleCollider2D circleCollider2D;
    public List<Transform> lsChilds;

    private Sequence throwSequence; // Lưu sequence DOTween
    private Vector3 initialPosition; // Lưu vị trí ban đầu của quả táo
    public Level_98Ctrl levelCtrl;
    public void Init()
    {
        initialPosition = transform.position; // Lưu vị trí ban đầu khi game bắt đầu
        ThrowToRandomAndBackLoop();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ⚠️ Kill DOTween nếu đang chạy hiệu ứng bay
        if (throwSequence != null)
        {
            throwSequence.Kill(); // Hủy hiệu ứng lặp
            throwSequence = null;
        }
        levelCtrl.PlayHitSound();
        // Vô hiệu hóa collider
        circleCollider2D.enabled = false;

        // Bắt đầu hiệu ứng văng mảnh
        ExplodeFruit();
    }

    void ExplodeFruit()
    {
        int numPieces = lsChilds.Count;

        for (int i = 0; i < numPieces; i++)
        {
            Transform piece = lsChilds[i];

            Vector3 randomDirection = Random.insideUnitCircle * 3f;

            piece.DOMove(piece.position + randomDirection, 0.4f)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    piece.DOJump(piece.position + new Vector3(0, -10f, 0), 1f, 1, 1f)
                        .SetEase(Ease.Linear);
                });

            piece.DORotate(new Vector3(0, 0, Random.Range(-360f, 360f)), 1f)
                .SetEase(Ease.Linear);
        }
    }

    // Hàm lấy tọa độ đích ngẫu nhiên quanh vị trí ban đầu
    private Vector3 GetRandomTargetPosition()
    {
        // Tính toán vị trí ngẫu nhiên so với vị trí ban đầu của quả táo
        float randomX = Random.Range(-2f, 2f);
        float randomY = Random.Range(1f, 3f);
        return initialPosition + new Vector3(randomX, randomY + 7f, 0f);
    }
    public void ThrowToRandomAndBackLoop()
    {
        throwSequence = DOTween.Sequence()
            .Append(transform.DOMove(GetRandomTargetPosition(), 1f).SetEase(Ease.OutQuad))
            .Join(transform.DORotate(new Vector3(0, 0, Random.Range(-360f, 360f)), 1f).SetEase(Ease.Linear))

            .Append(transform.DOJump(transform.position + Vector3.down * 2f, 1f, 1, 1f)
                .SetEase(Ease.InQuad))
            // Đồng thời quay thêm khi rơi xuống
            .Join(transform.DORotate(new Vector3(0, 0, Random.Range(-360f, 360f)), 1f)
                .SetEase(Ease.Linear))

            // 3. Nghỉ 0.2 giây trước khi bắt đầu lại
            .AppendInterval(0.2f)

            // Khi lặp lại, đảm bảo quả táo trở về vị trí ban đầu trước khi bắt đầu chu kỳ mới
            .OnStepComplete(() =>
            {
                Debug.LogError("Test");
                transform.position = initialPosition;
                ThrowToRandomAndBackLoop();
            });
    }
}