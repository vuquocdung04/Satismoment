using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class L105_BallArranger : MonoBehaviour
{
    public L105_ObjBall cubeBallRedPrefab; // Prefab của quả bóng đỏ
    public L105_ObjBall cueBall8;
    public int totalBalls = 15; // Tổng số bóng
    public float ballRadius = 0.5f; // Bán kính của mỗi quả bóng

    private List<L105_ObjBall> balls = new List<L105_ObjBall>(); // Danh sách lưu trữ các quả bóng đã tạo

    public void Init()
    {
        // Xóa tất cả các quả bóng cũ nếu có
        foreach (var ball in balls)
        {
            ball.transform.DOKill();
            SimplePool2.Despawn(ball.gameObject);
            ball.ResetState();
        }
        balls.Clear();

        // Tạo và sắp xếp các quả bóng
        ArrangeBallsInInvertedTriangle();
    }
    void ArrangeBallsInInvertedTriangle()
    {
        // Tính toán số hàng cần thiết
        int rows = 0;
        while ((rows * (rows + 1)) / 2 < totalBalls)
        {
            rows++;
        }

        // Vị trí bắt đầu với Y = 4 (đỉnh tam giác)
        Vector3 startPosition = new Vector3(transform.position.x, 1.75f, transform.position.z);

        // Khoảng cách giữa các bóng
        float distanceBetweenBalls = 2 * ballRadius;

        // Chiều cao giữa các hàng trong tam giác đều
        float rowHeight = distanceBetweenBalls * Mathf.Sqrt(3) / 2;

        // Biến để đếm số bóng đã đặt
        int ballCount = 0;

        // Sắp xếp từng hàng (từ hàng có nhiều bóng nhất đến ít nhất - tam giác ngược)
        for (int row = rows; row >= 1 && ballCount < totalBalls; row--)
        {
            int ballsInRow = row;

            // Canh giữa hàng theo trục X
            float offsetX = -(ballsInRow - 1) * distanceBetweenBalls / 2;

            // Tính vị trí Y: đỉnh ở y = 4, sau đó giảm dần khi đi xuống
            float offsetY = (rows - row) * rowHeight;

            // Đặt từng bóng trong hàng
            for (int i = 0; i < ballsInRow && ballCount < totalBalls; i++)
            {
                Vector3 position = new Vector3(
                    startPosition.x + offsetX + i * distanceBetweenBalls,
                    startPosition.y - offsetY,
                    startPosition.z
                );

                L105_ObjBall ball;
                if (ballCount == 10)
                {
                    // Tạo cueBall8 tại vị trí thứ 10
                    ball = SimplePool2.Spawn(cueBall8, position, Quaternion.identity);
                    ball.name = "Cue Ball 8";
                }
                else
                {
                    // Tạo bi đỏ bình thường
                    ball = SimplePool2.Spawn(cubeBallRedPrefab, position, Quaternion.identity);
                    ball.name = "Ball Red_" + ballCount;
                }

                balls.Add(ball);
                ballCount++;
            }
        }
    }
}