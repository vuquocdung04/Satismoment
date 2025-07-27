using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L159_DrawLine : MonoBehaviour
{
    public L159_CircleType circleType;
    public Color lineColor;
    public List<Vector3> points = new List<Vector3>();
    public bool isCompleted = false;

    // Thêm reference đến các circle đã kết nối
    public L159_Circle startCircle;
    public L159_Circle endCircle;

    public EdgeCollider2D lineCollider;

    private void Awake()
    {
        if (lineCollider == null)
        {
            lineCollider = GetComponent<EdgeCollider2D>();
            if (lineCollider == null)
            {
                lineCollider = gameObject.AddComponent<EdgeCollider2D>();
            }
        }

        lineCollider.isTrigger = true;
    }

    public void UpdateCollider()
    {
        if (lineCollider != null && points != null && points.Count >= 2)
        {
            Vector2[] colliderPoints = new Vector2[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                colliderPoints[i] = transform.InverseTransformPoint(points[i]);
            }
            lineCollider.points = colliderPoints;
        }
    }

    public bool IsPointOnLine(Vector3 worldPoint, float threshold = 0.1f)
    {
        if (points == null || points.Count < 2) return false;

        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector3 lineStart = points[i];
            Vector3 lineEnd = points[i + 1];

            float distance = DistanceToLineSegment(worldPoint, lineStart, lineEnd);
            if (distance <= threshold)
            {
                return true;
            }
        }
        return false;
    }

    private float DistanceToLineSegment(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
    {
        Vector3 lineVector = lineEnd - lineStart;
        Vector3 pointVector = point - lineStart;

        float lineLength = lineVector.magnitude;
        if (lineLength == 0) return Vector3.Distance(point, lineStart);

        float t = Mathf.Clamp01(Vector3.Dot(pointVector, lineVector) / (lineLength * lineLength));
        Vector3 projection = lineStart + t * lineVector;

        return Vector3.Distance(point, projection);
    }
}
