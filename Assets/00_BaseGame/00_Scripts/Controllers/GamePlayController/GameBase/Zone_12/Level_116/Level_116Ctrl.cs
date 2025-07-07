using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Level_116Ctrl : BaseDragController<L116_PiecePotato>
{
    [SerializeField] private List<L116_PiecePotato> potatoPieces = new List<L116_PiecePotato>();
    [SerializeField] private float pieceWidth = 1f; // The width of each potato piece.


    private void Start()
    {
        ArrangePieces();
    }

    protected override void OnDragEnded()
    {
        ArrangePieces();
        draggableComponent.OnEndDrag();
        if (CheckWin())
        {
            StartCoroutine(HandleWinCondition());
        }
    }
    Vector3 newPos;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        // Only allow dragging along the X-axis.
        newPos = draggableComponent.transform.position + new Vector3(deltaMousePosition.x, 0, 0);
        newPos.x = Mathf.Clamp(newPos.x,-1.15f,1.15f);
        draggableComponent.transform.position = newPos;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
    }

    public void ArrangePieces()
    {
        potatoPieces.Sort((p1, p2) => p1.transform.position.x.CompareTo(p2.transform.position.x));
        float startX = -((potatoPieces.Count - 1) * pieceWidth) / 2f;

        // Iterate through the sorted pieces and set their target positions.
        for (int i = 0; i < potatoPieces.Count; i++)
        {
            Vector3 targetPos = new Vector3(startX + i * pieceWidth, 0.5f, 0); // Y and Z remain 0 for a 2D arrangement.
            potatoPieces[i].transform.position = targetPos;
        }
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return StartCoroutine(PlayingAnimation());
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }

    bool CheckWin()
    {
        int count = potatoPieces.Count;
        // Tìm vị trí giữa
        int middleIndex = count / 2;

        if (potatoPieces[middleIndex].id != middleIndex)
        {
            return false;
        }

        // Kiểm tra đối xứng hai bên
        for (int i = 0; i < middleIndex; i++)
        {
            if (potatoPieces[i].id != potatoPieces[count - 1 - i].id)
            {
                return false;
            }
        }

        return true;
    }
    IEnumerator PlayingAnimation()
    {
        int i = 0;
        var waitTime = new WaitForSeconds(0.1f);
        while( i < 3)
        {
            i++;
            foreach(var potato in this.potatoPieces)
            {
                potato.transform.DOMoveY(potato.transform.position.y + 0.5f, 0.2f).OnComplete(delegate
                {
                    potato.transform.DOMoveY(potato.transform.position.y - 0.5f, 0.2f);
                });
                yield return waitTime;
            }
            yield return waitTime;
        }
    }

    [Button("Setup ",ButtonSizes.Large)]
    void Setup()
    {
        foreach(var potato in this.potatoPieces)
        {
            potato.InitAfter();
            potato.InitBefore();
        }
    }
}