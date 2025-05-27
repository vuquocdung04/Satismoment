using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_38Ctrl : BaseDragController<L38_Car>
{
    public int winProgress = 0;
    public List<L38_Car> lsCars;
    protected override void OnDragStarted()
    {

    }
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
        draggableComponent.DoRotating();
    }

    protected override void OnDragEnded()
    {

        if (IsInCorrectPosition())
        {
            winProgress++;
            draggableComponent.colli.enabled = false;
        }
        else
        {
            draggableComponent.DoRotatingAngleDefault();
        }
        CheckWinCodition();
    }
    
    bool IsInCorrectPosition()
    {
        float distance = Vector2.Distance(draggableComponent.transform.position, draggableComponent.pos);
        if (distance > -0.3f && distance < 0.3f)
        {
            draggableComponent.transform.position = draggableComponent.pos;
            return true;
        }
        return false;
    }

    void CheckWinCodition()
    {
        if (winProgress == lsCars.Count)
        {
            isWin = true;
            WinBox.SetUp().Show();
        }
    }


    [Button("setup", ButtonSizes.Large)]
    void Setup()
    {
        foreach (var car in this.lsCars)
        {
            car.angle = car.transform.rotation.eulerAngles;
            car.colli = car.transform.GetComponent<BoxCollider2D>();
        }
    }
}
