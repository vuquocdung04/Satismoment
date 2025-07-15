using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum L133_ItemType
{
    None,
    StackOfBooks,
    Box,
}
public class L133_RoomItems : BaseDraggableObject
{
    public L133_ItemType itemType;
    public bool CheckCorrectPosition()
    {
        float distance = Vector2.Distance(transform.position, posCorrect);
        if (Mathf.Abs(distance) < 0.2f)
        {
            return true;
        }
        return false;
    }

    public void MoveItemToCorrectPosition()
    {
        if (itemType == L133_ItemType.StackOfBooks || itemType == L133_ItemType.Box)
            gameObject.SetActive(false);
        else
            transform.DOMove(posCorrect, 0.2f).SetEase(Ease.Linear);
        
    }
    
    public override void ReturnToOriginalPosition()
    {

    }
}
