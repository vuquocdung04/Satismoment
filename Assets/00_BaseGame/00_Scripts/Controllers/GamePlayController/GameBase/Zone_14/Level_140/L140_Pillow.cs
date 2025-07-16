using DG.Tweening;
using System.Collections;
using UnityEngine;

public class L140_Pillow : MonoBehaviour
{
    public Level_140Ctrl levelCtrl;
    bool isReady = true;
    bool isUp = false; // Trạng thái: có đang ở vị trí trên không?
    float moveDistance = 1f; // Khoảng cách di chuyển

    private void OnMouseDown()
    {
        if (levelCtrl.isWin) return;
        if (!isReady) return;
        StartCoroutine(HanldeActionPillow());
    }

    IEnumerator HanldeActionPillow()
    {
        isReady = false;
        float targetMoveY;
        if (isUp)
            targetMoveY = transform.position.y - moveDistance;
        else
            targetMoveY = transform.position.y + moveDistance;

        Tween pillowMove = transform.DOMoveY(targetMoveY, 0.5f).SetEase(Ease.Linear);
        yield return pillowMove.WaitForCompletion();
        levelCtrl.CheckClothOverlaping();
        isUp = !isUp;
        isReady = true;
    }
}