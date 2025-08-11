using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_80Ctrl : BaseDragController<L80_toothBrush>
{
    public L80_ToothpasteFoam toothpasteFoamPrefab; // Prefab foam bạn sẽ spawn
    public Transform foamParent; // Dùng để chứa các foam đã spawn (tùy chọn)

    private bool canSpawnFoam = true; // Cờ cho phép spawn
    public float spawnInterval = 0.5f; // Khoảng cách giữa các lần spawn (giây)

    public bool isComplete = false;
    protected override void OnDragEnded()
    {
        canSpawnFoam = true;
        StartCoroutine(HandleWinCodition());
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
        draggableComponent.DrawAtPosition(currentMousePosition + Vector3.left);

        // Gọi Spawn có giới hạn
        if (canSpawnFoam)
        {
            SpawnFoamEffect();
            canSpawnFoam = false;

            // Bắt đầu coroutine để cho phép spawn lại sau 0.5s
            StartCoroutine(EnableSpawnAfterDelay());
        }
    }

    protected override void OnDragStarted()
    {
        // Có thể để trống hoặc thêm logic nếu cần
    }

    void SpawnFoamEffect()
    {
        if (toothpasteFoamPrefab == null) return;

        // Tạo foam tại vị trí bàn chải
        L80_ToothpasteFoam foamInstance = SimplePool2.Spawn(
            toothpasteFoamPrefab,
            draggableComponent.transform.position + Vector3.left,
            Quaternion.identity
        );

        foamInstance.transform.SetParent(foamParent);
    }

    IEnumerator EnableSpawnAfterDelay()
    {
        yield return new WaitForSeconds(spawnInterval);
        canSpawnFoam = true;
    }

     IEnumerator HandleWinCodition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.3f);
        WinBox.SetUp().Show();
    }
}