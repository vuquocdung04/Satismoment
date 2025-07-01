using UnityEngine;
using System.Collections;
public class L105_CueBall : MonoBehaviour
{
    public Rigidbody2D rb;

    public void ApplyStrikeForce(Vector2 direction, float power)
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        rb.AddForce(direction * power, ForceMode2D.Impulse);

        // Bắt đầu coroutine kiểm tra xem bóng có dừng không
        StartCoroutine(CheckAndStopBall());
    }

    private IEnumerator CheckAndStopBall(float checkInterval = 0.2f)
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            // Kiểm tra nếu vận tốc và xoay đều rất nhỏ
            if (rb.velocity.magnitude < 1f)
            {
                // Dừng hoàn toàn quả bóng
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;

                // Thoát khỏi vòng lặp
                break;
            }
        }

        // Có thể thêm logic gì đó ở đây, ví dụ: báo hiệu bóng đã dừng
        OnBallStopped();
    }

    private void OnBallStopped()
    {
        Debug.Log("Quả bóng đã dừng lại!");
        // Ví dụ: Gọi event hoặc kích hoạt hành động tiếp theo trong game
    }
}