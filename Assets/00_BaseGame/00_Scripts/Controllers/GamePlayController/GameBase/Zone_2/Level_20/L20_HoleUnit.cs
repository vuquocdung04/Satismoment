using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L20_HoleUnit : MonoBehaviour
{
    public bool isHit;
    public float durationShow = 2f;
    public float resetDuration = 2f;
    [Space(10)]
    public Sprite moleStun;
    public SpriteRenderer srMole;
    public Transform transMole;
    public Transform effectStun;
    public CapsuleCollider2D capsuleCollider2D;

    private void Update()
    {
        if (isHit)
        {
            return;
        }

        durationShow -= Time.deltaTime;
        if(durationShow < 0)
        {
            DoMoleMove();    
            durationShow = resetDuration;
        }
    }

    void DoMoleMove()
    {
        int rand = Random.Range(0,10);
        if (rand % 3 == 0)
        {
            transMole.DOLocalMoveY(0.43f, resetDuration/2-0.1f).OnComplete(() => transMole.DOLocalMoveY(-0.63f, resetDuration / 2 - 0.1f));
        }

    }

    void RotateEffect()
    {
        effectStun.DOLocalRotate(new Vector3(0, 0, 360f), 2f, RotateMode.FastBeyond360)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }

    public void HandleHit()
    {
        transMole.DOKill();
        effectStun.gameObject.SetActive(true);
        srMole.sprite = moleStun;
        transMole.transform.localPosition = new Vector2(0, 0.43f);
        capsuleCollider2D.enabled = false;
        isHit = true;

        this.RotateEffect();
    }
}
