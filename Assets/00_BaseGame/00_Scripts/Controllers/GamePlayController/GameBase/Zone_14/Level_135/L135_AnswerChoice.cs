using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L135_AnswerChoice : MonoBehaviour
{
    public int id;
    public Level_135Ctrl levelCtrl;
    public BoxCollider2D objCollider;

    public bool CheckToCorrectPosition()
    {
        foreach (var point in levelCtrl.lsPoints)
        {
            float distance = Vector2.Distance(transform.position, point.transform.position);
            if (Mathf.Abs(distance) < 0.5f)
            {
                transform.DOMove(point.transform.position, 0.2f).SetEase(Ease.Linear);
                if(point.id == id)
                {
                    objCollider.enabled = false;
                    levelCtrl.correctedAnswerCount++;
                }
                return true;
            }
        }

        Debug.Log("Chưa đúng vị trí nào");
        return false;
    }
}
