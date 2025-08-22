using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using DG.Tweening;
using UnityEngine;

public class Level_170Ctrl : BaseDragController<L170_Btn>
{
    public AudioClip waterSound;
    public Transform shit;
    public Transform water;

    Tween waterScaleTween;    // tween nước phồng lên
    bool isAllFinished;       // true khi tất cả hoàn thành

    // ──────────────────────────── Drag callbacks ────────────────────────────
    protected override void OnDragStarted()
    {
        draggableComponent.objRenderer.sprite = draggableComponent.spriteHold;
        GameController.Instance.musicManager.PlaySingle(waterSound);
        // Nếu chưa bắt đầu thì tạo mới
        if (waterScaleTween == null)
        {
            StartWaterScale();
        }
        // Nếu đang pause thì tiếp tục
        else if (!waterScaleTween.IsPlaying() && !isAllFinished)
        {
            waterScaleTween.Play();
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition) { }

    protected override void OnDragEnded()
    {
        draggableComponent.objRenderer.sprite = draggableComponent.defaultSprite;

        // Nếu tween đang chạy thì tạm dừng
        if (waterScaleTween != null && waterScaleTween.IsPlaying() && !isAllFinished)
        {
            waterScaleTween.Pause();
        }
    }

    // ──────────────────────────── Animation logic ────────────────────────────
    void StartWaterScale()
    {
        isAllFinished = false;

        // Reset nước về scale ban đầu
        water.localScale = Vector3.one;

        waterScaleTween = water.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 1.5f)
            .OnComplete(() =>
            {
                Debug.Log("Nước phồng xong! Bắt đầu xoay...");
                StartRotateAndFlush();
            })
            .SetAutoKill(false);
    }

    void StartRotateAndFlush()
    {
        // Shit xoay và biến mất
        shit.DORotate(new Vector3(0, 0, 720), 2f, RotateMode.LocalAxisAdd);
        shit.DOScale(Vector3.zero, 2f);

        // Nước xoay và biến mất
        water.DORotate(new Vector3(0, 0, 720), 3f, RotateMode.LocalAxisAdd);
        water.DOScale(Vector3.zero, 3f)
            .OnComplete(() =>
            {
                // Đợi một chút rồi nước trở lại
                DOVirtual.DelayedCall(0.1f, () =>
                {
                    water.DOScale(Vector3.one, 0.5f)
                        .OnComplete(() =>
                        {
                            isAllFinished = true;
                            Debug.Log("Tất cả hoàn thành!");
                            isWin = true;
                            WinBox.SetUp().Show();
                        });
                });
            });
    }
}
