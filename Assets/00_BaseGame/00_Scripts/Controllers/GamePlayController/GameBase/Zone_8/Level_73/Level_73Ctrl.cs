using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_73Ctrl : BaseDragController<L73_toasterSwitch>
{
    public Transform effect;
    public L73_bread bread;
    protected override void OnDragEnded()
    {
        float distanceY = draggableComponent.transform.position.y - (-0.25f);
        if(Mathf.Abs(distanceY) < 0.2f)
        {
            draggableComponent.transform.position = new Vector3(draggableComponent.transform.position.x, - 0.25f);
            StartCoroutine(DoToastingBread());
        }
    }

    Vector3 newToasterPos;
    float newBreadY;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        newToasterPos = draggableComponent.transform.position + new Vector3(0,mouseDelta.y,0);
        newToasterPos.x = -1.018f;
        newToasterPos.y = Mathf.Clamp(newToasterPos.y,-0.25f, 0.373f);
        draggableComponent.transform.position = newToasterPos;

        // Cập nhật vị trí của bread với giới hạn Y tương tự
        newBreadY = bread.transform.position.y + deltaMousePosition.y;
        newBreadY = Mathf.Clamp(newBreadY, 0.51f, 1.467f); // Giới hạn Y của bread

        bread.transform.position = new Vector3(bread.transform.position.x, newBreadY, bread.transform.position.z);
    }

    protected override void OnDragStarted()
    {

    }

    IEnumerator DoToastingBread()
    {
        isWin = true;
        yield return new WaitForSeconds(1f);
        bread.DoActionBeforeToasting();
        yield return new WaitForSeconds(1f);
        effect.gameObject.SetActive(true);
        StartCoroutine(HandleWinCodition());
    }
    IEnumerator HandleWinCodition()
    {        
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}
