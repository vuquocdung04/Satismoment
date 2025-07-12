using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L129_Thief : MonoBehaviour
{
    public L129_Circle currentCirle;
    [SerializeField] private Level_129Ctrl levelCtrl; // Tham chiếu đến controller để lấy police
    [SerializeField] private SpriteRenderer thiefRenderer; // Renderer của Thief
    [SerializeField] private List<Sprite> lsFrames; // Các frame hoạt hình

    private Coroutine animateCoroutine;
    private List<L129_Circle> validNeighbors = new List<L129_Circle>();

    public void Moving()
    {
        validNeighbors.Clear();
        foreach (var neighbor in currentCirle.lsNeighBor)
        {
            if (neighbor != levelCtrl.police.currentCirle)
            {
                validNeighbors.Add(neighbor);
            }
        }

        if (validNeighbors.Count == 0)
        {
            Debug.Log("Không còn nước đi hợp lệ cho Thief.");
            return;
        }

        // Chọn ngẫu nhiên từ danh sách hợp lệ
        int rand = Random.Range(0, validNeighbors.Count);
        Vector3 targetMove = validNeighbors[rand].transform.position;

        // Bắt đầu di chuyển
        var moveTween = transform.DOMove(targetMove, 0.5f).SetEase(Ease.Linear);

        // Bắt đầu hiệu ứng đổi sprite
        if (animateCoroutine != null)
            StopCoroutine(animateCoroutine);
        animateCoroutine = StartCoroutine(AnimateThief());

        // Dừng hiệu ứng khi di chuyển xong
        moveTween.OnComplete(() =>
        {
            if (animateCoroutine != null)
            {
                StopCoroutine(animateCoroutine);
                animateCoroutine = null;
            }

            Debug.Log("Thief Move Complete");
            // Có thể đặt lại sprite mặc định nếu cần
            // thiefRenderer.sprite = lsFrames[0];
        });

        currentCirle = validNeighbors[rand];
    }

    IEnumerator AnimateThief()
    {
        int frameIndex = 0;
        var waitTime = new WaitForSeconds(0.1f);
        while (true)
        {
            thiefRenderer.sprite = lsFrames[frameIndex];
            frameIndex = (frameIndex + 1) % lsFrames.Count;
            yield return waitTime;
        }
    }

    private void OnDestroy()
    {
        animateCoroutine = null;
        StopAllCoroutines();
    }
}