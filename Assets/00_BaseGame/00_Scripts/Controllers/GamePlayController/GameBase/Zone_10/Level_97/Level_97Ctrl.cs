using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_97Ctrl : BaseDragController<L97_Devices>
{
    public int winProgress;
    public List<L97_Devices> lsDevices;
    protected override void OnDragEnded()
    {
        draggableComponent.OnEndDrag();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
        switch (draggableComponent.deviceType)
        {
            case L97_DeviceType.SteamIron:
                draggableComponent.DrawAtPosition(currentMousePosition);
                break;
            default:
                break;
        }
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
    }


    [Button("Setup",ButtonSizes.Large)]
    void Setup()
    {
        foreach(var device in this.lsDevices)
        {
            device.InitAfter();
            device.InitBefore();
        }
    }
}
