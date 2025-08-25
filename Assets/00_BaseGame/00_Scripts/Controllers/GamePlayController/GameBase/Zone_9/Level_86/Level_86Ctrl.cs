
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_86Ctrl : BaseDragController<L86_Tooth>
{
    public Transform upper_Teeth;
    public Transform crocodile_Idle;
    public List<L86_Number> lsNumbers;
    public List<L86_Tooth> lsTooths;
    private int[] correctOrder = { 2, 4, 5, 1, 3 };
    private int currentStep = 0;

    private void Start()
    {
        StartCoroutine(InitEffectStart());

    }

    bool isInteracted;
    protected override void Update()
    {
        if (isInteracted) return;
        base.Update();

    }


    protected override void OnDragStarted()
    {
          GameController.Instance.musicManager.PlayPick();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {

    }

    protected override void OnDragEnded()
    {
        int clickedId = draggableComponent.idTooth;
        draggableComponent.HiddenTooth();
        if (clickedId == correctOrder[currentStep])
        {
            currentStep++;
            if(currentStep == lsTooths.Count)
            {
                StartCoroutine(HandleWinCondition());
            }
        }
        else
        {
            HandleLoseCondition();
        }
    }

    void HandleLoseCondition()
    {
        isInteracted = true;
        currentStep = 0;
        ResetStateTooth();
        crocodile_Idle.gameObject.SetActive(true);
        upper_Teeth.gameObject.SetActive(false);
        StartCoroutine(InitEffectStart());
    }
    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.3f);
        WinBox.SetUp().Show();
    }

    IEnumerator InitEffectStart()
    {
        int index = 0;
        while(index < lsNumbers.Count)
        {
            lsNumbers[index].ShowNumer();
            index++;
            yield return new WaitForSeconds(0.1f);
        }
        crocodile_Idle.gameObject.SetActive(false);
        upper_Teeth.gameObject.SetActive(true);
        isInteracted = false;
    }

    void ResetStateTooth()
    {
        foreach(var tooth in this.lsTooths)
        {
            tooth.ResetState();
        }
    }

}
