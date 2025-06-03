using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public enum L50_ItemType
{
    Treasure_Chest,
    Diffience,
}

public class L50_Item : MonoBehaviour
{
    public L50_ItemType itemType;
    public float duration;
    public bool isFish;
    public Tween currentLineMovement;

    float distance;
    private bool facingRight = false;
    private Vector3 startPos;
    private void Start()
    {
        if (!isFish) return;
        distance = -this.transform.position.x;
        MoveTo(startPos + Vector3.right * distance);
    }

    void MoveTo(Vector3 target)
    {
        FaceDirection(target.x > transform.position.x);

        currentLineMovement =  transform.DOMoveX(target.x, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Vector3 nextTarget = (target.x > startPos.x)
                    ? startPos + Vector3.left * distance
                    : startPos + Vector3.right * distance;

                MoveTo(nextTarget);
            });
    }

    void FaceDirection(bool isRight)
    {
        if (facingRight != isRight)
        {
            facingRight = isRight;    
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public void StopMovement()
    {
        if(currentLineMovement != null && currentLineMovement.IsActive())
        currentLineMovement.Kill();
    }

    void OnDestroy()
    {
        StopMovement(); 
    }
}
