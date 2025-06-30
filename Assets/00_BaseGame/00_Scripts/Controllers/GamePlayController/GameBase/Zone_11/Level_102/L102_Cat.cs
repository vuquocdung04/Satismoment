using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class L102_Cat : MonoBehaviour
{
    public List<Transform> lsPoints; // Danh sách các điểm xuất hiện

    private float moveDuration = 0.5f; // Thời gian di chuyển mỗi lần nhô ra/về
    private float appearDelay = 2f;    // Thời gian chờ trước khi xuất hiện lần đầu
    private float waitBetweenPeek = 1f; // Thời gian chờ giữa các lần peek
    private int repeatCount = 2;       // Số lần lặp lại hành động "nhô ra"

    private bool isPeeking = false;    // Trạng thái đang nhô ra hay không

    void Start()
    {
        StartCoroutine(AppearRandomly());
    }
    IEnumerator AppearRandomly()
    {
        yield return new WaitForSeconds(appearDelay);

        while (true)
        {
            // Chọn ngẫu nhiên một điểm trong danh sách
            int randomIndex = Random.Range(0, lsPoints.Count);
            Transform targetPoint = lsPoints[randomIndex];

            // Đảo chiều sprite nếu cần
            if (randomIndex != 0)
            {
                FlipSprite(-1);
            }
            else
            {
                FlipSprite(1);
            }

            // Di chuyển đến điểm đó
            transform.position = targetPoint.position;

            // Thực hiện animation peek nhiều lần
            for (int i = 0; i < repeatCount; i++)
            {
                Vector3 startPosition = transform.position;
                Vector3 endPosition;

                // Xác định hướng nhô ra dựa trên vị trí
                if (randomIndex == 0)
                {
                    endPosition = startPosition + new Vector3(0.5f, 0f, 0f); // Nhô phải
                }
                else
                {
                    endPosition = startPosition + new Vector3(-0.5f, 0f, 0f); // Nhô trái
                }

                // Cập nhật trạng thái isPeeking = true
                isPeeking = true;
                Debug.LogError("Dang nho");
                // Di chuyển ra ngoài (nhô ra)
                transform.DOMove(endPosition, moveDuration).SetEase(Ease.OutQuad);
                yield return new WaitForSeconds(moveDuration);

                // Cập nhật trạng thái isPeeking = false
                isPeeking = false;
                Debug.LogError("Het nho");

                // Di chuyển trở lại vị trí cũ
                transform.DOMove(startPosition, moveDuration).SetEase(Ease.InQuad);
                yield return new WaitForSeconds(moveDuration);


                yield return new WaitForSeconds(waitBetweenPeek);
            }
            yield return new WaitForSeconds(appearDelay);
        }
    }
    private void FlipSprite(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = direction;
        transform.localScale = scale;
    }

    // Ví dụ: hàm kiểm tra trạng thái từ bên ngoài
    public bool IsPeeking()
    {
        return isPeeking;
    }
}