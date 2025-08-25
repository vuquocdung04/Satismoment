using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_111Ctrl : BaseDragController<L111_RotaryDial>
{
    public AudioClip correctNumberSound;
    public int currentNumber;
    public L111_Handset handset;
    public L111_CordHook hook;
    public List<L111_NumberCall> lsNumbers;
    public bool isOpenHandseted;
    private void Start()
    {
        lsNumbers[0].ChangeColor();
    }

    protected override void Update()
    {
        if (!isOpenHandseted) return;
        base.Update();
    }
    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckNumberCorrect(hook.boxCollider))
        {
            currentNumber++;
            GameController.Instance.musicManager.PlaySingle(correctNumberSound);
            lsNumbers[currentNumber - 1].ResetColor();
        }
        else
        {
            lsNumbers[currentNumber].ResetColor();
            currentNumber = 0;
        }

        if(currentNumber < lsNumbers.Count)
        {
            lsNumbers[currentNumber].ChangeColor();
            draggableComponent.SetPositionAndSpriteNumber(currentNumber);
        }
        draggableComponent.OnDragEnded();
        CheckWin();
        
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
        GameController.Instance.musicManager.PlayPick();
    }

    private void CheckWin()
    {
        if (currentNumber == lsNumbers.Count)
        {
            isWin = true;
            StartCoroutine(HandleWinCondition());
        }
    }

    private IEnumerator HandleWinCondition()
    {
        draggableComponent.OnDragEnded();
        draggableComponent.numberDial.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }

    [Button("Setup number",ButtonSizes.Large)]
    void Setup()
    {
        foreach (var number in this.lsNumbers) number.Init();
    }
}
