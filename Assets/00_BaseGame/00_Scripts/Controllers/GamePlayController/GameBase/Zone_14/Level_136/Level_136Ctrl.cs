using UnityEngine;
using System.Collections;
using DG.Tweening;
public class Level_136Ctrl : BaseDragController<L136_CrankShaft>
{
    public L136_MusicBoxLid musicBoxLid;
    public L136_Effect effectPrefab;
    public L136_Penguin penguin;
    public Transform mask;
    private float totalRotation = 0f; // Góc xoay tích lũy
    private int previousSpriteIndex = -1; // Lưu index trước đó để tránh gọi Set nhiều lần
    protected override void OnDragEnded()
    {
        // Có thể giữ nguyên hoặc reset tùy ý
    }

    float angle;
    Vector3 objectCenter;
    Vector2 vectorToPrevMouse;
    Vector2 vectorToCurrentMouse;

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        objectCenter = draggableComponent.transform.position;
        vectorToPrevMouse = (Vector2)prevMouseWorldPos - (Vector2)objectCenter;
        vectorToCurrentMouse = (Vector2)currentMousePosition - (Vector2)objectCenter;

        angle = Vector2.SignedAngle(vectorToPrevMouse, vectorToCurrentMouse);

        // CHỈ CHO PHÉP XOAY THEO CHIỀU KIM ĐỒNG HỒ (angle < 0)
        if (angle < 0)
        {
            // Cập nhật góc xoay tổng
            float prevRotation = totalRotation;
            totalRotation += Mathf.Abs(angle);

            // Áp dụng xoay cho đối tượng
            draggableComponent.transform.Rotate(0, 0, angle);
            if (Mathf.FloorToInt(totalRotation / 360f) != Mathf.FloorToInt(prevRotation / 360f))
            {
                UpdateMusicBoxLidSprite();
                CheckRotationMilestone(totalRotation);
            }
        }
        else if (angle > 0)
        {
            // KHÔNG XỬ LÝ NẾU XOAY NGƯỢC CHIỀU
            return;
        }
    }

    private void UpdateMusicBoxLidSprite()
    {
        if (musicBoxLid == null || musicBoxLid.lsFrameOpens.Count == 0)
            return;
        Debug.LogError("test");
        int spriteCount = musicBoxLid.lsFrameOpens.Count;
        int maxIndex = spriteCount - 1;

        // Mỗi 360 độ đổi 1 sprite
        int targetIndex = Mathf.FloorToInt(totalRotation / 360f);

        // Giới hạn index trong danh sách sprite
        targetIndex = Mathf.Clamp(targetIndex, 0, maxIndex);

        // Chỉ cập nhật nếu index thay đổi
        if (targetIndex != previousSpriteIndex)
        {
            musicBoxLid.objRenderer.sprite = musicBoxLid.lsFrameOpens[targetIndex];
            Debug.Log($"🖼️ Đổi sprite sang index {targetIndex} (góc: {totalRotation:F0}°)");
            if(targetIndex == 2) mask.gameObject.SetActive(false);
            previousSpriteIndex = targetIndex;
            if (targetIndex == maxIndex)
            {
                isWin = true;
                StartCoroutine(HandleAnimationPenguin());
            }
        }
    }

    private void CheckRotationMilestone(float currentRotation)
    {
        int rotationMod = 360;
        int milestone = Mathf.FloorToInt(currentRotation / rotationMod) * rotationMod;

        if (milestone > 0 && Mathf.Approximately(currentRotation, milestone))
        {
            Debug.Log($"🎯 Đã đạt mốc {milestone} độ!");
        }
    }

    protected override void OnDragStarted()
    {

    }

    IEnumerator HandleAnimationPenguin()
    {
        Tween penguinMove =  penguin.transform.DOMoveY(penguin.transform.position.y + 1f, 0.5f).SetEase(Ease.Linear);
        yield return penguinMove.WaitForCompletion();
        StartCoroutine(penguin.PlayAnimation());
        yield return SpawnEffectsOverTime();
        StartCoroutine(HandleWinCondition());
    }
    IEnumerator SpawnEffectsOverTime()
    {
        int effectCount = 12;
        float interval = 0.15f;
        var waitTime = new WaitForSeconds(interval);
        for (int i = 0; i < effectCount; i++)
        {
            var effectClone = SimplePool2.Spawn(effectPrefab);
            effectClone.Init(penguin.transform.position);

            yield return waitTime;
        }
        
    }

    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}