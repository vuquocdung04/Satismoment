using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L50_hook : MonoBehaviour
{
    private Tween currentLineMovement;
    public L50_Item item;
    public Collider2D _collider;
    public void SetCurrentLineMovement(Tween tween)
    {
        currentLineMovement = tween;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        item = collision.GetComponent<L50_Item>();
        _collider.enabled = false;
        item.transform.SetParent(transform);
        switch (item.itemType)
        {
            case L50_ItemType.Diffience:
                item.StopMovement();
                break;
        }
        if(currentLineMovement != null && currentLineMovement.IsActive())
        {
            currentLineMovement.Kill();
        }
    }

    public void ResetCollisionState()
    {
        currentLineMovement = null;
        item = null;
    }

    public void KillTween()
    {
        if (currentLineMovement != null && currentLineMovement.IsActive())
        {
            currentLineMovement.Kill();
        }
        currentLineMovement = null;
    }
}
