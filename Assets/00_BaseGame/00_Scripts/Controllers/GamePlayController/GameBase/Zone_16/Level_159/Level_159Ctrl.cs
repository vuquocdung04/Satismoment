using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_159Ctrl : BaseDragController<L159_Circle>
{
    [Header("Line Renderer Settings")]
    public L159_DrawLine lineRendererPrefab;
    public float lineWidth = 0.1f;
    public float minDeltaThreshold = 0.1f;

    [Header("Collision Settings")]
    public float lineCollisionThreshold = 0.1f;

    [Header("Win Condition")]
    public int targetConnections = 7; // Số kết nối cần để thắng

    private LineRenderer currentLineRenderer;
    private L159_DrawLine currentDrawLineComponent;
    private List<Vector3> linePoints = new List<Vector3>();
    private List<L159_DrawLine> completedLines = new List<L159_DrawLine>();

    // THÊM: List để theo dõi các kết nối thành công
    private List<CircleConnection> successfulConnections = new List<CircleConnection>();

    protected override void OnDragStarted()
    {
        if (lineRendererPrefab != null && !draggableComponent.isConnected)
        {
            currentDrawLineComponent = SimplePool2.Spawn(lineRendererPrefab);
            currentLineRenderer = currentDrawLineComponent.GetComponent<LineRenderer>();

            Material lineMaterial = currentLineRenderer.material;
            lineMaterial.color = draggableComponent.circleColor;

            currentLineRenderer.startWidth = lineWidth;
            currentLineRenderer.endWidth = lineWidth;

            currentDrawLineComponent.circleType = draggableComponent.circleType;
            currentDrawLineComponent.lineColor = draggableComponent.circleColor;

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
                if (CheckLineCollision(currentMousePosition))
                {
                    Debug.Log("⚠️ Va chạm với đường line đã có! Dừng vẽ.");
                    ClearCurrentLine();
                    return;
                }

                linePoints.Add(currentMousePosition);
                UpdateLineRenderer();
            }
        }
    }

    protected override void OnDragEnded()
    {
        if (currentLineRenderer != null && currentDrawLineComponent != null)
        {
            CheckForMatchingCircle();

            currentDrawLineComponent.points = new List<Vector3>(linePoints);

            currentLineRenderer = null;
            currentDrawLineComponent = null;
            linePoints.Clear();
        }
    }

    /// <summary>
    /// Kiểm tra va chạm và destroy cả 2 line nếu va chạm
    /// </summary>
    private bool CheckLineCollision(Vector3 newPoint)
    {
        for (int i = completedLines.Count - 1; i >= 0; i--)
        {
            L159_DrawLine completedLine = completedLines[i];

            if (completedLine != null && completedLine.isCompleted)
            {
                if (completedLine.IsPointOnLine(newPoint, lineCollisionThreshold))
                {
                    Debug.Log($"🔥 Va chạm và destroy cả 2 line! Line bị va chạm: {completedLine.circleType}");

                    // Destroy line bị va chạm và remove khỏi connection list
                    DestroyCompletedLine(completedLine, i);

                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Destroy line và remove khỏi connection list
    /// </summary>
    private void DestroyCompletedLine(L159_DrawLine lineToDestroy, int index)
    {
        if (lineToDestroy == null) return;

        // Tìm và xóa connection tương ứng
        for (int i = successfulConnections.Count - 1; i >= 0; i--)
        {
            if (successfulConnections[i].connectionLine == lineToDestroy)
            {
                CircleConnection connection = successfulConnections[i];

                // Reset các circle
                if (connection.startCircle != null)
                {
                    connection.startCircle.SetConnected(false);
                    Debug.Log($"Reset start circle: {connection.startCircle.circleType}");
                }

                if (connection.endCircle != null)
                {
                    connection.endCircle.SetConnected(false);
                    Debug.Log($"Reset end circle: {connection.endCircle.circleType}");
                }

                // Xóa khỏi connection list
                successfulConnections.RemoveAt(i);
                Debug.Log($"🗑️ Removed connection from list. Remaining connections: {successfulConnections.Count}");
                break;
            }
        }

        // Xóa khỏi completed lines
        completedLines.RemoveAt(index);

        // Destroy GameObject
        SimplePool2.Despawn(lineToDestroy.gameObject);

        Debug.Log($"Đã destroy line type: {lineToDestroy.circleType}");
    }

    private void CheckForMatchingCircle()
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider != null)
        {
            L159_Circle hitCircle = hit.collider.GetComponent<L159_Circle>();

            if (hitCircle != null)
            {
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
    /// Xử lý khi kết nối thành công - THÊM vào connection list
    /// </summary>
    private void OnSuccessfulConnection(L159_Circle targetCircle)
    {
        Debug.Log("🎉 Kết nối thành công!");

        draggableComponent.SetConnected(true);
        targetCircle.SetConnected(true);

        if (currentDrawLineComponent != null)
        {
            currentDrawLineComponent.isCompleted = true;

            // Lưu reference đến các circle
            currentDrawLineComponent.startCircle = draggableComponent;
            currentDrawLineComponent.endCircle = targetCircle;

            completedLines.Add(currentDrawLineComponent);
            currentDrawLineComponent.UpdateCollider();

            // THÊM vào connection list
            CircleConnection newConnection = new CircleConnection(draggableComponent, targetCircle, currentDrawLineComponent);
            successfulConnections.Add(newConnection);

            Debug.Log($"✅ Added to connection list! Total connections: {successfulConnections.Count}");
            CheckWinCondition();
        }
    }

    /// <summary>
    /// Kiểm tra điều kiện thắng game
    /// </summary>
    private void CheckWinCondition()
    {
        if (successfulConnections.Count >= targetConnections)
        {
            Debug.Log($"🏆 WIN GAME! Đã hoàn thành {successfulConnections.Count}/{targetConnections} kết nối!");
            isWin = true;
            StartCoroutine(OnGameWin());
        }
    }

    /// <summary>
    /// Xử lý khi thắng game
    /// </summary>
    private IEnumerator OnGameWin()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
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

            if (currentDrawLineComponent != null)
            {
                currentDrawLineComponent.points = new List<Vector3>(linePoints);
            }
        }
    }
}
[System.Serializable]
public class CircleConnection
{
    public L159_Circle startCircle;
    public L159_Circle endCircle;
    public L159_DrawLine connectionLine;
    public L159_CircleType circleType;

    public CircleConnection(L159_Circle start, L159_Circle end, L159_DrawLine line)
    {
        startCircle = start;
        endCircle = end;
        connectionLine = line;
        circleType = start.circleType;
    }
}
