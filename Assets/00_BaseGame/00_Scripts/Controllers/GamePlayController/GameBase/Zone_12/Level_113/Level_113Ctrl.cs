using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_113Ctrl : BaseDragControllerVer2<L113_Garbage>
{
    public AudioClip carSound;
    public Transform car;
    public List<L113_GarbageCan> lsGarbageCans;
    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckCorrectWithGarbageCan(GetGarbageCanById(draggableComponent.id)))
        {
            winProgress++;
            if (CheckWin())
            {
                StartCoroutine(HandleWinCondition());
            }
        }
        else
        {
            draggableComponent.OnEndDrag();
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
        GameController.Instance.musicManager.PlayPick();
    }

    public L113_GarbageCan GetGarbageCanById(int id)
    {
        foreach(var garbageCan in this.lsGarbageCans)
        {
            if(garbageCan.id == id) return garbageCan;
        }
        return null;
    }

    private bool CheckWin()
    {
        if(winProgress == lsT_ItemDragables.Count)
        {
            return true;
        }
        return false;
    }

    protected override IEnumerator HandleWinCondition()
    {
        Tween carMove = car.DOMoveX(6f,1.5f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.2f);
        GameController.Instance.musicManager.PlaySingle(carSound);
        yield return carMove.WaitForCompletion();
        yield return base.HandleWinCondition();
    }
    //Odin INspector

    protected override void SetupComponent_PositionCorrect()
    {
        foreach (var garbage in this.lsT_ItemDragables) garbage.InitCorrect();
    }

    protected override void SetupPositionDefault()
    {
        foreach (var garbage in this.lsT_ItemDragables) garbage.InitDefault();
    }
}
