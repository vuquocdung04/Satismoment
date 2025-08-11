using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_97Ctrl : BaseDragController<L97_Devices>
{
    public int waterDropCount;
    public L97_Water water;
    public Transform posSpawn;
    public Transform T_ShirtWet;
    public float waterSpawnInterval = 0.5f; // Khoảng cách giữa các lần spawn nước
    private float lastWaterSpawnTime; // Thời điểm spawn nước cuối cùng
    public List<Transform> effectStar;
    public List<L97_Devices> lsDevices;

    private float lastApplyTime;
    public float applyInterval = 0.05f; // Thời gian tối thiểu giữa các lần áp dụng texture (ví dụ: 50ms)

    protected override void OnDragEnded()
    {
        draggableComponent.OnEndDrag();
        if(draggableComponent.deviceType == L97_DeviceType.SteamIron)
        draggableComponent.ApplyMaskChangesAndCheckCoverage();

        if (draggableComponent.ninetyPercentReached)
        {
            StartCoroutine(HandleWinCondition());
        }

    }
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
        switch (draggableComponent.deviceType)
        {
            case L97_DeviceType.SteamIron:
                draggableComponent.DrawAtPosition(currentMousePosition);

                // Áp dụng các thay đổi và kiểm tra độ phủ có giới hạn thời gian (throttling)
                if (Time.time - lastApplyTime > applyInterval)
                {
                    draggableComponent.ApplyMaskChangesAndCheckCoverage();
                    lastApplyTime = Time.time;
                }
                break;
            default:
                if (Time.time - lastWaterSpawnTime >= waterSpawnInterval)
                {
                    SpawnWater();
                    lastWaterSpawnTime = Time.time;
                }
                break;
        }
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
        lastApplyTime = Time.time; // Đặt lại thời gian khi bắt đầu kéo
    }

    void SpawnWater()
    {
        var waterClone = SimplePool2.Spawn(water,posSpawn.position,Quaternion.identity);
        StartCoroutine(waterClone.InitEffect());
    }
    private IEnumerator HandleWinCondition()
    {
        isWin = true;
        T_ShirtWet.gameObject.SetActive(false);
        foreach(var star in this.effectStar)
        {
            float randScale = Random.Range(0.3f,0.7f);
            star.transform.DOScale(new Vector3(randScale,randScale,randScale),0.5f);
        }
        yield return new WaitForSeconds(0.8f);
        WinBox.SetUp().Show();
    }

    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        foreach (var device in this.lsDevices)
        {
            device.InitAfter();
            device.InitBefore();
        }
    }
}