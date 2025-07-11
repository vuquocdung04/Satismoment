using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_127Ctrl : BaseDragController<L127_Hair>
{
    protected override void OnDragEnded()
    {
        
        if (draggableComponent.CheckAngle())
        {
            StartCoroutine(HandleWinCondition());
        }
        else
        {
            draggableComponent.OnDragEnd();
        }
    }


    float angle;
    Vector3 objectCenter;
    Vector2 vectorToPrevMouse;
    Vector2 vectorToCurrentMouse;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        objectCenter = draggableComponent.transform.position;

        vectorToPrevMouse = (Vector2)prevMouseWorldPos - (Vector2)objectCenter;

        vectorToCurrentMouse = (Vector2)currentMousePosition - (Vector2)objectCenter;
        angle = Vector2.SignedAngle(vectorToPrevMouse, vectorToCurrentMouse);
        draggableComponent.transform.Rotate(0, 0, angle);
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnDragStart();
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }
}
