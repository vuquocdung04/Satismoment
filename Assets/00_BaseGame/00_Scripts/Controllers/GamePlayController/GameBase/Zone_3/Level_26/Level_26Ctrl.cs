using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_26Ctrl : BaseDragController<Transform>
{
    public Transform ornamentBall;
    public Transform pullCord;
    public Transform cake;
    public Transform ornamentBall_Left;
    public Transform ornamentBall_Right;
    Vector3 newPos;
    [Space(10)]
    public Transform posSpawnEffect;
    protected override void OnDragStarted()
    {
        base.OnDragStarted();
        DoShakeOrnamentBall();
    }

    float yChange;

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        newPos = draggableComponent.transform.position;

        yChange = Mathf.Min(0f, deltaMousePosition.y);
        newPos.y += yChange;
        newPos.x = 0;

        draggableComponent.transform.position = newPos;

        if (draggableComponent.transform.position.y < 1)
            StartCoroutine(HandleWinCondition());
    }

    protected override void OnDragEnded()
    {
        base.OnDragEnded();
        ornamentBall.DOKill();
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        pullCord.DOMoveY(-11f, 2f);
        cake.DOMoveY(-3.3f, 2f);
        ornamentBall.DOKill();
        ornamentBall.transform.eulerAngles = Vector3.zero;
        ornamentBall_Left.transform.eulerAngles = new Vector3(0,0,-15);
        ornamentBall_Right.transform.eulerAngles = new Vector3(0,0,15);

        GameController.Instance.confettiEffectController.SpawmEffect_Drop_UI(posSpawnEffect);
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }



    void DoShakeOrnamentBall()
    {
        float currentZ = ornamentBall.eulerAngles.z;

        // Normalize từ 0~360 → -180~180
        if (currentZ > 180f)
            currentZ -= 360f;

        // Nếu đang ở 0° hoặc gần 0°, chọn bắt đầu từ -15°
        float firstTargetZ = currentZ >= 0 ? -15f : 15f;

        ornamentBall
            .DORotate(new Vector3(0, 0, firstTargetZ), 0.5f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                // Bắt đầu lắc đều giữa ±15°
                ornamentBall
                    .DORotate(new Vector3(0, 0, -firstTargetZ), 0.5f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);
            });
    }
}
