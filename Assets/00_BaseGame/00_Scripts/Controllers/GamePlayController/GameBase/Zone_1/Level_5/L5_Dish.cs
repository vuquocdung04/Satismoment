using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class L5_Dish : MonoBehaviour
{
    public int idDish;
    public Level_5Ctrl level5Ctrl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var cup = collision.GetComponent<L5_Cup>();
        if(cup.idCup == idDish)
        {
            Debug.LogError("CHECK");
            cup.transform.SetParent(transform);
            cup.transform.localPosition = Vector3.zero;
            cup.circleCollider.enabled = false;
            CheckWin();
        }
    }

    void CheckWin()
    {
        level5Ctrl.isDragging = false;
        level5Ctrl.cup = null;
        level5Ctrl.amount++;
        if(level5Ctrl.amount > 4)
        {
            DOVirtual.DelayedCall(0.7f, delegate
            {
                WinBox.SetUp().Show();
            });
        }
    }
}
