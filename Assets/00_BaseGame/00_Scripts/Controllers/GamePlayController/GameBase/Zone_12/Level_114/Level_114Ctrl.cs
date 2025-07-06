using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_114Ctrl : BaseDragController<L114_HeadVacuum>
{
    public int winProgress;
    public L114_Effect effect;
    public L114_Button btn;

    public Transform vacuumBodyPoint;
    public Transform hoseAttachPoint;

    public LineRenderer lineRenderer;

    [Range(5, 50)] // Giúp điều chỉnh số lượng đoạn trong Inspector
    public int segmentCount = 7;

    [Range(0.1f, 5.0f)] // Giúp điều chỉnh độ cao đường cong trong Inspector
    public float curveControlOffset = 2.0f;

    [Range(0.0f, 1.0f)] // Điều chỉnh độ lệch của điểm điều khiển (0.5 là ở giữa)
    public float curveBias = 0.5f;


    private void Start()
    {
        UpdateLineRenderer();
    }

    protected override void Update()
    {
        if (!btn.isOpened) return;
        base.Update();
    }

    protected override void OnDragEnded()
    {

    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        // Di chuyển đầu hút theo chuột
        draggableComponent.transform.position += mouseDelta;

        // Cập nhật LineRenderer trong mỗi frame khi kéo
        UpdateLineRenderer();
    }

    protected override void OnDragStarted()
    {
        
    }

    public IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(1f);

        WinBox.SetUp().Show();
    }


    /// <summary>
    /// Hàm duy nhất để tính toán và cập nhật các điểm của LineRenderer.
    /// Hàm này sẽ được gọi từ Start, OnDragLogic và OnDragStarted để tránh lặp code.
    /// </summary>
    float t;
    Vector3 point;
    Vector3 controlPoint;
    private void UpdateLineRenderer()
    {
        // Kiểm tra null để tránh lỗi nếu các đối tượng chưa được gán trong Inspector
        if (vacuumBodyPoint == null || hoseAttachPoint == null || lineRenderer == null)
        {
            Debug.LogWarning("Please assign vacuumBodyPoint, hoseAttachPoint, and LineRenderer in the Inspector for Level_114Ctrl.");
            return;
        }
        // Tính toán điểm điều khiển cho đường cong Bezier
        // controlPoint được đặt ở vị trí nội suy giữa currentStartPoint và currentEndPoint,
        // sau đó dịch chuyển lên trên theo trục Y.
        controlPoint = Vector3.Lerp(vacuumBodyPoint.position, hoseAttachPoint.position, curveBias);
        controlPoint.y += curveControlOffset; // Dịch chuyển lên trên để tạo độ cong

        lineRenderer.positionCount = segmentCount;

        for (int i = 0; i < segmentCount; i++)
        {
            t = (float)i / (segmentCount - 1);

            // Công thức cho đường cong Bezier bậc hai
            point = Mathf.Pow(1 - t, 2) * vacuumBodyPoint.position +
                            2 * (1 - t) * t * controlPoint +
                            Mathf.Pow(t, 2) * hoseAttachPoint.position;

            lineRenderer.SetPosition(i, point);
        }
    }
}