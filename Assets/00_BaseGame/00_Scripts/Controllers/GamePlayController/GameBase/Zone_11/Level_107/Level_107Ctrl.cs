using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_107Ctrl : BaseDragController<L107_PieceDonut>
{

    public Transform donut9;
    public Transform mask;
    public List<L107_PieceDonut> lsPieceDonuts;
    public List<Vector2> lsPosTargetDonuts;
    protected override void OnDragEnded()
    {

    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {

    }

    protected override void OnDragStarted()
    {
        if(draggableComponent.neighbor != null)
        {
            StartCoroutine(SwapTile(draggableComponent, draggableComponent.neighbor));
        }
    }

    private IEnumerator SwapTile(L107_PieceDonut donut1, L107_PieceDonut donutNone)
    {
        Vector2 pos1 = donut1.transform.position;
        Vector2 posNone = donutNone.transform.position;

        Tween donut1Move = donut1.transform.DOMove(posNone, 0.1f).SetEase(Ease.Linear);
        donutNone.transform.position = pos1;
        yield return donut1Move.WaitForCompletion();
        SetNeighBor();

        if (CheckWin())
            StartCoroutine(HandleWinCondition());
    }

    void SetNeighBor()
    {
        foreach (var donut in this.lsPieceDonuts)
        {
            if (donut.neighbor == null) continue;
            donut.neighbor = null;
        }
        foreach (var donut in this.lsPieceDonuts) donut.CheckNeighbors();
    }

    public bool CheckWin()
    {
        float distance = 0;
        for (int i = 0; i < lsPieceDonuts.Count - 1; i++)
        {
            distance = Vector2.Distance(lsPieceDonuts[i].transform.position, lsPosTargetDonuts[i]);

            if (Mathf.Abs(distance) > 0.1f)
            {
                Debug.Log($"Tile {i + 1} chưa đúng vị trí. Khoảng cách: {distance}");
                return false; // Phát hiện lỗi → trả về false luôn
            }
        }
        Debug.Log("🎉 You Win! Chúc mừng bạn đã chiến thắng.");
        return true; // Mọi thứ đều đúng → thắng
    }

    IEnumerator HandleWinCondition()
    {
        Tween moveDonut = donut9.DOMoveY(-1.235f, 0.4f).SetEase(Ease.Linear);
        yield return moveDonut.WaitForCompletion();
        yield return new WaitForSeconds(0.1f);
        Tween moveMask = mask.DOMoveY(0,1f).SetEase(Ease.Linear);
        yield return moveMask.WaitForCompletion();
        yield return new WaitForSeconds(0.3f);
        WinBox.SetUp().Show();
    }

    [Button("Setup Donut",ButtonSizes.Large)]
    void SetupDonut()
    {

        foreach (var donut in this.lsPieceDonuts) donut.Init();
    }
}
