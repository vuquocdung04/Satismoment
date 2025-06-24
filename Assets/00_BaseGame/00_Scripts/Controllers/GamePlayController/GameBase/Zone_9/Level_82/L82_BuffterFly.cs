using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L82_BuffterFly : MonoBehaviour
{
    public float durationFly = 3f;             // Tổng thời gian bay qua 4 điểm
    public SpriteRenderer spriteRenderer;
    public List<Transform> lsPoints;          // Danh sách 4 điểm: point0 -> point3
    public List<Sprite> lsSpriteFlys;         // 2 sprite để vỗ cánh

    public bool isCompleteFly;                // Biến kiểm tra đã bay xong chưa

    public void DoFlying()
    {
        Vector3[] path = new Vector3[]
        {
            lsPoints[0].position,
            lsPoints[1].position,
            lsPoints[2].position,
            lsPoints[3].position
        };
        Tween pathTween = transform.DOPath(path, durationFly, PathType.CatmullRom)
                 .SetEase(Ease.Linear);
        pathTween.OnComplete(() =>
        {
            isCompleteFly = true;
        });

        // Bắt đầu hiệu ứng vỗ cánh
        StartCoroutine(HandleAnimFly());
    }

    IEnumerator HandleAnimFly()
    {
        int index = 0;
        float interval = 0.1f;
        var waitTime = new WaitForSeconds(interval);

        while (!isCompleteFly)
        {
            // Nếu đến gần điểm cuối cùng thì dừng hiệu ứng
            if (Vector3.Distance(transform.position, lsPoints[3].position) < 0.1f)
            {
                spriteRenderer.sprite = lsSpriteFlys[0]; // giữ nguyên hình đầu tiên
                yield break;
            }

            // Đổi sprite liên tục
            spriteRenderer.sprite = lsSpriteFlys[index];
            index = (index + 1) % lsSpriteFlys.Count;

            yield return waitTime;
        }
    }
}