using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_53Ctrl : BaseDragController<L53_Wiper>
{
    public int winProgress = 0;
    public L53_HookRod hookRod;
    public float tiltSmoothTime = 0.2f;

    public List<SpriteRenderer> lsPoints;
    public Sprite pointGreen;

    private float rotationVelocity;

    protected override void OnDragEnded()
    {
        draggableComponent.transform.DOMoveX(draggableComponent.posDefault.x,0.2f);
        if (!isLimitReached)
        {
            hookRod.transform.DORotate(Vector3.zero, 0.5f);
        }
    }

    float countTime = 0;
    Vector3 wiperPos;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {

        wiperPos = draggableComponent.transform.position;
        wiperPos.x += deltaMousePosition.x;
        wiperPos.x = Mathf.Clamp(wiperPos.x, draggableComponent.maxDistanceLeft, draggableComponent.maxDistanceRight);

        wiperPos.y = draggableComponent.posDefault.y;
        draggableComponent.transform.position = wiperPos;

        if (isLimitReached)
        {
            return;
        }
        countTime += Time.deltaTime;
        MoveHookRod();
        if(countTime > 0.1f)
        {
            RotateHookRod();
        }

    }

    protected override void OnDragStarted()
    {
        isLimitReached = false;
        hookRod.transform.DOKill();
        rotationVelocity = 0f;
        countTime = 0;
    }

    float hookRodSpeed;
    Vector3 hookPos;
    float newAngle;
    private bool isLimitReached = false;
    void MoveHookRod()
    {
        hookRodSpeed = draggableComponent.transform.position.x - draggableComponent.posDefault.x;
        hookRod.transform.position += Vector3.right * hookRodSpeed * 2.5f * Time.deltaTime;
        hookPos = hookRod.transform.position;
        hookPos.x = Mathf.Clamp(hookPos.x, hookRod.minXPosition, hookRod.maxXPosition);
        hookRod.transform.position = hookPos;

        bool atMin = Mathf.Approximately(hookPos.x, hookRod.minXPosition);
        bool atMax = Mathf.Approximately(hookPos.x, hookRod.maxXPosition);

        if (atMin || atMax)
        {
            hookRod.transform.DORotate(Vector3.zero, 0.5f);
        }
    }
    float progress;
    float targetAngle;
    float minWiperOffset;
    float maxWiperOffset;
    void RotateHookRod()
    {

        minWiperOffset = draggableComponent.maxDistanceLeft - draggableComponent.posDefault.x;
        maxWiperOffset = draggableComponent.maxDistanceRight - draggableComponent.posDefault.x;

        progress = Mathf.InverseLerp(minWiperOffset, maxWiperOffset, hookRodSpeed);
        targetAngle = Mathf.Lerp(20f, -20f, progress);

        newAngle = Mathf.SmoothDampAngle(hookRod.transform.eulerAngles.z, targetAngle, ref rotationVelocity, tiltSmoothTime);
        hookRod.transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }



    public IEnumerator HandleWinCodition()
    {
        lsPoints[winProgress - 1].sprite = pointGreen;
        if(winProgress == 3)
        {
            isWin = true;
            yield return new WaitForSeconds(0.1f);
            WinBox.SetUp().Show();
        }
    }
}
