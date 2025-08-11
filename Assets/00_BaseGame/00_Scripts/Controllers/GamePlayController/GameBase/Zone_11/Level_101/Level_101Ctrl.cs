using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_101Ctrl : BaseDragController<L101_Angten>
{
    public L101_NoisyScreen noisyScreen;
    public SpriteRenderer backGround;
    public float currentRotation = -35f;

    private float lastAlpha = 0f; // Biến lưu lại giá trị alpha cuối cùng

    private void Start()
    {
        StartCoroutine(noisyScreen.PlayingAnimation());
    }


    float middle;
    float distanceFromMiddle;
    float maxDistance;
    float alpha;

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 mouseDelta)
    {
        currentRotation -= mouseDelta.x * 30f;
        currentRotation = Mathf.Clamp(currentRotation, -35f, 100f);

        draggableComponent.transform.localRotation = Quaternion.Euler(0, 0, currentRotation);

        // Tính alpha
         middle = (-35f + 100f) / 2; // = 32.5f
         distanceFromMiddle = Mathf.Abs(currentRotation - middle);
         maxDistance = 100f - middle; // = 67.5f
         alpha = Mathf.InverseLerp(0, maxDistance, distanceFromMiddle);

        // Lưu lại alpha để dùng sau này
        lastAlpha = alpha;

        noisyScreen.SetAlpha(alpha);
    }

    protected override void OnDragEnded()
    {
        // Dùng lại giá trị lastAlpha đã lưu ở OnDragLogic
        if (lastAlpha < 0.1f)
        {
            Debug.Log("✅ Tín hiệu ổn định! Alpha gần bằng 0.");
            noisyScreen.isPlayingAnimation = true;
            StartCoroutine(HandleWinCondition());
        }
        else
        {
            Debug.Log($"❌ Tín hiệu chưa ổn định. Alpha hiện tại: {lastAlpha:F3}");
        }
    }
    protected override void OnDragStarted()
    {

    }

    IEnumerator HandleWinCondition()
    {
        backGround.color = new Color32(255,255,121,255);
        isWin = true;
        yield return new WaitForSeconds(0.4f);
        WinBox.SetUp().Show();
    }
}
