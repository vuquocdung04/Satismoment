using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L87_Buffterfly : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> lsSprite;
    public List<Transform> lsPointMoves;

    public bool reachedEnd = false;

    public void DoFlying()
    {
        reachedEnd = false;

        StartCoroutine(FlipSpriteLoop());
        StartCoroutine(MoveThroughPoints());
    }

    IEnumerator FlipSpriteLoop()
    {
        int spriteIndex = 0;
        float interval = 0.5f; // thời gian giữa các lần đổi sprite

        var waitTime = new WaitForSeconds(interval);

        while (!reachedEnd) // chỉ chạy khi chưa tới đích
        {
            spriteRenderer.sprite = lsSprite[spriteIndex];
            spriteIndex = (spriteIndex + 1) % lsSprite.Count;

            yield return waitTime;
         }

        // Khi reachedEnd == true => dừng lại ở sprite cuối
        spriteRenderer.sprite = lsSprite[spriteIndex]; // giữ nguyên sprite cuối
    }

    IEnumerator MoveThroughPoints()
    {
        // Tạo danh sách vị trí cần bay tới
        List<Vector3> path = new List<Vector3>();
        path.Add(transform.position); // vị trí ban đầu

        foreach (Transform point in lsPointMoves)
        {
            path.Add(point.position);
        }

        // Thời gian tổng cộng để bay hết hành trình
        float totalDuration = 5f;

        // Bay theo đường cong mượt (AutoControlPoints)
        Sequence sequence = DOTween.Sequence();

        Tween moveTween = transform.DOPath(
            path.ToArray(),
            totalDuration,
            PathType.CatmullRom,     // Đường cong mượt
            PathMode.Full3D,
            10,                      // Độ phân giải đường cong
            Color.green
        ).SetEase(Ease.InOutSine);

        // Thêm hiệu ứng xoay theo hướng di chuyển
        Tween rotateTween = transform.DOLocalRotate(new Vector3(0, 0, -90), 0.01f); // Giữ mặc định nếu không cần điều chỉnh

        // Kết hợp vào sequence để đồng bộ
        sequence.Append(moveTween);

        // Tự động xoay theo hướng di chuyển trong suốt hành trình
        StartCoroutine(AutoRotateAlongPath(path, totalDuration));

        yield return new WaitForSeconds(totalDuration);

        // Sau khi hoàn tất
        reachedEnd = true;
        transform.DORotate(Vector3.zero, 0.3f);
    }

    IEnumerator AutoRotateAlongPath(List<Vector3> path, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Tính toán hướng di chuyển tức thời giữa 2 điểm gần nhất
            float t = elapsedTime / duration;
            int segment = Mathf.Min(Mathf.FloorToInt(t * (path.Count - 1)), path.Count - 2);
            Vector3 current = path[segment];
            Vector3 next = path[segment + 1];

            Vector3 direction = (next - current).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            transform.rotation = Quaternion.Euler(0, 0, angle);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Đảm bảo góc cuối cùng đúng
        Vector3 finalDir = (path[^1] - path[^2]).normalized;
        float finalAngle = Mathf.Atan2(finalDir.y, finalDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, finalAngle);
    }
}