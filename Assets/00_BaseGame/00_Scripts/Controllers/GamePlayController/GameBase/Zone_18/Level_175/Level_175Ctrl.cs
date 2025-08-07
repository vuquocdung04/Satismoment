using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_175Ctrl : BaseDragController<Transform>
{
    public L175_Dog dog;
    protected override void OnDragEnded()
    {
        float angle = draggableComponent.transform.eulerAngles.z;
        Debug.LogError(angle);
        if(angle < 231f && angle > 228f)
        {
            draggableComponent.transform.eulerAngles = new Vector3(0,0,229.5f);
            isWin = true;
            StartCoroutine(HandleWinCondition());
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

        draggableComponent.transform.Rotate(0, 0, angle / 2);
    }

    protected override void OnDragStarted()
    {
        
    }

    IEnumerator HandleWinCondition()
    {
        float duration = 5f;
        dog.Moving(duration);
        yield return new WaitForSeconds(duration + 1f);
        WinBox.SetUp().Show();
    }
}
