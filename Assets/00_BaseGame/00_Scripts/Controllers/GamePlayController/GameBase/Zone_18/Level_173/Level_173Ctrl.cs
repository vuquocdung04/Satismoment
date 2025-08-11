using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using Unity.VisualScripting;
using UnityEngine;

public class Level_173Ctrl : BaseDragController<L173_Picture>
{
    public Transform bg;
    public L173_Picture picture;
    public L173_Stone stonePrefab;
    private void Start()
    {
        // Cần thêm tham số cho DOShakePosition
        bg.DOShakePosition(
            duration: 2f,      // Thời gian rung (giây)
            strength: 0.5f,    // Cường độ rung
            vibrato: 15,       // Số lần rung
            randomness: 90f    // Độ ngẫu nhiên (0-180)
        );
        picture.InitState();
        StartCoroutine(SpawnStone());
    }

    protected override void OnDragEnded()
    {
        if(draggableComponent.transform.eulerAngles.z < 1f && draggableComponent.transform.eulerAngles.z > -1f)
        {
            draggableComponent.objRenderer.sprite = draggableComponent.spriteOn;
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

        draggableComponent.transform.Rotate(0, 0, angle/2);
    }

    protected override void OnDragStarted()
    {
        
    }

    IEnumerator SpawnStone()
    {
        for(int i =0; i < 5; i++)
        {
            var stoneClone = SimplePool2.Spawn(stonePrefab);
            stoneClone.InitState();
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.4f);
        WinBox.SetUp().Show();
    }
}
