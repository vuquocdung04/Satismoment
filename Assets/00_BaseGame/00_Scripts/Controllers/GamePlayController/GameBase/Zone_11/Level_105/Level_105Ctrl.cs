using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_105Ctrl : BaseDragController<L105_CueBall>
{
    public L105_BallArranger ballArranger;
    L105_CueBall cueBallClone;
    public int winProgress;
    public Transform cueStick;
    public float maxPullBackDistance = 2f; // Khoảng cách lùi tối đa của gậy
    public float initialOffsetFromBall = 0.2f; // Khoảng cách ban đầu của gậy so với bóng khi chưa kéo

    private void Start()
    {
        ballArranger.Init();
    }

    public void ResetGame()
    {
        ballArranger.Init();
        cueBallClone.OnBallStopped();
        cueBallClone.transform.position = new Vector2(0,-1f);
    }

    protected override void OnDragEnded()
    {
        StartCoroutine(CoolDown());
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        RotateCueStick(currentMousePosition);
        MoveCueStickBack(currentMousePosition);

        UpdateLineRenderer();
    }

    protected override void OnDragStarted()
    {
        cueStick.gameObject.SetActive(true);
    }
    private IEnumerator CoolDown()
    {
        Tween cueMove = cueStick.DOMove(draggableComponent.transform.position,0.2f).SetEase(Ease.InCubic);
        cueBallClone = draggableComponent;
        yield return cueMove.WaitForCompletion();
        cueStick.gameObject.SetActive(false);
        float power = CalculatePower(currentPullBack);
        cueBallClone.ApplyStrikeForce(-directionToMouse, power);
    }



    // Hàm xử lý xoay gậy theo hướng chuột
    private void RotateCueStick(Vector3 mousePosition)
    {
        Vector3 directionToMouse = (mousePosition - draggableComponent.transform.position).normalized;
        float angleZ = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angleZ + 90);

        cueStick.transform.rotation = Quaternion.Slerp(
            cueStick.transform.rotation,
            targetRotation,
            Time.deltaTime * 15f
        );
    }

    // Hàm xử lý kéo gậy lùi lại để đánh
    Vector3 directionToMouse;
    float currentPullBack;
    private void MoveCueStickBack(Vector3 mousePosition)
    {
        Vector3 ballPosition = draggableComponent.transform.position;
        directionToMouse = (mousePosition - ballPosition).normalized;
        float distanceToMouseFromBall = Vector3.Distance(ballPosition, mousePosition);
        currentPullBack = Mathf.Clamp(distanceToMouseFromBall - initialOffsetFromBall, 0, maxPullBackDistance);

        Vector3 targetStickPosition = ballPosition + directionToMouse * (initialOffsetFromBall + currentPullBack);

        cueStick.transform.position = Vector3.Lerp(
            cueStick.transform.position,
            targetStickPosition,
            Time.deltaTime * 15f
        );
    }
    private void UpdateLineRenderer()
    {
        // Điểm bắt đầu là vị trí của bóng
        Vector3 ballPosition = draggableComponent.transform.position;

        // Điểm kết thúc là vị trí mà gậy đang chỉ đến
        Vector3 targetPosition = ballPosition - directionToMouse * (initialOffsetFromBall + currentPullBack);

        // Vẽ đường bằng Debug.DrawLine
        Debug.DrawLine(ballPosition, targetPosition, Color.white);
    }
    private float CalculatePower(float pullBackDistance)
    {
        // Có thể tùy chỉnh hệ số ở đây để tăng/giảm lực
        float powerMultiplier = 7f; 
        return pullBackDistance * powerMultiplier;
    }


    public IEnumerator HandleWinConditon()
    {
        isWin = true;
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }
}