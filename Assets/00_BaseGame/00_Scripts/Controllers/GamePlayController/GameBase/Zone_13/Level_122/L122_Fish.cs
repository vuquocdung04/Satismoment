using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L122_Fish : MonoBehaviour
{
    
    public SpriteRenderer objRenderer; // Renderer của đối tượng cá
    public List<Sprite> lsSprite;      // Danh sách các sprite cần đổi liên tục

    [SerializeField] private float frameRate = 0.2f; // Tốc độ đổi frame (giây)
    public float limitMoveX = 5f;     // Giới hạn di chuyển theo trục X
    public float moveDuration = 2f;   // Thời gian di chuyển từ trái sang phải (hoặc ngược lại)

    private int currentFrame = 0;
    private bool isMovingRight = true; // Hướng di chuyển hiện tại

    bool isChangeSprite = false;
    
    public void InitState()
    {
        StartCoroutine(ChangeSprite());
        FishMoving();
    }


    IEnumerator ChangeSprite()
    {
        var waitTime = new WaitForSeconds(frameRate);
        while (!isChangeSprite)
        {
            if (lsSprite.Count > 0)
            {
                objRenderer.sprite = lsSprite[currentFrame];
                currentFrame = (currentFrame + 1) % lsSprite.Count;
            }
            yield return waitTime;
        }
    }

    void FishMoving()
    {
        // Xác định đích di chuyển: trái hoặc phải, dựa vào isMovingRight
        float targetX = isMovingRight ? limitMoveX : -limitMoveX;

        // Di chuyển theo trục X tới targetX
        transform.DOLocalMoveX(targetX, moveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // Đảo hướng và lật scale khi đến đích
                isMovingRight = !isMovingRight;
                Vector3 newScale = transform.localScale;
                newScale.x = isMovingRight ? 1f : -1f;
                transform.localScale = newScale;

                // Gọi lại FishMoving để tiếp tục di chuyển ngược lại
                FishMoving();
            });
    }

    public void StopAll()
    {
        transform.DOKill();
        isChangeSprite = true;
    }
}