using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_36Ctrl : MonoBehaviour
{
    public Transform popIt;
    public int winProgress = 0;
    public bool isWin = false;
    Vector3 posMouse;
    private void Update()
    {
        if (isWin) return;

        posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posMouse.z = 0;

        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(posMouse, Vector2.zero);
            if (hit.collider == null) return;
            hit.collider.gameObject.SetActive(false);
            winProgress++;
            PlayPopAnimation();
        }
        if (Input.GetMouseButtonUp(0))
        {
            CheckWin();
        }
    }

    void PlayPopAnimation()
    {
        popIt.DOScale(1.1f, 0.15f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            // Bật ra lại
            popIt.DOScale(1f, 0.15f).SetEase(Ease.OutSine);
        });
    }

    void CheckWin()
    {
        if(winProgress > 22)
        {
            isWin = true;
            WinBox.SetUp().Show();
        }
    }
}
