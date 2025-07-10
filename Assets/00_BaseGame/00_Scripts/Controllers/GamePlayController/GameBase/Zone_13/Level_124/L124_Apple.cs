using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L124_Apple : L124_ObjDragable
{
    public Sprite bigSprite;
    public override void HandleCollisionWithObj()
    {
        if (!objCollider.IsTouching(levelCtrl.penguin.objCollider)) return;
        StartCollisionState();
        var penguinPosition = levelCtrl.penguin.transform.position + new Vector3(0, 1, 0);
        var seedPostion = levelCtrl.penguin.transform.position + new Vector3(1, 0, 0);
        StartCoroutine(levelCtrl.SpawnTimmingBar(penguinPosition, delegate
        {
            levelCtrl.SpawnSeed(seedPostion);
            SimplePool2.Despawn(gameObject);
        }));
    }

    void StartCollisionState()
    {
        objCollider.enabled = false;
        objRenderer.sortingOrder = -1;
    }

    public void InitState()
    {
        objRenderer.sortingOrder = 5;
    }

    public bool CheckTochingWithZone()
    {
        if (objCollider.IsTouching(levelCtrl.canvas.boxCollider2d))
        {
            Vector3 targetPos = levelCtrl.canvas.AssignItemToNearestAvailable(this, transform.position);

            if (targetPos != Vector3.zero)
            {
                transform.DOMove(targetPos, 0.2f).SetEase(Ease.OutQuad);
                objCollider.enabled = false;
                levelCtrl.IncreaseAmountApple();
                objRenderer.sprite = bigSprite;
                return true;
            }
        }
        return false;
    }


}
