using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L163_PumpHandle : MonoBehaviour
{
    public float limitYTop;
    public float limitYBot;
    Vector2 newPosition;
    Vector2 originalPosition;
    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void OnDragEnd(Level_163Ctrl levelCtrl)
    {
        transform.DOLocalMoveY(limitYTop, 0.3f).SetEase(Ease.Linear);
        levelCtrl.canPump = true;
    }
    public void Pumping(float speed, Level_163Ctrl levelCtrl)
    {
        newPosition = transform.localPosition + new Vector3(0,speed,0);
        transform.localPosition = newPosition;
        if(transform.localPosition.y >= limitYTop)
        {
            transform.localPosition = new Vector2(originalPosition.x, limitYTop);
        }
        else if(transform.localPosition.y <= limitYBot)
        {
            transform.localPosition = new Vector2(originalPosition.x, limitYBot);
            if (levelCtrl.canPump)
            {
                levelCtrl.amountPump++;
                levelCtrl.canPump = false;
                levelCtrl.bike.DOJump(levelCtrl.bike.position + new Vector3(0, 0.01f, 0), 0.2f, 1, 0.2f);
            }
        }
    }
}
