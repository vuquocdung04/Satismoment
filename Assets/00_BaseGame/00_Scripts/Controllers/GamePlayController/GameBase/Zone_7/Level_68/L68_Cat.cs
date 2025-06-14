using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L68_Cat : MonoBehaviour
{
    public Level_68Ctrl levelCtrl;
    public Transform zoneCheck;
    public Transform mouseTransform;
    public LayerMask obstacleLayer;

    public float timeChecked = 3f;
    public float timeReset = 3f;
    public SpriteRenderer eyeRenderer;
    public List<Sprite> lsSprite;
    private Tween currentMoveTween; // Biến lưu DOTween hiện tại
    private bool isCountDown = false;

    private void Update()
    {
        if (levelCtrl.isWin || isCountDown) return;

        timeChecked -= Time.deltaTime;
        if (timeChecked < 0)
        {
            timeChecked = timeReset;
            StartCoroutine(HandleCheck());
        }
    }

    IEnumerator HandleCheck()
    {
        isCountDown = true;
        hasDetectedMouse = false;

        // Di chuyển xuống
        currentMoveTween = transform.DOMoveY(-2f, 0.5f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1f);

        // Di chuyển lên
        currentMoveTween = transform.DOMoveY(0f, 1f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1.1f);

        // Bật vùng kiểm tra và bắt đầu raycast
        zoneCheck.gameObject.SetActive(true);
        eyeRenderer.sprite = lsSprite[0];

        // Kiểm tra tầm nhìn trong 1s
        StartCoroutine(CheckForMouseContinuously(1f));

        yield return new WaitForSeconds(1f); // Giữ mắt mở 1s

        zoneCheck.gameObject.SetActive(false);

        if (!hasDetectedMouse)
        {
            // Chỉ di chuyển xuống nếu KHÔNG phát hiện chuột
            currentMoveTween = transform.DOMoveY(-3f, 1f).SetEase(Ease.Linear);
        }

        isCountDown = false;
    }

    IEnumerator CheckForMouseContinuously(float duration)
    {
        float timer = 0f;
        float checkInterval = 0.2f;
        var waiTime = new WaitForSeconds(checkInterval);
        while (timer < duration)
        {
            CheckMouseVisibility();
            timer += checkInterval;
            yield return waiTime;
        }
    }

    private bool hasDetectedMouse = false;

    void CheckMouseVisibility()
    {
        Vector2 direction = (mouseTransform.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, mouseTransform.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, obstacleLayer);
        Debug.DrawRay(transform.position, direction * distance, Color.red, 0.3f);

        if (hit.collider != null && hit.collider.transform == mouseTransform)
        {
            if (!hasDetectedMouse)
            {
                eyeRenderer.sprite = lsSprite[1];
                Debug.LogError("Phát hiện chuột!");

                // Dừng toàn bộ DOTween của mèo
                if (currentMoveTween != null)
                {
                    currentMoveTween.Kill();
                }

                // Gọi hàm xử lý thua
                StartCoroutine(levelCtrl.HandleLoseGame());

                hasDetectedMouse = true;
            }
        }
    }

    public void StopCatLogic()
    {
        // Dừng tất cả DOTween của mèo
        if (currentMoveTween != null)
        {
            currentMoveTween.Kill();
            currentMoveTween = null;
        }
        StopAllCoroutines();

    }

    private void OnDestroy()
    {
        StopCatLogic();
    }
}