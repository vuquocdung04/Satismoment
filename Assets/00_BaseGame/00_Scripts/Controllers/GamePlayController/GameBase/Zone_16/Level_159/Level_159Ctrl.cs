using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_159Ctrl : BaseDragController<L159_Circle>
{
    [Header("Line Renderer Settings")]
    public L159_DrawLine lineRendererPrefab; // Prefab có sẵn LineRenderer và L159_DrawLine
    public float lineWidth = 0.1f;
    public float minDeltaThreshold = 0.1f;

    private LineRenderer currentLineRenderer;
    private L159_DrawLine currentDrawLineComponent; // Thêm reference này
    private List<Vector3> linePoints = new List<Vector3>();

    protected override void OnDragStarted()
    {
        if (lineRendererPrefab != null)
        {
            // Instantiate prefab (đã có sẵn L159_DrawLine component)
            currentDrawLineComponent = SimplePool2.Spawn(lineRendererPrefab);
            currentLineRenderer = currentDrawLineComponent.GetComponent<LineRenderer>();

            // Set màu từ circle được chọn
            Material lineMaterial = currentLineRenderer.material;
            lineMaterial.color = draggableComponent.circleColor;

            // Thiết lập cơ bản
            currentLineRenderer.startWidth = lineWidth;
            currentLineRenderer.endWidth = lineWidth;

            // Set thông tin cho L159_DrawLine component
            currentDrawLineComponent.circleType = draggableComponent.circleType;
            currentDrawLineComponent.lineColor = draggableComponent.circleColor;

            // Khởi tạo điểm đầu tiên
            linePoints.Clear();
            linePoints.Add(mouseWorldPos);
            UpdateLineRenderer();
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        if (currentLineRenderer != null)
        {
            float deltaMagnitude = deltaMousePosition.magnitude;

            if (deltaMagnitude >= minDeltaThreshold)
            {
                linePoints.Add(currentMousePosition);
                UpdateLineRenderer();
            }
        }
    }

    protected override void OnDragEnded()
    {
        if (currentLineRenderer != null && currentDrawLineComponent != null)
        {
            // Bắn raycast tại vị trí thả tay
            CheckForMatchingCircle();

            // Cập nhật thông tin cuối cùng cho L159_DrawLine component
            currentDrawLineComponent.points = new List<Vector3>(linePoints);

            // Reset references
            currentLineRenderer = null;
            currentDrawLineComponent = null;
            linePoints.Clear();
        }
    }

    private void CheckForMatchingCircle()
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider != null)
        {
            L159_Circle hitCircle = hit.collider.GetComponent<L159_Circle>();

            if (hitCircle != null)
            {
                // Kiểm tra xem target circle đã được kết nối chưa
                if (hitCircle.isConnected)
                {
                    Debug.Log($"Circle đã được kết nối rồi! Type: {hitCircle.circleType}");
                    ClearCurrentLine();
                    return;
                }

                if (hitCircle.circleType == draggableComponent.circleType)
                {
                    Debug.Log($"Trúng circle cùng loại! Type: {hitCircle.circleType}");
                    OnSuccessfulConnection(hitCircle);
                }
                else
                {
                    Debug.Log($"Trúng circle khác loại! Đang vẽ: {draggableComponent.circleType}, Trúng: {hitCircle.circleType}");
                    ClearCurrentLine();
                }
            }
            else
            {
                ClearCurrentLine();
                Debug.Log("Trúng object nhưng không phải L159_Circle");
            }
        }
        else
        {
            Debug.Log("Không trúng object nào");
            ClearCurrentLine();
        }
    }

    /// <summary>
    /// Xử lý khi kết nối thành công (circle cùng loại)
    /// </summary>
    private void OnSuccessfulConnection(L159_Circle targetCircle)
    {
        Debug.Log("🎉 Kết nối thành công!");

        // Đánh dấu cả 2 circle đã được kết nối
        draggableComponent.SetConnected(true);
        targetCircle.SetConnected(true);

        // Đánh dấu line cũng đã hoàn thành (nếu cần)
        if (currentDrawLineComponent != null)
        {
            currentDrawLineComponent.isCompleted = true;

            // Có thể đổi màu line để báo hiệu thành công
            Material lineMaterial = currentLineRenderer.material;
            lineMaterial.color = Color.green;
        }
    }

    private void ClearCurrentLine()
    {
        if (currentDrawLineComponent != null)
        {
            SimplePool2.Despawn(currentDrawLineComponent.gameObject);
        }
    }


    private void UpdateLineRenderer()
    {
        if (currentLineRenderer != null && linePoints.Count > 0)
        {
            currentLineRenderer.positionCount = linePoints.Count;
            currentLineRenderer.SetPositions(linePoints.ToArray());

            // Cập nhật points cho L159_DrawLine component theo thời gian thực
            if (currentDrawLineComponent != null)
            {
                currentDrawLineComponent.points = new List<Vector3>(linePoints);
            }
        }
    }
}
