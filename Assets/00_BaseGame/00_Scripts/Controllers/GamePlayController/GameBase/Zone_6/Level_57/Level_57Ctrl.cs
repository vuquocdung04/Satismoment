using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_57Ctrl : BaseDragController<L57_TurntablePart>
{
    public Transform item;
    public L57_noteMusic prefabs;

    private L57_TurntablePart vinylDiscClone;
    private L57_TurntablePart turntableArmClone;
    protected override void OnDragEnded()
    {
        switch (draggableComponent.type)
        {
            case TurntablePartType.vinylDisc:
                float distance = Vector2.Distance(draggableComponent.transform.position, draggableComponent.posCorrect);
                if (distance < 0.5f)
                {
                    draggableComponent.transform.position = draggableComponent.posCorrect;
                    vinylDiscClone = draggableComponent;
                    vinylDiscClone.isMovevinylDisc = true;
                    vinylDiscClone.transform
                        .DORotate(new Vector3(0, 0, -360f), 5f, RotateMode.FastBeyond360)
                        .SetEase(Ease.Linear)
                        .SetLoops(-1);
                }
                break;
            case TurntablePartType.turntableArm:
                float zRotation = draggableComponent.transform.eulerAngles.z;

                if (zRotation > 180f) zRotation -= 360f;

                if (zRotation <= -2f && zRotation >= -8f)
                {
                    item.gameObject.SetActive(true);
                    StartCoroutine(HandleWinCodition());
                }
                break;
        }

    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
       
        switch (draggableComponent.type)
        {
            case TurntablePartType.vinylDisc:
                if(!draggableComponent.isMovevinylDisc)
                draggableComponent.transform.position += mouseDelta;
                break;
            case TurntablePartType.turntableArm:
                if (vinylDiscClone == null) return;
                if(vinylDiscClone.isMovevinylDisc)
                draggableComponent.transform.Rotate(0, 0, -mouseDelta.y * 15f);
                break;
        }
    }

    protected override void OnDragStarted()
    {
        
    }

    IEnumerator HandleWinCodition()
    {
        var waiTime = new WaitForSeconds(0.4f);
        for (int i = 0; i < 5; i++)
        {
            yield return waiTime;
            var clone = Instantiate(prefabs, new Vector2(0.61f, -0.4f), Quaternion.identity);
            clone.Effect();
        }
        yield return new WaitForSeconds(0.3f);
        WinBox.SetUp().Show();
    }
}
