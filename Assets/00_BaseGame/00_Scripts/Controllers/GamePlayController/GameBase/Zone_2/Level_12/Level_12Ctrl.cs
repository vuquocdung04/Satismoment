using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_12Ctrl : BaseDragController<L12_PaintRoller>
{
    public LineRenderer lineRenderer;
    List<Vector3> lsPoints = new List<Vector3>();

    Vector2 oldPoint = Vector2.zero;
    Vector3 point;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.Translate(mouseDelta);
        if (Vector2.Distance(oldPoint, draggableComponent.transform.position) > 0.5f)
        {
            oldPoint = mouseWorldPos;
            lsPoints.Add(draggableComponent.transform.position);
            lineRenderer.positionCount = lsPoints.Count;

            for(int i = 0; i < lsPoints.Count; i++)
            {
                point = lsPoints[i];
                point.z = -1;
                lineRenderer.SetPosition(i, point);
            }
        }
    }

    protected override void OnDragStarted()
    {
        base.OnDragStarted();
        lsPoints.Clear();
        lineRenderer.positionCount = 0;
    }

    protected override void OnDragEnded()
    {
        base.OnDragEnded();
    }
}
