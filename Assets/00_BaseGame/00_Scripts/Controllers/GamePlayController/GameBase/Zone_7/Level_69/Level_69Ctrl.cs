using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_69Ctrl : BaseDragController<L69_DryLeaf>
{
    public List<Transform> lsFlowers;
    public int winProgress;
    Vector2 checkDistance;
    protected override void OnDragEnded()
    {
        float distance = Vector2.Distance(checkDistance, draggableComponent.transform.position);
        if (Mathf.Abs(distance) > 0.5f)
        {
            winProgress++;
            draggableComponent.InteractWithLeaf();
            GameController.Instance.musicManager.PlayPick();
        }
        else
        {
            draggableComponent.transform.position = draggableComponent.defaultPos;
        }
        if(winProgress == 10)
        {
            isWin = true;
            StartCoroutine(HandleWinCodition());
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        checkDistance = draggableComponent.transform.position;
    }

    IEnumerator HandleWinCodition()
    {
        for (int i = 0; i < lsFlowers.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);
            lsFlowers[i].gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}
