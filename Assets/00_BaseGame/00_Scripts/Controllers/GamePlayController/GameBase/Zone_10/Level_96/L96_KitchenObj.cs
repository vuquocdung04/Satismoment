using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum L96_KitchenObjType
{
    Potato,
    PressHandle,
    FoodBasket
}

public class L96_KitchenObj : BaseDraggableObject
{
    public L96_KitchenObjType objType;
    public override void ReturnToOriginalPosition()
    {

    }

    public virtual bool CheckCorrectPosition()
    {
        switch (objType)
        {
            case L96_KitchenObjType.PressHandle:
                if(transform.eulerAngles.z < 0.1f && transform.eulerAngles.z > -0.1f)
                {
                    return true;
                }
                break;
            default:
                float distance = Vector2.Distance(transform.localPosition, posCorrect);
                if (distance < 0.3f)
                {
                    transform.localPosition = posCorrect;
                    objectCollider.enabled = false;
                    return true;
                }
                break;
        }
        return false;
    }
    
}
