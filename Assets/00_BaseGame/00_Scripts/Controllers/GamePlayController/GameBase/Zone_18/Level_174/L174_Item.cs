using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EL174_ItemType
{
    Tem, Letter,
}
public class L174_Item : MonoBehaviour
{
    public EL174_ItemType type;
    public SpriteRenderer objRenderer;
    public BoxCollider2D objCollider;
    public Transform posCorrect;
    public void OnStartDrag()
    {
        if(type == EL174_ItemType.Tem)
        {
            
        }
        else
        {
            transform.DORotate(Vector3.zero, 0.2f);
        }
    }

    public void CheckCorrectToPosition(Level_174Ctrl levelCtrl)
    {
        if(type == EL174_ItemType.Tem)
        {
            float distance = Vector2.Distance(transform.position, posCorrect.localPosition);
            Debug.LogError(distance);
            if (Mathf.Abs(distance) < 0.4f)
            {
                transform.DOMove(posCorrect.position, 0.2f).SetEase(Ease.Linear);
                StartCoroutine(levelCtrl.HandleWinCondition());
            }
        }
    }
}
