using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_43Ctrl : BaseDragController<L43_pipe>
{
    public Transform mask;
    public Transform waterPipe;
    public Transform waterValve;
    public L43_Penguin penguin;
    public List<L43_pipe> lsPipes;


    private void Start()
    {
        RandomAnglesPipe();

    }

    protected override void OnDragStarted()
    {
        draggableComponent.DoRotating();
    }
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        Debug.LogError("test");
    }
    protected override void OnDragEnded()
    {
        if (CheckWinCodition())
        {
            StartCoroutine(HandleWinCodition());
        }
    }


    bool CheckWinCodition()
    {
        foreach(var pipe in this.lsPipes)
        {
            if(Mathf.Abs(Mathf.DeltaAngle(pipe.transform.eulerAngles.z, pipe.angleCorrect)) > 1f)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator HandleWinCodition()
    {
        isWin = true;
        waterValve.DORotate(new Vector3(0, 0, 180), 1f, RotateMode.Fast);
        yield return new WaitForSeconds(0.4f);
        waterPipe.DOLocalMoveY(1f, 1f);
        yield return new WaitForSeconds(0.6f);
        mask.DOLocalMoveY(1.36f, 1f);
        yield return new WaitForSeconds(0.5f);
        penguin.DoChangingSprite();
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();

    }


    void RandomAnglesPipe()
    {
        int[] allowedAngles = { 0, 90, 180, 270 };
        System.Random rand = new System.Random();

        foreach (var pipe in this.lsPipes)
        {
            int randomIndex = rand.Next(allowedAngles.Length);
            float angle = allowedAngles[randomIndex];
            pipe.transform.eulerAngles = new Vector3(
                pipe.transform.eulerAngles.x,
                pipe.transform.eulerAngles.y,
                angle
            );
            pipe.rotate = pipe.transform.eulerAngles;
        }
    }


    [Button("Setup",ButtonSizes.Large)]
    void Setup()
    {
        foreach(var pipe in this.lsPipes)
        {
            pipe.angleCorrect = pipe.transform.eulerAngles.z;
        }
    }
}
